using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class Register : Form
    {
        private string connString = ConfigurationManager.ConnectionStrings["VSMS02.Properties.Settings.VSMSConnectionString"].ConnectionString;

        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter a valid username and password.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Equals(confirmPassword))
            {
                var key = Encryptor.GeneratePassword(16);
                var hash = Encryptor.EncodePassword(password, key);

                string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
                string dbDir = projDir + "\\VSMS.mdf";
                string connString2 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connString2))
                {
                    string query = "INSERT into Account (Username,Password,HashKey) VALUES (@username,@password,@hashKey)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Connection = connection;
                        cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = hash;
                        cmd.Parameters.Add("@hashKey", SqlDbType.NVarChar).Value = key;
                        connection.Open();
                        //cmd.ExecuteNonQuery();

                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Your account has been registered!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form form = new Login();
                            form.Show();
                        }
                        else
                        {
                            MessageBox.Show("Registration has failed. Please try again later or contact and admin.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
                MessageBox.Show("The password don't matched. Re-type your password", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            

        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Login();
            form.Show();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister_Click(this, new EventArgs());
            }
        }
    }
}
