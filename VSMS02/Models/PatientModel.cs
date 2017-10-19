using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSMS02.Models
{
    class PatientModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string ContactName { get; set; }
        public string PhilhealthNumber { get; set; }
        public DateTime DateAdmitted { get; set; }
        public DateTime DateDischarged { get; set; }

        public PatientModel() { }
        public PatientModel(int id) { Id = id; }

        public PatientModel(int id, string fn, string mn, string ln, DateTime bd, string add, string gen, int age, string phone,
            string tel, string cn, string phn, DateTime da, DateTime dd)
        {
            Id = id;
            FirstName = fn;
            MiddleName = mn;
            LastName = ln;
            BirthDate = bd;
            Address = add;
            Gender = gen;
            Age = age;
            PhoneNumber = phone;
            TelephoneNumber = tel;
            ContactName = cn;
            PhilhealthNumber = phn;
            DateAdmitted = da;
            DateDischarged = dd;
        }

        public PatientModel(string fn, string mn, string ln, DateTime bd, string add, string gen, int age, string phone,
            string tel, string cn, string phn, DateTime da, DateTime dd)
        {
            FirstName = fn;
            MiddleName = mn;
            LastName = ln;
            BirthDate = bd;
            Address = add;
            Gender = gen;
            Age = age;
            PhoneNumber = phone;
            TelephoneNumber = tel;
            ContactName = cn;
            PhilhealthNumber = phn;
            DateAdmitted = da;
            DateDischarged = dd;
        }

        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }
    }
}
