using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace WebApplication2.ServiceModel
{
    [Route("/login", "POST")]
    public class loginRequest : IReturn<loginResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
    public class loginResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public HttpStatusCode Post(loginRequest request)
        {
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();

            SqlParameter loginParam;
            string command;
            SqlDataReader dataReader;

            try
            {
                //check if user exists

                loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Clear();
                paramsList.Add(loginParam);
                command = "Select Password from Users where Login=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    string password = dataReader.GetString(0);

                    dataReader.Close();

                    if(password == request.Password)
                    {
                        return HttpStatusCode.OK;
                    }
                    else
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }

            }
            catch (SqlException ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
