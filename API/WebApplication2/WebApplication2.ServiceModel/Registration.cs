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

        public int Session(RegistrationRequest request)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "workstation id=fantastic.mssql.somee.com;packet size=4096;user id=qacpiweb_SQLLogin_1;pwd=cfzqlqsobm;data source=fantastic.mssql.somee.com;persist security info=False;initial catalog=fantastic";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                //Console.WriteLine("Connection Open ! ");
                string register = String.Format("SELECT * from Users where Login='{0}'", request.Login);
                SqlCommand polecenie = new SqlCommand(register, cnn);
                SqlDataReader dataReader;
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Close();
                    cnn.Close();
                    return 409;
                }
                else
                {
                    dataReader.Close();
                    //string newtable = String.Format("CREATE TABLE TablicaWpisow_{0} (id int not null primary key, Tytul varchar(75), autor int, Tresc text, Tagi binary(500) attachment varchar(150))", request.Login);
                    string command = String.Format("INSERT into Users (Imie, Nazwisko, NazwaBloga, Login, Password) values ('{0}', '{1}', '{2}', '{3}', '{4}')", request.Firstname, request.Lastname, request.Login, request.Login, request.Password);
                    polecenie = new SqlCommand(command, cnn);
                    dataReader = polecenie.ExecuteReader();
                    dataReader.Close();
                    cnn.Close();
                    return 201;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }
    }

}