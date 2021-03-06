﻿using System;
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
    {/*
        TransactionScope tran;
        RouteSqlDAL routeDAL;

        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=omnibus;" +
            "User ID=te_student;Password=sqlserver1";
        int id = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into routes values('testRoute');SELECT CAST(scope_identity() AS int)", connection);
                id = (int)command.ExecuteScalar();

                command = new SqlCommand("insert into waypoints values('waypoint1'," + id + ")", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("insert into waypoints values ('waypoint2'," + id + ")", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("insert into waypoints values('waypoint3'," + id + ")", connection);
                command.ExecuteNonQuery();
            }
            routeDAL = new RouteSqlDAL(connectionString);
            
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetRouteTest()
        {
            Route r = routeDAL.GetRoute(new Route() { RouteName = "test route", RouteID = id });
            Assert.AreEqual(3, r.Waypoints.Count);
        }

        [TestMethod]
        public void InsertRouteTest()
        {

            Route r = new Route() { RouteName = "test route", Waypoints = new List<string>() { "wayPoint 1", "waypoint 2", "waypoint 3" } };
            Assert.IsTrue(routeDAL.InsertRoute(r));
            Assert.AreEqual("TEST ROUTE", r.RouteName);
        }*/
    }
}
