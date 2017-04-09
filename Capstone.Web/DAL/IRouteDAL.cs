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

        void RemoveRoute(Route r);

        bool InsertWaypoints(Route r);
    }
}
