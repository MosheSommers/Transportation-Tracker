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

        [Required(ErrorMessage = "An email address is required to register")]
        public string EmailAddress { get; set; }


        public string Phone { get; set; }
        public bool IsAdmin { get; set; }

        [Required]
        public string Password { get; set; }


        public string Salt { get; set; }


    }
}