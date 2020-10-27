using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Amul.Models
{
    [Table("amuladdtocart")]
    public class cart
    {
        [Key]

        public int productid { get; set; }
        public string productname { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

        public string description { get; set; }

        public List<cart> carts { get; set; }



        //public int productid { get; set; }

    }
}