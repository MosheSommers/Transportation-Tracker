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
        private const string InsertWayPointQuery = "insert into waypoints (waypoint_position, route_id) values(@wayPoint, (select route_id from routes where route_name = @routeName))";
        private const string InsertRouoteQuery = "insert into routes values(@routeName)";
        private const string GetRouteQuery = "select waypoint_position from waypoints where route_id = (select route_id from routes where route_name = @name)";
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

                    SqlCommand command = new SqlCommand(GetRouteQuery, connection);
                    command.Parameters.AddWithValue("@name", r.RouteName);
                    SqlDataReader reader = command.ExecuteReader();

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
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(InsertRouoteQuery, connection);
                    command.Parameters.AddWithValue("@routeName", r.RouteName);
                    command.ExecuteNonQuery();

                    int rowsAffected = 0;

                    foreach (string wayPoint in r.Waypoints)
                    {
                        command = new SqlCommand(InsertWayPointQuery, connection);
                        command.Parameters.AddWithValue("@wayPoint", wayPoint);
                        command.Parameters.AddWithValue("@routeName", r.RouteName);

                        rowsAffected += command.ExecuteNonQuery();
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