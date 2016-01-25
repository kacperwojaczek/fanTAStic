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
    [Route("/users/", "GET")]
    [Route("/users/{Login}", "GET")]
    [Route("/users/{Login}", "PATCH")]
    [Route("/users/{Login}", "POST")]
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

            user.Id = dataReader.GetString(0);
            user.Firstname = dataReader.GetString(1);
            user.Lastname = dataReader.GetString(2);
            user.Login = dataReader.GetString(3);
            user.Password = dataReader.GetString(4);
            user.Avatar = dataReader.GetString(5);
            user.Bio = dataReader.GetString(6);

            dataReader.Close();

            return user; 
        }

        public List<User> GetAll(UserRequest request)
        {
            List<User> usersList = new List<User>();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();

            try
            {
                string command = "Select * from Users";
                SqlDataReader dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        usersList.Add(new User
                        {
                            Id = dataReader.GetString(0),
                            Firstname = dataReader.GetString(1),
                            Lastname = dataReader.GetString(2),
                            Login = dataReader.GetString(3),
                            Password = dataReader.GetString(4),
                            Avatar = dataReader.GetString(5),
                            Bio = dataReader.GetString(6)
                        });
                    }
                    dataReader.Close();
                    return usersList;
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

        public UserErrorWrapper Get(UserRequest request)
        {
            UserErrorWrapper retVal = new UserErrorWrapper();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                string command = "Select * from Users where Id=@Login";
                SqlDataReader dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    retVal.user = readDb(dataReader);
                    retVal.status = HttpStatusCode.OK;

                    return retVal;
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
                        retVal.user = readDb(dataReader);
                        retVal.status = HttpStatusCode.OK;

                        return retVal;
                    }
                    else
                    {
                        retVal.user = null;
                        retVal.status = HttpStatusCode.NotFound;

                        return retVal;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal.user = null;
                retVal.status = HttpStatusCode.InternalServerError;

                return retVal;
            }

        }

        public UserErrorWrapper Patch(UserRequest request)
        {
            var retVal = new UserErrorWrapper();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();

            var response = new UserResponse();

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                string command = "Select * from Users where id=@Login";
                SqlDataReader dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    retVal.user = readDb(dataReader);
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
                        retVal.user = readDb(dataReader);
                    }
                    else
                    {
                        retVal.user = null;
                        retVal.status = HttpStatusCode.NotFound;
                    }
                }

                //update user

                retVal.user.Firstname = request.Firstname;
                retVal.user.Lastname = request.Lastname;
                retVal.user.Password = request.Password;
                retVal.user.Avatar = request.Avatar;
                retVal.user.Bio = request.Bio;

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
                tempParam = new SqlParameter("@Id", SqlDbType.VarChar, retVal.user.Id.Length);
                tempParam.Value = retVal.user.Id;
                paramsList.Add(tempParam);

                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.RecordsAffected > 0)
                {
                    retVal.status = HttpStatusCode.OK;

                    return retVal;
                }
                else
                {
                    retVal.user = null;
                    retVal.status = HttpStatusCode.InternalServerError;
                    return retVal;
                }
            }
            catch (Exception ex)
            {
                retVal.user = null;
                retVal.status = HttpStatusCode.InternalServerError;
                return retVal;
            }

        }

        public UserErrorWrapper Post(UserRequest request)
        {
            var retVal = new UserErrorWrapper();
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
                command = "Select * from Users where Login=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    retVal.user = null;
                    retVal.status = HttpStatusCode.Conflict;
                    return retVal;
                }

                dataReader.Close();

                //create user

                retVal.user.Id = System.Guid.NewGuid().ToString();
                retVal.user.Login = request.Login;
                retVal.user.Firstname = request.Firstname;
                retVal.user.Lastname = request.Lastname;
                retVal.user.Password = request.Password;
                retVal.user.Avatar = request.Avatar;
                retVal.user.Bio = request.Bio;

                paramsList.Clear();

                command = "INSERT into Users values (@Id, @Name, @Surname, @Login, @Password, @Avatar, @Bio)";

                SqlParameter tempParam = new SqlParameter("@Id", SqlDbType.VarChar, retVal.user.Id.Length);
                tempParam.Value = retVal.user.Id;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Name", SqlDbType.VarChar, request.Firstname.Length);
                tempParam.Value = request.Firstname;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Surname", SqlDbType.VarChar, request.Lastname.Length);
                tempParam.Value = request.Lastname;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                tempParam.Value = request.Login;
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

                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.RecordsAffected > 0)
                {
                    retVal.status = HttpStatusCode.Created;
                    return retVal;
                }
                else
                {
                    retVal.status = HttpStatusCode.InternalServerError;
                    retVal.user = null;
                    return retVal;
                }
            }
            catch (SqlException ex)
            {
                retVal.status = HttpStatusCode.InternalServerError;
                retVal.user = null;
                return retVal;
            }
        }

        public UserErrorWrapper Delete(UserRequest request)
        {
            var retVal = new UserErrorWrapper();
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
                command = "Select * from Users where Login=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    retVal.user = readDb(dataReader);

                    dataReader.Close();
                    
                    //check if user has any posts

                    paramsList.Clear();

                    command = "DELETE from Users where Login=@Login";

                    SqlParameter tempParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                    tempParam.Value = request.Login;
                    paramsList.Add(tempParam);

                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.RecordsAffected > 0)
                    {
                        retVal.status = HttpStatusCode.OK;

                        return retVal;
                    }
                    else
                    {
                        retVal.user = null;
                        retVal.status = HttpStatusCode.InternalServerError;
                        return retVal;
                    }

                }
                else
                {
                    retVal.user = null;
                    retVal.status = HttpStatusCode.NotFound;
                    return retVal;
                }
            }
            catch (SqlException ex)
            {

                retVal.user = null;
                retVal.status = HttpStatusCode.InternalServerError;
                return retVal;
            }
        }
    }
}

    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
    }

    public class UserErrorWrapper
    {
        public User user { get; set; }
        public HttpStatusCode status { get; set; }
    }
