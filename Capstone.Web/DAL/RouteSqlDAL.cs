using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class RouteSqlDAL : IRouteDAL
    {
        private string connectionString;

        public RouteSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Route GetRoute(Route r)
        {
            throw new NotImplementedException();
        }

        public bool InsertRoute(Route r)
        {
            throw new NotImplementedException();
        }
    }
}