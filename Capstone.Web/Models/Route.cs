using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Route
    {
        
        private string routeName;

        public string RouteName
        {
            get { return routeName; }
            set { routeName = value.ToUpper(); }
        }

        public int RouteID { get; set; }
        public int PrivateNumber { get; set; }
        public string CreatedBy { get; set; }
        public bool IsPrivate { get; set; }

        public List<string> Waypoints { get; set; } = new List<string>();

        public void AddWaypoint(string newWaypoint)
        {
            this.Waypoints.Add(newWaypoint);
        }
    }
}