using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class RegisterUser : User
    {        
        [Required]
        public string ConfirmedPassword { get; set; }
    }
}