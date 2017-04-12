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
            List<DateTime> times = routeDal.GetRoute(new Models.Route { RouteID = routeID }).Times;

            var result = new { waypoints = waypoints, times = times };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //POST: Edit Waypoints
        [HttpPost]
        public ActionResult Edit(int routeID, string editAction)
        {
            if (editAction == "Edit Users")
            {
                Route routeToEdit = routeDal.GetRoute(new Route { RouteID = routeID });
                return View("EditUsersOnRoute", routeToEdit);
            }
            else if (editAction == "Edit Route")
            {
                Route routeToEdit = routeDal.GetRoute(new Route { RouteID = routeID });
                return View("Edit", routeToEdit);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Edit; Save new Route
        [HttpPost]
        public ActionResult UpdateRoute(Route r)
        {
            routeDal.RemoveWaypointsFromRoute(r);
            routeDal.InsertWaypoints(r);
            return RedirectToAction("Index", "Route");
        }

        //POST: Edit Users on Route
        [HttpPost]
        public ActionResult EditUsersOnRoute(int routeID)
        {
            Route routeToEdit = routeDal.GetUsersOnRoute(new Route { RouteID = routeID });
            return View("EditUsersOnRoute", routeToEdit);
        }

        //POST: Edit, Save users on Route
        [HttpPost]
        public ActionResult UpdateUsersOnRoute(Route r)
        {
            routeDal.RemoveUsersFromRoute(r);
            routeDal.InsertUsersToRoute(r);
            return RedirectToAction("Index", "Route"); //DOES NOT DO WHAT WE WANT IT TO DO TEMPORARY PUSH FIX
        }
    }
}
