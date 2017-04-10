using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }

        private string phone;
        public string Phone
        {
            get { return phone; }

            set
            {
                if (value != null)
                {
                    phone = CreateNumber(value);
                }
            }
               
        }

        public bool IsAdmin { get; set; }
        public string Salt { get; set; }

        private static string CreateNumber(string text)
        {
            string phoneNum = "";
            foreach (char c in text)
            {
                
                if (char.IsDigit(c))
                {
                    phoneNum += c;
                }
            }
            if (phoneNum[0] == '1')
            {
                phoneNum = phoneNum.Substring(1);
            }
            if (phoneNum.Length > 10)
            {
                phoneNum = phoneNum.Substring(0, 10);
            }
          return  phoneNum;
        }
    }
}