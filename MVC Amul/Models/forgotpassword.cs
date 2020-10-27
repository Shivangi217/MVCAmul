using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Amul.Models
{
    public class forgotpassword
    {
        [Table("amulreg")]
        public class Rs
        {
            [Key]
            public int ID { get; set; }

            public string email { get; set; }
        }
    }
}