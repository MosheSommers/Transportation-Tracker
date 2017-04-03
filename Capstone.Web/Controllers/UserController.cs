using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;

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
        public ActionResult Index()
        {
            return View();
        }

        //Get: Register
        public ActionResult Register()
        {
            return View();
        }
    }
}