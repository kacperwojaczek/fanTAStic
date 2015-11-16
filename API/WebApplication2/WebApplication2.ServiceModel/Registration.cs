using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data.SqlClient;

namespace WebApplication2.ServiceModel
{
    [Route("/register/{Login}", "POST")]
    public class RegistrationRequest : IReturn<RegistrationResponse>
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }

    public class RegistrationResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public string Session(RegistrationRequest request)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                //Console.WriteLine("Connection Open ! ");
                string register = String.Format("SELECT * from Users where NazwaBloga={0}", request.Login);
                SqlCommand polecenie = new SqlCommand(register, cnn);
                SqlDataReader dataReader;
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Close();
                    cnn.Close();
                    return String.Format("Istnieje już osoba o loginie {0}!", request.Login);
                }
                else
                {
                    string newtable = String.Format("CREATE TABLE TablicaWpisow_{0} (id int not null primary key, Tytul varchar(75), autor int, Tresc text, Tagi binary(500) attachment varchar(150))", request.Login);
                    string command = String.Format("INSERT into Users (ImieBloggera, NazwiskoBloggera, NazwaBloga, Haslo, EmailBloggera) values ({0}, {1}, {2}, {3}, {4})", request.Firstname, request.Lastname, request.Login, request.Password, request.Email);
                    polecenie = new SqlCommand(newtable, cnn);
                    polecenie = new SqlCommand(command, cnn);
                    dataReader.Close();
                    cnn.Close();
                    return String.Format("Dziękujemy Ci za rejestrację {0}!", request.Login); 
                }
            }
            catch (Exception ex)
            {
                return String.Format("Błąd ładowania bazy");
            }
        }
    }

}