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
        public ActionResult LoginPost(User model)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            if (model.EmailAddress != null && model.EmailAddress != "")
            {
                HashProvider hashProvider = new HashProvider();

                User u = new User() { EmailAddress = model.EmailAddress };
                User validatedUser = userDAL.GetUser(u);

                if (validatedUser != null && validatedUser.Password != null && validatedUser.Salt != null)
                {

                    bool passwordMatches = hashProvider.VerifyPasswordMatch(validatedUser.Password, model.Password, validatedUser.Salt);

                    if (validatedUser.EmailAddress != null && passwordMatches)
                    {
                        Session["Login"] = validatedUser;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

            }

            return RedirectToAction("Index");
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