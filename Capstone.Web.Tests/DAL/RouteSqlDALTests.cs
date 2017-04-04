using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.Web.DAL;
using Capstone.Web.Models;

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
            Route r = routeDAL.GetRoute(new Route());
            Assert.AreEqual("East 55th", r.RouteName);
        }

        [TestMethod]
        public void InsertRouteTest()
        {
            Assert.IsTrue(routeDAL.InsertRoute(new Route()));
        }
    }
}
