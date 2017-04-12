using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ApiController : Controller
    {
        // POST: Api
        [HttpPost]
        public ActionResult UpdateLocation(double longitude, double latitude, int routeId)
        {
            // Notify connected clients about update to bus location
            var websocketContext = GlobalHost.ConnectionManager.GetConnectionContext<TransportationConnection>();

            var update = new
            {
                routeId = routeId,
                latitude = latitude,
                longitude = longitude
            };

            websocketContext.Connection.Broadcast(update);

            return Json(true);
        }
    }
}