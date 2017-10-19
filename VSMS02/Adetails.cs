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

        private Patients patientsForm = null;
        public Adetails(Form callingForm)
        {
            patientsForm = callingForm as Patients;
            this.patientsForm.Enabled = false;

            InitializeComponent();
        }

        private void Adetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.patientsForm.Enabled = true;
            this.patientsForm.FormClosable = false;
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
                            MessageBox.Show("Invalid", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Invalid current password", "Account Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void SaveNewPassword(string un, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(GetConnString()))
            {
                string query = "UPDATE Account SET Password=@password, HashKey=@hashKey WHERE Username='" + un + "'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    var key = Encryptor.GeneratePassword(16);
                    var hash = Encryptor.EncodePassword(newPassword, key);

                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@password", hash);
                    cmd.Parameters.AddWithValue("@hashKey", key);
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

        private void SavePersonalInfo(string username, string newUsername, string newName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnString()))
            {
                string query = "UPDATE Account SET Username=@username, Name=@name WHERE Username='" + username + "'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@username", newUsername);
                    cmd.Parameters.AddWithValue("@name", newName);
                    if (!SessionData.UserName.Equals(newUsername))
                    {
                        SessionData.UserName = newUsername;
                    }
                    connection.Open();

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Error changing personal info.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Success changing personal info.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    //this.Hide();
                    //Form form = new Patients();
                    //form.Show();
                }
            }
        }

        private string GetName()
        {
            using (SqlConnection connection = new SqlConnection(GetConnString()))
            {
                connection.Open();

                string query = "SELECT Name FROM Account WHERE Username='" + SessionData.UserName + "'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["Name"].ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
        }

        private void btnSavePersonal_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string name = txtName.Text;

            errEditAdmin.Clear();
            Validation.ErrorCount = 0;
            if (String.IsNullOrEmpty(username)) { errEditAdmin.SetError(lblUsername, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(name)) { errEditAdmin.SetError(lblName, "Should not be empty"); Validation.ErrorCount += 1; }
            if (!Validation.IsFormValid()) return;
            if (!Validation.IsValidUsername(username)) { errEditAdmin.SetError(lblUsername, "Invalid username"); Validation.ErrorCount += 1; }
            if (!Validation.IsValidName(name)) { errEditAdmin.SetError(lblName, "Invalid name"); Validation.ErrorCount += 1; }
            if (!Validation.IsFormValid()) return;

            Validation.ErrorCount = 0;

            SavePersonalInfo(SessionData.UserName, username, name);
        }

        private void Adetails_Load(object sender, EventArgs e)
        {
            txtUsername.Text = SessionData.UserName;
            txtName.Text = GetName();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Patients();
            form.Show();
        }
    }
}
