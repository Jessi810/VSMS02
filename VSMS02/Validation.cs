using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSMS02
{
    class Validation
    {
        public static int ErrorCount = 0;

        public static bool IsFormValid()
        {
            return ErrorCount == 0;
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        public static bool IsValidGender(string str)
        {
            if (str.ToLower().Equals("male")) return true;
            if (str.ToLower().Equals("female")) return true;

            return false;
        }

        public static bool IsValidPhoneNumber(string str)
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

        public static bool IsValidTelephoneNumber(String str)
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

        public static bool IsPasswordSame(string pw1, string pw2)
        {
            return pw1.Equals(pw2) ? true : false;
        }
    }
}
