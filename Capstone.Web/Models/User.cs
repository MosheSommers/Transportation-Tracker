using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
    }
}