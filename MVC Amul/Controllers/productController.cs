using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MVC_Amul.Models;


namespace MVC_Amul.Controllers
{
    public class productController : Controller
    {
        // GET: product
        public ActionResult Index()
        {


            
                String mycon = "Data Source=10.112.60.43;Initial Catalog=Student;User ID=student;Password=student";
                String myquery = "Select * from amuladdproduct";
                SqlConnection con = new SqlConnection(mycon);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = myquery;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                productdetails pd1 = new productdetails();
                List<productdetails> productlist = new List<productdetails>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    productdetails pd = new productdetails();
                    pd.productid = Convert.ToInt32(ds.Tables[0].Rows[i]["productid"].ToString());
                    pd.productname = ds.Tables[0].Rows[i]["productname"].ToString();
                    pd.price = Convert.ToInt64(ds.Tables[0].Rows[i]["price"].ToString());
                    pd.productimage = ds.Tables[0].Rows[i]["productimage"].ToString();
                    productlist.Add(pd);
                }
                pd1.productlist = productlist;
                con.Close();

                return View(pd1);
            }


        //    return View();
        //}
    }
}