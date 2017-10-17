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
            //string gender = cboGender.SelectedItem.ToString();
            string gender = cboGender.Text; Debug.WriteLine(cboGender.Text);
            string age = txtAge.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string telephoneNumber = txtTelephoneNumber.Text;
            string contactName = txtContactName.Text;
            string philhealthNumber = txtPhilhealthNumber.Text;
            string dateAdmitted = dteDateAdmitted.Text;

            errAddPatient.Clear();
            if (String.IsNullOrEmpty(firstName)) { errAddPatient.SetError(txtFirstName, "Should not be empty"); }
            if (String.IsNullOrEmpty(middleName)) { errAddPatient.SetError(txtMiddleName, "Should not be empty"); }
            if (String.IsNullOrEmpty(lastName)) { errAddPatient.SetError(txtLastName, "Should not be empty"); }
            if (!IsDigitsOnly(age)) { errAddPatient.SetError(txtAge, "Should only be numeric"); }
            if (!IsValidGender(gender)) { errAddPatient.SetError(cboGender, "Invalid gender"); }
            if (!IsValidPhoneNumber(phoneNumber)) { errAddPatient.SetError(txtPhoneNumber, "Invalid phone number"); }
            if (!IsValidTelephoneNumber(telephoneNumber)) { errAddPatient.SetError(txtTelephoneNumber, "Invalid telephone number"); }

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
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Padd_Load(object sender, EventArgs e)
        {
            cboGender.SelectedItem = "Male";
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        bool IsValidGender(string str)
        {
            if (str.ToLower().Equals("male")) return true;
            if (str.ToLower().Equals("female")) return true;

            return false;
        }

        bool IsValidPhoneNumber(string str)
        {
            string validChar = "0123456789-+ ";
            foreach (char c in str)
            {
                if (!validChar.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsValidTelephoneNumber(String str)
        {
            string validChar = "0123456789-";
            foreach (char c in str)
            {
                if (!validChar.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
