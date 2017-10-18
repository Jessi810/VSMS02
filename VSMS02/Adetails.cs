using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            if (String.IsNullOrEmpty(currentPassword)) { errEditAdmin.SetError(lblCurrentPassword, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(newPassword)) { errEditAdmin.SetError(lblNewPassword, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(confirmPassword)) { errEditAdmin.SetError(lblConfirmPassword, "Should not be empty"); Validation.ErrorCount += 1; }
            // Check if all form is not empty
            if (!Validation.IsFormValid()) return;
            if (Validation.IsPasswordSame(newPassword, confirmPassword)) { errEditAdmin.SetError(lblConfirmPassword, "Password don't matched"); Validation.ErrorCount += 1; }
            if (!Validation.IsFormValid()) return;

            Validation.ErrorCount = 0;

            // Save new password to database
            using (SqlConnection connection = new SqlConnection(GetConnString()))
            {
                string query = "INSERT into Account (Password,HashKey)" +
                    " VALUES (@password,@hashKey)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    var key = Encryptor.GeneratePassword(16);
                    var hash = Encryptor.EncodePassword(confirmPassword, key);

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

        private string GetConnString()
        {
            string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
            string dbDir = projDir + "\\VSMS.mdf";
            string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";
            return connString;
        }
    }
}
