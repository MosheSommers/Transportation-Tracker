using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string ConfirmedPassword { get; set; }

        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
        public string Salt { get; set; }


    }
}