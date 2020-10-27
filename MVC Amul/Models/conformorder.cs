using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_Amul.Models
{
    [Table("amuladdress")]
    public class conformorder
    {
        [Key]

        [Required(ErrorMessage = "Please Enter fullname")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Please Enter city")]
        public string city { get; set; }


        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public int contatno { get; set; }








        
    }
}