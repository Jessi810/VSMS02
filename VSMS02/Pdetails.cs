﻿using System;
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
using System.IO;

namespace VSMS02
{
    public partial class Pdetails : Form
    {
        private static string pid = Patients.pid;
        private string connString = ConfigurationManager.ConnectionStrings["VSMS02.Properties.Settings.VSMSConnectionString"].ConnectionString;

        BindingSource bindingSource1 = new BindingSource();

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
        }

        private void Pdetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void GetData(string selectCommand)
        {
            try
            {
                // Specify a connection string. Replace the given value with a 
                // valid connection string for a Northwind SQL Server sample
                // database accessible to your system.
                string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
                string dbDir = projDir + "\\VSMS.mdf";
                string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";

                // Create a new data adapter based on the specified query.
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, connString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand. These are used to
                // update the database.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                //grdData.AutoResizeColumns(
                //    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void Pdetails_Load(object sender, EventArgs e)
        {
            // Bind the DataGridView to the BindingSource
            // and load the data from the database.
            grdData.DataSource = bindingSource1;
            GetData("SELECT * FROM Data WHERE Patient_Id=" + pid);
        }
    }
}
