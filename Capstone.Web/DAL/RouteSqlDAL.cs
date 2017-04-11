using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class RouteSqlDAL : IRouteDAL
    {
        private const string InsertWayPointsQuery = "insert into waypoints (waypoint_position, stop_time, route_id) values(@wayPoint, @stopTime, @routeId)";
        private const string InsertRouteQuery = "insert into routes values(@routeName, @isPrivate);SELECT CAST(scope_identity() AS int)";
        private const string GetWaypointsQuery = "select waypoint_position from waypoints where route_id = @routeId";
        private const string GetAllRoutesQuery = "select * from routes";
        private const string GetRouteName = "select route_name from routes where route_id = @routeId";
        private const string RemoveAllWaypointsFromRouteQuery = "DELETE FROM waypoints WHERE route_Id = ";


        private string connectionString;

        public RouteSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Route GetRoute(Route r)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(GetRouteName, connection);
                    command.Parameters.AddWithValue("@routeId", r.RouteID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        r.RouteName = Convert.ToString(reader["route_name"]);
                    }
                    reader.Close();

                    command = new SqlCommand(GetWaypointsQuery, connection);
                    command.Parameters.AddWithValue("@routeId", r.RouteID);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        r.AddWaypoint(Convert.ToString(reader["waypoint_position"]));
                    }

                    return r;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool InsertRoute(Route r)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(InsertRouteQuery, connection);
                    command.Parameters.AddWithValue("@routeName", r.RouteName);
                    command.Parameters.AddWithValue("@isPrivate", r.IsPrivate);
                    int id = (int)command.ExecuteScalar();

                    int rowsAffected = 0;

                    for (int i = 0; i < r.Waypoints.Count; i++)
                    {
                        if (r.Waypoints[i] != null && r.Waypoints[i] != "")
                        {
                            var stopTime = r.Times[i];

                            command = new SqlCommand(InsertWayPointsQuery, connection);
                            command.Parameters.AddWithValue("@wayPoint", r.Waypoints[i]);
                            command.Parameters.AddWithValue("@routeId", id);
                            command.Parameters.AddWithValue("@stopTime", stopTime);
                            rowsAffected += command.ExecuteNonQuery();
                        }
                    }
                    //foreach (string wayPoint in r.Waypoints)
                    //{
                    //    if (wayPoint != null && wayPoint != "")
                    //    {
                            
                    //        var stopTime = r.Times[] 

                    //        command = new SqlCommand(InsertWayPointsQuery, connection);
                    //        command.Parameters.AddWithValue("@wayPoint", wayPoint);
                    //        command.Parameters.AddWithValue("@routeId", id);
                    //        command.Parameters.AddWithValue("@stopTime", stopTime);
                    //        rowsAffected += command.ExecuteNonQuery();
                    //    }
                    //}
                    return rowsAffected == r.Waypoints.Count();
                }
            }
            catch (SqlException e)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Route> GetAllRoutes()
        {
            try
            {
                List<Route> routes = new List<Route>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(GetAllRoutesQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Route r = new Route();
                        r.RouteName = Convert.ToString(reader["route_name"]);
                        r.RouteID = Convert.ToInt32(reader["route_id"]);

                        routes.Add(r);
                    }
                    return routes;
                }
            }
            catch (SqlException e)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveRoute(Route r)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string delete = RemoveAllWaypointsFromRouteQuery + r.RouteID;
                    connection.Open();

                    SqlCommand command = new SqlCommand(delete, connection);

                    int result = command.ExecuteNonQuery();

                }
            }
            catch (SqlException e)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool InsertWaypoints(Route r)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int rowsAffected = 0;

                    foreach (string wayPoint in r.Waypoints)
                    {
                        if (wayPoint != null && wayPoint != "")
                        {
                            SqlCommand command = new SqlCommand(InsertWayPointsQuery, connection);
                            command.Parameters.AddWithValue("@wayPoint", wayPoint);
                            command.Parameters.AddWithValue("@routeId", r.RouteID);

                            rowsAffected += command.ExecuteNonQuery();
                        }
                    }

                    return rowsAffected == r.Waypoints.Count();
                }
            }
            catch (SqlException e)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}