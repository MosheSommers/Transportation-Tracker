using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IRouteDAL
    {
        bool InsertRoute(Route r);

        Route GetRoute(Route r);

        List<Route> GetAllRoutes();

        void RemoveWaypointsFromRoute(Route r);
        bool InsertWaypoints(Route r);

        Route GetUsersOnRoute(Route r);
        bool InsertUsersToRoute(Route r);
        void RemoveUsersFromRoute(Route r);
    }
}
