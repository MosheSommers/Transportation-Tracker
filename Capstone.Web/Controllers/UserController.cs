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
        public ActionResult Register(RegisterUser model)
        {
            if (model.Password != model.ConfirmedPassword && !String.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("UnconfirmedPassword", "Please Confirm Your Password.");
            }
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }
        
            if (userDAL.GetUser(model).EmailAddress != model.EmailAddress)
            {
                HashProvider hashProvider = new HashProvider();
                string hashedPassword = hashProvider.HashPassword(model.Password); //<-- password they provided during registration
                string salt = hashProvider.SaltValue;
                model.Salt = salt;
                model.Password = hashedPassword;
                userDAL.InsertNewUser(model);
                User validatedUser = userDAL.GetUser(model);
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