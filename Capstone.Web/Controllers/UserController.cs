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

        [HttpPost]
        public ActionResult LoginPost(String email, String password)
        {
            User u = new User() { EmailAddress = email, Password = password };
            User validatedUser = userDAL.GetUser(u);
            if (validatedUser.EmailAddress != null && validatedUser.Password == u.Password)
            {
                Session["Login"] = validatedUser;
                Session["UserName"] = email;
            }
            else
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        //Get: Register
        public ActionResult Register()
        {
            return View();
        }

        //Get: Logout
        public ActionResult Logout()
        {
            Session.Abandon();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}