using MVC_Amul.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Amul.Controllers
{
    public class amuladdproductsController : Controller
    {
        private StudentEntities1 db = new StudentEntities1();
        private StudentEntities3 db1 = new StudentEntities3();
        private DB dB = new DB();

        // GET: amuladdproducts
        public ActionResult Index()
        {
            return View(db.amuladdproducts.ToList());
        }

        public ActionResult viewdetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            amuladdproduct amuladdproduct = db.amuladdproducts.Find(id);
            if (amuladdproduct == null)
            {
                return HttpNotFound();
            }
            return View(amuladdproduct);
        }

        public ActionResult productlist()
        {
            return View(db.amuladdproducts.ToList());
        }
        // GET: amuladdproducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            amuladdproduct amuladdproduct = db.amuladdproducts.Find(id);
            if (amuladdproduct == null)
            {
                return HttpNotFound();
            }
            return View(amuladdproduct);
        }

        // GET: amuladdproducts/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult userdetails()
        {
            return View(dB.amulregs.ToList());
        }
        public ActionResult Adminhome()
        {
            return View();
        }

        // POST: amuladdproducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,productname,productimage,price,description,quantity,categoryname,available,soldout")] amuladdproduct amuladdproduct)
        {
            if (ModelState.IsValid)
            {
                db.amuladdproducts.Add(amuladdproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amuladdproduct);
        }

        // GET: amuladdproducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            amuladdproduct amuladdproduct = db.amuladdproducts.Find(id);
            if (amuladdproduct == null)
            {
                return HttpNotFound();
            }
            return View(amuladdproduct);
        }

        // POST: amuladdproducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productid,productname,productimage,price,description,quantity,categoryname,available,soldout")] amuladdproduct amuladd, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (file.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string filepath = Path.Combine(Server.MapPath("~/image"), filename);

                        file.SaveAs(filepath);

                        //masterEntities1 st = new masterEntities1                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                        amuladd.productimage = filename.ToString();
                        db.Entry(amuladd).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "amuladdproducts");
                    }

                    TempData["Add"] = "Product is successfully Added";
                    return RedirectToAction("Index", "amuladdproducts");
                }
                catch
                {
                    TempData["fail"] = "Product Not Added";
                    return RedirectToAction("Edit", "amuladdproducts");
                }

                //db.Entry(amuladdproduct).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(amuladd);
        }

        // GET: amuladdproducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            amuladdproduct amuladdproduct = db.amuladdproducts.Find(id);
            if (amuladdproduct == null)
            {
                return HttpNotFound();
            }
            return View(amuladdproduct);
        }

        // POST: amuladdproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            amuladdproduct amuladdproduct = db.amuladdproducts.Find(id);
            db.amuladdproducts.Remove(amuladdproduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult conformorder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult conformorder(conformorder c1)
        {



            conformorder c11 = new conformorder();
            using (SqlConnection con = new SqlConnection("Data Source = 10.112.60.43; Initial Catalog = Student; User ID = student; Password = student" ))
            {
                using (SqlCommand cmd = new SqlCommand("usercrudoperations", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", c11.fullname);
                    cmd.Parameters.AddWithValue("@education", c11.address);
                    cmd.Parameters.AddWithValue("@location", c11.city);
                    cmd.Parameters.AddWithValue("@status", c11.email);
                    cmd.Parameters.AddWithValue("@status", c11.contatno);
                    con.Open();
                    ViewData["conformorder"] = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return View();

        }
        

        public ActionResult Upload()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, amuladdproduct Fadd)
        {
            try
            {


                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("~/image"), filename);
                    // string res = filename.GetLast(3);
                    string ext = filename.Substring(filename.Length - 3);
                    if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "PNG" || ext == "JPG")
                    {
                        file.SaveAs(filepath);

                        Fadd.productimage = filename.ToString();

                        db.amuladdproducts.Add(Fadd);
                        db.SaveChanges();
                        var sd = db.amuladdproducts.ToList().LastOrDefault();
                        // Session["ppid"] = sd.Pid;
                        TempData["Addt"] = "Product is successfully Added";
                        return RedirectToAction("Adminhome", "amuladdproducts");
                    }
                    else
                    {
                        TempData["errr"] = "Only Upload Jpg Or Png Files";
                        return RedirectToAction("Upload", "amuladdproducts");
                    }

                }
                else
                {
                    return RedirectToAction("Upload", "amuladdproducts");
                }


            }
            catch
            {
                TempData["failt"] = "Product Not Added";
                return RedirectToAction("Upload", "amuladdproducts");
            }


        }
        //public ActionResult Buy()
        //{
        //    return View(db.amuladdproducts.ToList());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Buy(FormCollection formCollection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        amuladdtocart c1 = new amuladdtocart();
        //        c1.productid = Convert.ToInt32(formCollection["productid"]);
        //        c1.productname = formCollection["productname"];
        //        c1.price = Convert.ToInt32(formCollection["price"]);
        //        c1.description = formCollection["description"];
        //        c1.quantity = Convert.ToInt32(formCollection["quantity"]);
        //        //Session["tot"] = c1.price * c1.quantity;
        //        //dB.carts.Add(c1);
        //        db1.amuladdtocarts.Add(c1);
        //        dB.SaveChanges();
        //        RedirectToAction("Add");
        //    }
        //    return View();
        //}

        //public ActionResult Index1()
        //{
        //    return View(db.amuladdproducts.OrderByDescending(x => x.productid).ToString());
        //}

        DataTable dt;
        amuladdproduct ad = new amuladdproduct();
        //// GET: AddToCart  
        public ActionResult Buy(productdetails mo)
        {


            if (Session["cart"] == null)
            {
                List<productdetails> li = new List<productdetails>();

                li.Add(mo);
                Session["cart"] = li;
                ViewBag.cart = li.Count();


                Session["count"] = 1;


            }
            else
            {
                List<productdetails> li = (List<productdetails>)Session["cart"];
                li.Add(mo);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return View(db.amuladdproducts.ToList());



        }

        //public ActionResult Remove(amuladdproduct mob)
        //{
        //    List<amuladdproduct> li = (List<amuladdproduct>)Session["cart"];
        //    li.RemoveAll(x => x.productid == mob.productid);
        //    Session["cart"] = li;
        //    Session["count"] = Convert.ToInt32(Session["count"]) - 1;
        //    return RedirectToAction("Remove");

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(FormCollection formCollection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //          cart c1 = new cart();
        //        c1.productid = Convert.ToInt32(formCollection["productid"]);
        //        c1.productname = formCollection["productname"];
        //        c1.price = Convert.ToInt32(formCollection["price"]);
        //        c1.description = formCollection["description"];
        //        c1.quantity = Convert.ToInt32(formCollection["quantity"]);
        //          //dB.carts.Add(c1);
        //        dB.SaveChanges();
        //        RedirectToAction("Add");
        //    }
        //    return View();
        //}




        //public ActionResult Cart(int? id)
        //{
        //    Session["lim"] = null;
        //    using (var db = new StudentEntities1())
        //    {
        //        Session["out"] = null;
        //        ceramikatile tl = st.ceramikatiles.Where(i => i.Id == id).SingleOrDefault();
        //        Session["ide"] = tl.Id;
        //        TempData["ud"] = tl.Id;
        //        if (Convert.ToInt32(tl.Quantity) == 0)
        //        {
        //            Session["out"] = "product Out Of Stock";
        //        }
        //        int stk = Convert.ToInt32(tl.Quantity);
        //        int pnd = Convert.ToInt32(tl.Pending);
        //        Session["lim"] = stk - pnd;
        //        //Session["vv"] = Convert.ToInt32(tl.Quantity) - Convert.ToInt32(Session["vv1"]);
        //        List<Prodvm> data = (from s1 in db.ceramikacategories join s2 in db.ceramikatiles on s1.Pid equals s2.Pid where s2.Id == id select new Prodvm { ceramikacategory = s1, ceramikatile = s2 }).ToList();
        //        return View(data);
        //    }

        //}

        //List<cart> li = new List<cart>();
        //[HttpPost]
        //public ActionResult Cart(ceramikacategory pi, string qty, int id, ceramikatile ti)
        //{
        //    Session["out"] = null;
        //    Session["vv"] = null;
        //    Session["vv1"] = null;
        //    int de = Convert.ToInt32(Session["ide"]);
        //    int dd = Convert.ToInt32(TempData["ud"]);
        //    ceramikatile d = st.ceramikatiles.Where(u => u.Id == id).SingleOrDefault();
        //    int ppiidd = d.Pid;
        //    ceramikacategory p = st.ceramikacategories.Where(x => x.Pid == ppiidd).SingleOrDefault();

        //    TempData["pid"] = d.Id;
        //    cart c = new cart();
        //    c.productid = d.Id;
        //    c.productname = p.Productname;
        //    c.type = p.Tname;
        //    if (Convert.ToInt32(d.Quantity) == 0)
        //    {
        //        Session["out"] = "product Out Of Stock";
        //    }
        //    c.price = Convert.ToInt32(d.Price);
        //    c.qty = Convert.ToInt32(qty);
        //    c.bill = c.price * c.qty;
        //    ceramikatile ms = st.ceramikatiles.Where(u => u.Id == id).SingleOrDefault();
        //    int penn = Convert.ToInt32(ms.Pending) + Convert.ToInt32(qty);
        //    ms.Pending = penn.ToString();
        //    // UpdateModel(ms);
        //    st.SaveChanges();
        //    ceramikatile uus = st.ceramikatiles.Where(u => u.Id == id).SingleOrDefault();
        //    int stk = Convert.ToInt32(uus.Quantity);
        //    int pnd = Convert.ToInt32(uus.Pending);
        //    Session["lim"] = stk - pnd;


        //    Session["vv1"] = Convert.ToInt32(qty);

        //    if (TempData["cart"] == null)
        //    {
        //        li.Add(c);
        //        TempData["cart"] = li;
        //    }
        //    else
        //    {

        //        List<cart> li2 = TempData["cart"] as List<cart>;
        //        int flag = 0;
        //        foreach (var item in li2)
        //        {
        //            if (item.productid == c.productid)
        //            {
        //                item.qty += c.qty;
        //                item.bill += c.bill;
        //                flag = 1;
        //            }
        //        }
        //        if (flag == 0)
        //        {
        //            li2.Add(c);
        //        }
        //        TempData["cart"] = li2;
        //    }
        //    using (var db = new StudentEntities())
        //    {
        //        //int pid = id;
        //        var data = (from s1 in db.ceramikacategories join s2 in db.ceramikatiles on s1.Pid equals s2.Pid where s2.Id == id select new Prodvm { ceramikacategory = s1, ceramikatile = s2 }).ToList();
        //        // return View(data);
        //    }
        //    TempData.Keep();
        //    return RedirectToAction("pdisplay");
        //}




    }
}

