using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace VSMS02
{
    public partial class Pdetails : Form
    {
        private static string pid = Patients.pid;
        private string connString = ConfigurationManager.ConnectionStrings["VSMS02.Properties.Settings.VSMSConnectionString"].ConnectionString;

        public Pdetails()
        {
            InitializeComponent();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "SELECT * FROM Patient WHERE Id=" + pid;
                Debug.WriteLine("QUERY: " + query);

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string id = reader["Id"].ToString();
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();
                            string middleName = reader["MiddleName"].ToString();

                            lblFullName.Text = firstName + " " + middleName + " " + lastName;
                        }
                    }
                }
            }
            //string query = "SELECT * FROM Patient";
            ////string query = "SELECT * FROM Patient WHERE Id=" + pid;
            //SqlConnection connection = new SqlConnection(connString);
            //connection.Open();
            //SqlDataReader myReader = null;
            //SqlCommand myCommand = new SqlCommand(query, connection);
            //myReader = myCommand.ExecuteReader();
            //if (myReader.Read())
            //{
            //    Debug.WriteLine(myReader["FirstName"].ToString());
            //    Debug.WriteLine(myReader["LastName"].ToString());
            //}
            //connection.Close();
        }

        private void Pdetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
