using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data.SqlClient;

namespace WebApplication2.ServiceModel
{
    [Route("/users/", "GET")]
    [Route("/users/{Login}", "GET")]
    [Route("/users/{Login}", "PATCH")]
    public class UserRequest : IReturn<UserResponse>
    {
        public string Login { get; set; }
    }

    public class UserResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public User Respond(UserRequest request)
        {
            User user = new User();
            string connectionString = null;
            SqlConnection cnn;
            connectionString = "workstation id=fantastic.mssql.somee.com;packet size=4096;user id=qacpiweb_SQLLogin_1;pwd=cfzqlqsobm;data source=fantastic.mssql.somee.com;persist security info=False;initial catalog=fantastic";
            cnn = new SqlConnection(connectionString);
            try
            {
                string command = String.Format("Select * from Users where Login='{0}'", request.Login);
                SqlCommand polecenie = new SqlCommand(command, cnn);
                SqlDataReader dataReader;
                cnn.Open();
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    user.Firstname = dataReader.GetString(1);
                    user.Lastname = dataReader.GetString(2);
                    user.Login = dataReader.GetString(4);
                    user.Password = dataReader.GetString(5);

                    dataReader.Close();
                    cnn.Close();
                    return user;
                }
                else
                {
                    dataReader.Close();
                    cnn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

    public class User
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
