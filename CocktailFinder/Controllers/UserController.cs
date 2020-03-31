using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CocktailFinder.Models;

namespace CocktailFinder.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ViewResult Account()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserCredentials user)
        {
            UserLogin login = user.UserLogin;
            ViewBag.Page = "Login";
            string message = "";

            using (DrinkDBEntities dc = new DrinkDBEntities())
            {
                var queryResult = dc.Users.Where(u => u.Username == login.Username).FirstOrDefault();
                if(queryResult != null)
                {
                    if(string.Compare(PasswordHashing.Hash(login.Password), queryResult.Password) == 0)
                    {
                        #region Cookie to stay logged in
                        int timeout = login.RememberMe ? 10000 : 60;
                        var ticket = new FormsAuthenticationTicket(login.Username, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        #endregion

                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        message = "Password is incorrect!";
                    }
                }
                else
                {
                    message = "Invalid credentials";
                }
            }

            ViewBag.Message = message;
            return View("Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsVerified, ActivationCode")] UserCredentials userCred)
        {
            ViewBag.Page = "Registration";
            User user = new User();
            user = userCred.UserRegistration;

            bool isSuccessful = false;
            string message = "";
            

            if (ModelState.IsValid)
            {
                #region Check if email and username are taken
                var isExists = IsEmailExist(user.Email, user.Username);
                if (isExists[0])
                {
                    ModelState.AddModelError("EmailExists", "Email already Exists");
                    
                    return View("Account");
                }
                if (isExists[1])
                {
                    ModelState.AddModelError("UsernameExists", "Username is taken!");
                    return View("Account");
                }
                #endregion

                #region Generate Activation Code

                user.ActivationCode = Guid.NewGuid();

                #endregion

                #region Pw hashing
                user.Password = PasswordHashing.Hash(user.Password);
                user.ConfirmPassword = PasswordHashing.Hash(user.ConfirmPassword);
                #endregion

                user.isVerified = false;

                //cHxY2:)CoCkTaIlFiNdeR

                #region Save to DB
                using (DrinkDBEntities dc = new DrinkDBEntities())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    SendEmailVerification(user.Username, user.Email, user.ActivationCode.ToString());
                    message = "Successful Registration! Check your email and verify your account!";
                    isSuccessful = true;

                }
                #endregion
            } else
            {
                message = "Invalid request!";
            }
            ViewBag.Status = isSuccessful;
            ViewBag.Message = message;
            return View("Account");
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using(DrinkDBEntities dc = new DrinkDBEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var query = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if( query != null)
                {
                    // TODO: See if the user already activated the account!

                    query.isVerified = true;
                    dc.SaveChanges();
                    Status = true;
                } else
                {
                    ViewBag.Message = "Invalid Request!";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        [NonAction]
        public Boolean[] IsEmailExist(string email, string username)
        {
            using (DrinkDBEntities dc = new DrinkDBEntities())
            {
                var equery = dc.Users.Where(u => u.Email == email).FirstOrDefault();
                var uquery = dc.Users.Where(u => u.Username == username).FirstOrDefault();
                Boolean[] exists = { true, true};
                if (equery == null) exists[0] = false;
                if (uquery == null) exists[1] = false;
                return exists;
            }
        }

        public void SendEmailVerification(string username, string email, string code)
        {
            var verifyUrl = "/User/VerifyAccount/" + code;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("geriboygaming@gmail.com", "Cocktail Finder");
            var toEmail = new MailAddress(email);
            var fromPassword = "cHxY2:)CoCkTaIlFiNdeR";
            string subject = "Hello " + username + "! Wellcome to the world of Cocktails!";
            string body = "Your account is successfully created! Click the link below to verify your account! <br/> <a href='" +
                link + "'> Click Here to activate! </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromPassword)
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