using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

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
            User u = new User() { EmailAddress = email, Password = password };
            User validatedUser = userDAL.GetUser(u);
            if (validatedUser.EmailAddress != null && validatedUser.Password == u.Password)
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