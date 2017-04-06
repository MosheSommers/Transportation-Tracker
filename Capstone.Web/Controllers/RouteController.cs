using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class RouteController : Controller
    {
        private IRouteDAL routeDal;
        public RouteController(IRouteDAL routeDal)
        {
            this.routeDal = routeDal;
        }
        // GET: Route
        public ActionResult Index()
        {
           List<Route> routes = routeDal.GetAllRoutes();
            return View(routes);
        }


        // GET: Create
        public ActionResult Create()
        {
            var user = (Capstone.Web.Models.User)Session["Login"];
            if (user != null && user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(Route r)
        {
            routeDal.InsertRoute(r);
            return RedirectToAction("Index");
        }
    }
}