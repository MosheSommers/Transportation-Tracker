using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Collections.Generic;

namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class RouteSqlDALTests
    {
        TransactionScope tran;
        RouteSqlDAL routeDAL;

        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=omnibus;" +
            "User ID=te_student;Password=sqlserver1";

        [TestInitialize]
        public void Initialize()
        {
            routeDAL = new RouteSqlDAL(connectionString);
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetRouteTest()
        {
            Route r = routeDAL.GetRoute(new Route() { RouteName = "test route", Waypoints = new List<string>() { "wayPoint 1", "waypoint 2", "waypoint 3" } });
            Assert.AreEqual(3, r.Waypoints.Count);
        }

        [TestMethod]
        public void InsertRouteTest()
        {

            Route r = new Route() { RouteName = "test route", Waypoints = new List<string>() { "wayPoint 1", "waypoint 2", "waypoint 3" } };
            Assert.IsTrue(routeDAL.InsertRoute(r));
        }
    }
}
