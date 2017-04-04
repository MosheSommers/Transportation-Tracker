using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class RouteController : Controller
    {
        // GET: Route
        public ActionResult Index()
        {
            return View();
        }


        // GET: Create
        public ActionResult Create()
        {
            Route newRoute = new Route();
            newRoute.AddWaypoint("Bacon");
            return View(newRoute);
        }
    }
}