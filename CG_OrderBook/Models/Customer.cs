using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace CG_OrderBook.Models
{
    public class Customer
    {
        public int CustomerID { get; set;  }

        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname Required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$",
            ErrorMessage = "Please Enter a Valid Email")] //Valid Email
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$", 
            ErrorMessage = "Please Enter Valid Phone Number")] //Valid UK phone Number
        public string Phone { get; set; } //Has to be a string due to the 0 at the beginning of the number
    }
}