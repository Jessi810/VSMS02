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
    public partial class Padd : Form
    {
        public Padd()
        {
            InitializeComponent();
        }

        private void Padd_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
            string dbDir = projDir + "\\VSMS.mdf";
            string connString2 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";


            string firstName = txtFirstName.Text;
            string middleName = txtMiddleName.Text;
            string lastName = txtLastName.Text;
            string birthDate = dteBirthDate.Text;
            string address = txtAddress.Text;
            string gender = cboGender.Text;
            string age = txtAge.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string telephoneNumber = txtTelephoneNumber.Text;
            string contactName = txtContactName.Text;
            string philhealthNumber = txtPhilhealthNumber.Text;
            string dateAdmitted = dteDateAdmitted.Text;

            // Validate form
            errAddPatient.Clear();
            Validation.ErrorCount = 0;
            if (String.IsNullOrEmpty(firstName)) { errAddPatient.SetError(lblFirstName, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(middleName)) { errAddPatient.SetError(lblMiddleName, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(lastName)) { errAddPatient.SetError(lblLastName, "Should not be empty"); Validation.ErrorCount += 1; }
            if (String.IsNullOrEmpty(age)) { errAddPatient.SetError(lblAge, "Should not be empty"); Validation.ErrorCount += 1; }
            if (!Validation.IsDigitsOnly(age)) { errAddPatient.SetError(lblAge, "Should only be numeric"); Validation.ErrorCount += 1; }
            if (!Validation.IsValidGender(gender)) { errAddPatient.SetError(lblGender, "Invalid gender"); Validation.ErrorCount += 1; }
            if (!Validation.IsValidPhoneNumber(phoneNumber)) { errAddPatient.SetError(lblPhoneNumber, "Invalid phone number"); Validation.ErrorCount += 1; }
            if (!Validation.IsValidTelephoneNumber(telephoneNumber)) { errAddPatient.SetError(lblTelephoneNumber, "Invalid telephone number"); Validation.ErrorCount += 1; }

            if (!Validation.IsFormValid())
            {
                Debug.WriteLine("INVALID FORM : ERROR COUNT " + Validation.ErrorCount);
                return;
            }

            // Reset counter if form is all valid
            Validation.ErrorCount = 0;

            using (SqlConnection connection = new SqlConnection(connString2))
            {
                string query = "INSERT into Patient (FirstName,MiddleName,LastName,BirthDate,Address,Gender,Age,PhoneNumber,TelephoneNumber,ContactName,PhilhealthNumber,DateAdmitted)" +
                    " VALUES (@firstName,@middleName,@lastName,@birthDate,@address,@gender,@age,@phoneNumber,@telephoneNumber,@contactName,@philhealthNumber,@dateAdmitted)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = firstName;
                    cmd.Parameters.Add("@middleName", SqlDbType.NVarChar).Value = middleName;
                    cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = lastName;
                    cmd.Parameters.Add("@birthDate", SqlDbType.DateTime).Value = birthDate;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
                    cmd.Parameters.Add("@gender", SqlDbType.NVarChar).Value = gender;
                    cmd.Parameters.Add("@age", SqlDbType.Int).Value = Int32.Parse(age);
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@telephoneNumber", SqlDbType.NVarChar).Value = telephoneNumber;
                    cmd.Parameters.Add("@contactName", SqlDbType.NVarChar).Value = contactName;
                    cmd.Parameters.Add("@philhealthNumber", SqlDbType.NVarChar).Value = philhealthNumber;
                    cmd.Parameters.Add("@dateAdmitted", SqlDbType.DateTime).Value = dateAdmitted;
                    connection.Open();
                    
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Error adding patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Success adding patient.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form form = new Patients();
                    form.Show();
                }
            }
        }

        private void Padd_Load(object sender, EventArgs e)
        {
            cboGender.SelectedItem = "Male";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Patients();
            form.Show();
        }
    }
}
