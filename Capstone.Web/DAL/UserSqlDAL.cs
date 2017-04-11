using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class UserSqlDAL : IUserDAL
    {
        private const string GetUserQuery = "select * from users where email_address = @email";
        private const string InsertUserQuery = "insert into users (email_address, password, salt, phone_number, is_admin) values(@email, @password, @salt, @phoneNumber, 0)";
        private const string InsertUserWithoutPhoneQuery = "insert into users (email_address, password, salt, is_admin) values(@email, @password, @salt, 0)";

        private string connectionString;

        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User GetUser(User u)
        {
            try
            {
                User returnedUser = new User();
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(GetUserQuery, connection);
                    command.Parameters.AddWithValue("@email", u.EmailAddress);

                    SqlDataReader reader  = command.ExecuteReader();
                    if (reader.Read())
                    {
                        returnedUser.EmailAddress = Convert.ToString(reader["email_address"]);                       
                        returnedUser.Password = Convert.ToString(reader["password"]);
                        returnedUser.Salt = Convert.ToString(reader["salt"]);
                        returnedUser.IsAdmin = Convert.ToBoolean(reader["is_admin"]);
                        //returnedUser.UserID = Convert.ToInt32(reader["user_id"]);

                        if (reader["phone_number"] != DBNull.Value)
                        {
                            returnedUser.Phone = Convert.ToString(reader["phone_number"]);
                        }

                    }
                    return returnedUser;
                }
            }
            catch (SqlException e)
            {
                throw;
                return null;
            }
            catch (Exception)
            {
                throw;
                return null;
            }
        }

        public bool InsertNewUser(User u)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;

                    if (u.Phone != null && u.Phone != "")
                    {
                        command = new SqlCommand(InsertUserQuery, connection);
                        command.Parameters.AddWithValue("@phoneNumber", u.Phone);
                    }
                    else
                    {
                        command = new SqlCommand(InsertUserWithoutPhoneQuery, connection);
                    }
                    
                    command.Parameters.AddWithValue("@email", u.EmailAddress);
                    command.Parameters.AddWithValue("@password", u.Password);
                    command.Parameters.AddWithValue("@salt", u.Salt); 
                    command.Parameters.AddWithValue("@isAdmin", false);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}