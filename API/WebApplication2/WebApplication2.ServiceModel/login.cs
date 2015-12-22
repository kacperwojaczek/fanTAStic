using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data.SqlClient;

namespace WebApplication2.ServiceModel
{
    [Route("/login", "POST")]
    [Route("/login/{Login}", "POST")]
    public class loginRequest : IReturn<loginResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
    public class loginResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public int Session(loginRequest request)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Server=tcp:fantastic.database.windows.net,1433;Database=fantastic;User ID=qacpiweb@fantastic;Password=nhm554WW;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            cnn = new SqlConnection(connetionString);
            try
            {
                
                //Console.WriteLine("Connection Open ! ");
                string command = String.Format("Select Password from Users where Login='{0}'", request.Login);
                SqlCommand polecenie = new SqlCommand(command, cnn);
                SqlDataReader dataReader;
                cnn.Open();
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    string password = dataReader.GetString(0);
                    if (password == request.Password)
                    {
                        dataReader.Close();
                        cnn.Close();
                        return 200;
                    }
                    else
                    {
                        dataReader.Close();
                        cnn.Close();
                        return 401;
                    }
                }
                else
                {
                    dataReader.Close();
                    cnn.Close();
                    return 404;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }

        }
    }
}
