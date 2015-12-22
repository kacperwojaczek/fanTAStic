using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data.SqlClient;
using System.Data;

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

        public User readDb(SqlDataReader dataReader)
        {
            var user = new User();

            dataReader.Read();

            user.Id = dataReader.GetInt32(0);
            user.Firstname = dataReader.GetString(1);
            user.Lastname = dataReader.GetString(2);
            user.Login = dataReader.GetString(4);
            user.Password = dataReader.GetString(5);
            user.Avatar = dataReader.GetString(6);
            user.Bio = dataReader.GetString(7);

            dataReader.Close();

            return user; 
        }

        public User Get(UserRequest request)
        {
            User user;
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                string command = "Select * from Users where id=@Login";
                SqlDataReader dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    user = readDb(dataReader);

                    return user;
                }
                else
                {
                    dataReader.Close();
                    SqlParameter login2Param = new SqlParameter("@Login2", SqlDbType.VarChar, request.Login.Length);
                    login2Param.Value = request.Login;
                    paramsList.Clear();
                    paramsList.Add(login2Param);
                    command = "Select * from Users where Login=@Login2";
                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.HasRows)
                    {
                        user = readDb(dataReader);

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
            var user = new User();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                string command = "Select * from Users where id=@Login";
                SqlDataReader dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    user = readDb(dataReader);
                }
                else
                {
                    dataReader.Close();
                    SqlParameter login2Param = new SqlParameter("@Login2", SqlDbType.VarChar, request.Login.Length);
                    login2Param.Value = request.Login;
                    paramsList.Clear();
                    paramsList.Add(login2Param);
                    command = "Select * from Users where Login=@Login2";
                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.HasRows)
                    {
                        user = readDb(dataReader);
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

                paramsList.Clear();

                command = "UPDATE Users set Imie=@Name, Nazwisko=@Surname, Password=@Password, Avatar=@Avatar, Bio=@Bio where Id=@Id";
                
                SqlParameter tempParam = new SqlParameter("@Name", SqlDbType.VarChar, request.Firstname.Length);
                tempParam.Value = request.Firstname;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Surname", SqlDbType.VarChar, request.Lastname.Length);
                tempParam.Value = request.Lastname;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Password", SqlDbType.VarChar, request.Password.Length);
                tempParam.Value = request.Password;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Avatar", SqlDbType.VarChar, request.Avatar.Length);
                tempParam.Value = request.Avatar;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Bio", SqlDbType.VarChar, request.Bio.Length);
                tempParam.Value = request.Bio;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Id", SqlDbType.Int);
                tempParam.Value = user.Id;
                paramsList.Add(tempParam);

                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.RecordsAffected > 0)
                {
                    return user;
                }
                else
                {
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
