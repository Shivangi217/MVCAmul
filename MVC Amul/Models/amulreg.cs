using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVC_Amul.Models;
using System.Data.SqlClient;
using System.Data;

namespace MVC_Amul.Models
{
    [Table("amulreg")]
    public class amulreg
    {
        [Key]

        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Please Enter LastName")]
        public string last_name { get; set; }


        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]

        public string email { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]

        public string address { get; set; }

        [Required(ErrorMessage = "Please Enter city")]

        public string city { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]

        public string contact_no { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]

        public string user_name { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]

        public string password { get; set; }
        //    [Required(ErrorMessage = "Please Enter Password")]
        //    [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
    }
}
