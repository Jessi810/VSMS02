using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSMS02
{
    public partial class Adetails : Form
    {
        public Adetails()
        {
            InitializeComponent();
        }

        private void Adetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Validate form
            errEditAdmin.Clear();
            Validation.ErrorCount = 0;
            if (String.IsNullOrEmpty(SessionData.UserName)) { Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(currentPassword)) { errEditAdmin.SetError(lblCurrentPassword, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(newPassword)) { errEditAdmin.SetError(lblNewPassword, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(confirmPassword)) { errEditAdmin.SetError(lblConfirmPassword, "Should not be empty"); Validation.ErrorCount += 1; }
            // Check if all form is not empty
            if (!Validation.IsFormValid()) return;
            if (!Validation.IsPasswordSame(newPassword, confirmPassword)) { errEditAdmin.SetError(lblConfirmPassword, "Password don't matched"); Validation.ErrorCount += 1; }
            if (!Validation.IsFormValid()) return;

            Validation.ErrorCount = 0;

            // Validate if current password is valid
            bool valid = IsPasswordCorrect(SessionData.UserName, currentPassword);

            if (!valid) return;

            // Save new password to database
            SaveNewPassword(SessionData.UserName, confirmPassword);
        }

        private string GetConnString()
        {
            string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
            string dbDir = projDir + "\\VSMS.mdf";
            string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";
            return connString;
        }

        private string GetUserHashKey(string un)
        {
            // Get hash key of user for decoding password
            using (SqlConnection connection = new SqlConnection(GetConnString()))
            {
                connection.Open();

                string query = "SELECT HashKey FROM Account WHERE Username='" + un + "'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["HashKey"].ToString();
                        }
                        else
                        {
                            // TODO: Add error message when a hash key is not found
                        }
                    }
                }

                return null;
            }
        }

        private bool IsPasswordCorrect(string un, string pw)
        {
            using (SqlConnection connection = new SqlConnection(GetConnString()))
            {
                connection.Open();

                string query = "SELECT Password,HashKey FROM Account WHERE Username='" + un + "'";
                string hashKey = "";
                string oldPassword = "";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashKey = reader["HashKey"].ToString();
                            oldPassword = reader["Password"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Invalid1", "Account Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }

                string encodeNewPassword = Encryptor.EncodePassword(pw, hashKey);

                Debug.WriteLine("--- IsPasswordCorrect ------------------------------");
                Debug.WriteLine("Query       : " + query);
                Debug.WriteLine("Hash Key    : " + hashKey);
                Debug.WriteLine("Old Password: " + oldPassword);
                Debug.WriteLine("New Password: " + encodeNewPassword);

                //return encodedPassword.Equals(pw) ? true : false;
                if (encodeNewPassword.Equals(oldPassword))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Invalid2", "Account Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void SaveNewPassword(string un, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(GetConnString()))
            {
                string query = "UPDATE Account (Password,HashKey)" +
                    " SET Password=@password, HashKey=@hashKey" +
                    " WHERE Username='" + un + "'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    var key = Encryptor.GeneratePassword(16);
                    var hash = Encryptor.EncodePassword(newPassword, key);

                    cmd.Connection = connection;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = hash;
                    cmd.Parameters.Add("@hashKey", SqlDbType.NVarChar).Value = key;
                    connection.Open();

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Error changing password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Success changing password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtCurrentPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";

                    //this.Hide();
                    //Form form = new Patients();
                    //form.Show();
                }
            }
        }
    }
}
