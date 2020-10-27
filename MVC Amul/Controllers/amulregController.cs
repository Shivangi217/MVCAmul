using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Amul.Models;
using System.Web.Helpers;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;


namespace MVC_Amul.Controllers
{
    public class amulregController : Controller
    {
        private DB db = new DB();
        //public string EmailIds { get; private set; }

        // GET: amulreg
        public ActionResult Index()
        {
            using (DB db = new DB())
            {
                return View(db.amulregs.ToList());
            }

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(amulreg account)
        {
            if (ModelState.IsValid)
            {
                using (DB db = new DB())
                {
                    db.amulregs.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.first_name + " " + account.last_name + "successfully registered.";
            }
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult login(amulreg user)
        {
            using (DB db = new DB())
            {
                var usr = db.amulregs.Where(u => u.user_name == user.user_name && u.password == user.password).FirstOrDefault();

                if (usr != null)
                {
                    Session["ID"] = usr.ID.ToString();
                    Session["user"] = 1;
                    Session["user_name"] = usr.user_name.ToString();
                    return RedirectToAction("loggedIn");
                }
                else if (user.user_name == "admin" && user.password == "admin")
                {
                    //Session["ID"] = usr.ID.ToString();
                    Session["admin"] = 1;
                    return RedirectToAction("Adminhome", "amuladdproducts");
                }
                else
                {
                    ModelState.AddModelError("", "username or password is wrong.");
                }
            }
            return View();
        }

        public ActionResult loggedIn()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public ActionResult Logout()
        {
            Session["admin"] = null;
            Session["user"] = null;
            Session["ID"] = null;
            // Session["user"] = 1;
            Session["user_name"] = null;
            Session["cart"] = null;
            Session["count"] = null;
            Response.Cache.SetNoStore();
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult forgotpassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult forgotpassword(string email)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            //bool status = false;

           // using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var account = db.amulregs.Where(a => a.email == email).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.email, resetCode, "ResetPassword");
                    account.password = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

       
    
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("patelshivangi217@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
        var fromEmailPassword = "Shiv@ngi8450"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

    }
}