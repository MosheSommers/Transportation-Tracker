using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class UserSqlDALTests
    {/*
        TransactionScope tran;
        UserSqlDAL userDal;

        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=omnibus;" +
            "User ID=te_student;Password=sqlserver1";
        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into users values('testemail', 'password','22222', '1234567890', 1)", connection);
                command.ExecuteNonQuery();
            }
            userDal = new UserSqlDAL(connectionString);
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetUserTest()
        {
            User u = new User() { EmailAddress = "testemail" };
            u = userDal.GetUser(u);

            Assert.AreEqual("password", u.Password);

        }


        [TestMethod]
        public void InsertNewUserTest()
        {
            //UserSqlDAL userDal = new UserSqlDAL(connectionString);
            User u = new User() { EmailAddress = "moshe@tester.com", IsAdmin = true, Password = "testpass", Phone = "1234567890" , Salt = "12344"};

            Assert.IsTrue(userDal.InsertNewUser(u));
        }*/
    }
}
