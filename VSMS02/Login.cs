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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtUsername.Text = "admin";
            txtPassword.Text = "admin";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsername.Text) || String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter username and password.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string hashKey = "";
            string passwordHash = "";

            string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
            string dbDir = projDir + "\\VSMS.mdf";
            string connString2 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";

            // Get hash key of user for decoding password
            using (SqlConnection connection = new SqlConnection(connString2))
            {
                connection.Open();

                string query = "SELECT HashKey FROM Account WHERE Username='" + username + "'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashKey = reader["HashKey"].ToString();
                            Debug.WriteLine("Hash Key: " + hashKey);
                        }
                    }
                }
            }

            var hash = Encryptor.EncodePassword(password, hashKey);
            Debug.WriteLine("Decoded Hash: " + hash);

            // Decode password
            using (SqlConnection connection = new SqlConnection(connString2))
            {
                connection.Open();

                string query = "SELECT Password FROM Account WHERE Username='" + username + "'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            passwordHash = reader["Password"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Error. Please try again later or contact an admin.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Debug.WriteLine("Password Hash: " + passwordHash);
                    }
                }
            }

            if (!passwordHash.Equals(hash))
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("You've been login successfully!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // If reached, all success
            this.Hide();
            Form form = new Patients();
            form.Show();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new Register();
            this.Hide();
            form.Show();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }
    }
}
