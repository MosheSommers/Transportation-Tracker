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

        // (waypoints)
        public JsonResult WaypointList(int routeID)
        {
            List<string> waypoints = routeDal.GetRoute(new Models.Route { RouteID = routeID }).Waypoints;
            return Json(waypoints, JsonRequestBehavior.AllowGet);
        }

        //POST: Edit
        [HttpPost]
        public ActionResult Edit(int routeID)
        {
            Route routeToEdit = routeDal.GetRoute(new Route { RouteID = routeID });
            return View("Edit", routeToEdit);
        }

        // POST: Edit; Save new Route
        [HttpPost]
        public ActionResult UpdateRoute(Route r) // need to somehow get the RouteID from this page's model
        {
            // Do the DAL stuff
            return RedirectToAction("Index", "Route");
        }
    }
}
