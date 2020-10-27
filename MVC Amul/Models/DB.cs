using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVC_Amul.Models
{
    public class DB:DbContext
    {
        public DbSet<amulreg> amulregs { get; set; }
        //public DbSet<productdetails> productdetails { get; set; }
        //public DbSet<cart> carts { get; set; }
       
    }

}