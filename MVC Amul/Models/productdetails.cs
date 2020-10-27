using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Amul.Models
{
    [Table("amuladdproducts")]
    public class productdetails
    {


        [Key]
        public int productid { get; set; }
        public string productname { get; set; }
        public float price { get; set; }
        public string productimage { get; set; }

        public int quantity { get; set; }

        public int description { get; set; }
        //public int avilable { get; set; }


        public List<productdetails> productlist { get; set; }






       
    }
}