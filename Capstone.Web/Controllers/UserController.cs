using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Capstone.Web.Security;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserDAL userDAL;

        public UserController(IUserDAL userDAL)
        {
            this.userDAL = userDAL;
        }
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult LoginPost(String email, String password)
        {
            HashProvider hashProvider = new HashProvider();
            string hashedPassword = hashProvider.HashPassword(password); //<-- password they provided during registration
            User u = new User() { EmailAddress = email, Password = hashedPassword };
            User validatedUser = userDAL.GetUser(u);

            bool passwordMatches = hashProvider.VerifyPasswordMatch(u.Password, password, validatedUser.Salt); 

            if (validatedUser.EmailAddress != null && passwordMatches)
            {
                Session["Login"] = validatedUser;
            }
            else
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(String email, String password, String phone)
        {
            User u = new Models.User() { EmailAddress = email, Password = password, Phone = phone };
            if (userDAL.GetUser(u).EmailAddress != u.EmailAddress)
            {
                HashProvider hashProvider = new HashProvider();
                string hashedPassword = hashProvider.HashPassword(u.Password); //<-- password they provided during registration
                string salt = hashProvider.SaltValue;
                u.Salt = salt;
                u.Password = hashedPassword;
                userDAL.InsertNewUser(u);
                User validatedUser = userDAL.GetUser(u);
                Session["Login"] = validatedUser;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Abandon();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}