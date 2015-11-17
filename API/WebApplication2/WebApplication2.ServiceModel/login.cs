using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data.SqlClient;

namespace WebApplication2.ServiceModel
{
    [Route("/login", "GET")]
    [Route("/login/{Login}", "GET")]
    public class loginRequest : IReturn<loginResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
    public class loginResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public string Session(loginRequest request)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Server=tcp:o2elk70fp8.database.windows.net,1433;Database=ipsumeAHtTXn574L;User ID=fantastic@o2elk70fp8;Password=nhm554WW;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                //Console.WriteLine("Connection Open ! ");
                string command=String.Format("Select Haslo from Users where NazwaBloga={0}", request.Login);
                SqlCommand polecenie = new SqlCommand(command, cnn);
                SqlDataReader dataReader;
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    if (String.Compare(dataReader.GetString(0), request.Password) == 1)
                    {
                        dataReader.Close();
                        cnn.Close();
                        return String.Format("Zalogowales sie, {0}!", request.Login);
                    }
                    else
                    {
                        dataReader.Close();
                        cnn.Close();
                        return String.Format("Niepoprawne hasło!");
                    }
                }
                else
                {
                    dataReader.Close();
                    cnn.Close();
                    return String.Format("W bazie danych nie ma użytkownika o nazwie {0}!", request.Login); 
                }
            }
            catch (Exception ex)
            {
                return String.Format("Błąd ładowania bazy");
            }

        }
    }
}
