using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data.SqlClient;

namespace WebApplication2.ServiceModel
{
    [Route("/users/{Login}", "GET")]
    [Route("/users/{Login}", "PATCH")]
    public class UserRequest : IReturn<UserResponse>
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
    }

    public class UserResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public User Get(UserRequest request)
        {
            User user = new User();

            string connectionString = null;
            SqlConnection cnn;
            connectionString = "Server=tcp:fantastic.database.windows.net,1433;Database=fantastic;User ID=qacpiweb@fantastic;Password=nhm554WW;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            cnn = new SqlConnection(connectionString);
            try
            {
                string command;
                command = String.Format("Select * from Users where id='{0}'", request.Login);
                SqlCommand polecenie = new SqlCommand(command, cnn);
                SqlDataReader dataReader;
                cnn.Open();
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    user.Id = dataReader.GetInt32(0);
                    user.Firstname = dataReader.GetString(1);
                    user.Lastname = dataReader.GetString(2);
                    user.Login = dataReader.GetString(4);
                    user.Password = dataReader.GetString(5);
                    user.Avatar = dataReader.GetString(6);
                    user.Bio = dataReader.GetString(7);

                    dataReader.Close();
                    cnn.Close();
                    return user;
                }
                else
                {
                    dataReader.Close();
                    command = String.Format("Select * from Users where Login='{0}'", request.Login);
                    polecenie = new SqlCommand(command, cnn);
                    dataReader = polecenie.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        user.Id = dataReader.GetInt32(0);
                        user.Firstname = dataReader.GetString(1);
                        user.Lastname = dataReader.GetString(2);
                        user.Login = dataReader.GetString(4);
                        user.Password = dataReader.GetString(5);
                        user.Avatar = dataReader.GetString(6);
                        user.Bio = dataReader.GetString(7);

                        dataReader.Close();
                        cnn.Close();
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public User Patch(UserRequest request)
        {
            User user = new User();

            string connectionString = null;
            SqlConnection cnn;
            connectionString = "Server=tcp:fantastic.database.windows.net,1433;Database=fantastic;User ID=qacpiweb@fantastic;Password=nhm554WW;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            cnn = new SqlConnection(connectionString);
            try
            {
                string command;
                command = String.Format("Select * from Users where id='{0}'", request.Login);
                SqlCommand polecenie = new SqlCommand(command, cnn);
                SqlDataReader dataReader;
                cnn.Open();
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    user.Id = dataReader.GetInt32(0);
                    user.Firstname = dataReader.GetString(1);
                    user.Lastname = dataReader.GetString(2);
                    user.Login = dataReader.GetString(4);
                    user.Password = dataReader.GetString(5);
                    user.Avatar = dataReader.GetString(6);
                    user.Bio = dataReader.GetString(7);

                    dataReader.Close();
                }
                else
                {
                    dataReader.Close();
                    command = String.Format("Select * from Users where Login='{0}'", request.Login);
                    polecenie = new SqlCommand(command, cnn);
                    dataReader = polecenie.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        user.Id = dataReader.GetInt32(0);
                        user.Firstname = dataReader.GetString(1);
                        user.Lastname = dataReader.GetString(2);
                        user.Login = dataReader.GetString(4);
                        user.Password = dataReader.GetString(5);
                        user.Avatar = dataReader.GetString(6);
                        user.Bio = dataReader.GetString(7);

                        dataReader.Close();
                    }
                    else
                    {
                        return null;
                    }
                }

                //update user

                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.Password = request.Password;
                user.Avatar = request.Avatar;
                user.Bio = request.Bio;

                command = String.Format("UPDATE Users set Imie='{0}', Nazwisko='{1}', Password='{2}', Avatar='{3}', Bio='{4}' where Id='{5}' ", request.Firstname, request.Lastname, request.Password, request.Avatar, request.Bio, user.Id);
                polecenie = new SqlCommand(command, cnn);
                dataReader = polecenie.ExecuteReader();

                if (dataReader.RecordsAffected > 0)
                {
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
        public int Id { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
    }
