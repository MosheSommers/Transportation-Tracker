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
        private const string ChangePrivacyQuery = "UPDATE routes SET is_Private = @isPrivate WHERE route_id = @routeId";
        private const string GetWaypointsQuery = "select waypoint_position, stop_time from waypoints where route_id = @routeId";
        private const string GetAllRoutesQuery = "select * from routes";
        private const string GetRouteName = "select route_name from routes where route_id = @routeId";
        private const string RemoveAllWaypointsFromRouteQuery = "DELETE FROM waypoints WHERE route_Id = ";
        private const string GetUsersQuery = "SELECT email_address FROM private_route_users WHERE route_id = @routeId";
        private const string InsertUserQuery = "INSERT INTO private_route_users (route_id, email_address) VALUES (@routeId, @emailAddress)";
        private const string RemoveUserQuery = "DELETE FROM private_route_users WHERE route_Id = @routeId AND email_address = @emailAddress";
        private const string GetPrivateRoutesForUser = "SELECT route_id FROM private_route_users WHERE email_address = @emailAddress";

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
                        r.AddTime(Convert.ToDateTime(reader["stop_time"]));

                    }
                    reader.Close();

                    command = new SqlCommand(GetUsersQuery, connection);
                    command.Parameters.AddWithValue("routeId", r.RouteID);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        r.AddUser(Convert.ToString(reader["email_address"]));
                    }


                    return r;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public void RemoveWaypointsFromRoute(Route r)
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

                    SqlCommand command = new SqlCommand(ChangePrivacyQuery, connection);
                    command.Parameters.AddWithValue("@routeId", r.RouteID);
                    command.Parameters.AddWithValue("@isPrivate", r.IsPrivate);

                    int rowsAffected = 0;

                    for (int i = 0; i < r.Waypoints.Count; i++)
                    {
                        if (r.Waypoints[i] != null && r.Waypoints[i] != "")
                        {
                            var stopTime = r.Times[i];

                            command = new SqlCommand(InsertWayPointsQuery, connection);
                            command.Parameters.AddWithValue("@wayPoint", r.Waypoints[i]);
                            command.Parameters.AddWithValue("@routeId", r.RouteID);
                            command.Parameters.AddWithValue("@stopTime", stopTime);
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

        public Route GetUsersOnRoute(Route r)
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

                    command = new SqlCommand(GetUsersQuery, connection);
                    command.Parameters.AddWithValue("@routeId", r.RouteID);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        r.AddUser(Convert.ToString(reader["email_address"]));
                    }

                    return r;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertUserToRoute(string User, int RouteID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int rowsAffected = 0;

                    if (User != null && User != "")
                    {
                        SqlCommand command = new SqlCommand(InsertUserQuery, connection);
                        command.Parameters.AddWithValue("@routeId", RouteID);
                        command.Parameters.AddWithValue("@emailAddress", User);

                        rowsAffected += command.ExecuteNonQuery();
                    }


                    return rowsAffected == 1;
                }
            }
            catch (Exception a)
            {
                return false;
            }
        }

        public bool RemoveUserFromRoute(string User, int RouteID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int rowsAffected = 0;

                    if (User != null && User != "")
                    {
                        SqlCommand command = new SqlCommand(RemoveUserQuery, connection);
                        command.Parameters.AddWithValue("@routeId", RouteID);
                        command.Parameters.AddWithValue("@emailAddress", User);

                        rowsAffected += command.ExecuteNonQuery();
                    }

                    return rowsAffected == 1;
                }
            }
            catch (Exception a)
            {
                return false;
            }
        }


    }
}