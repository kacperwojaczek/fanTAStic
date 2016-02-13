using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication2.ServiceModel
{
    [Route("/users/{Login}/friends", "GET")]
    [Route("/users/{Login}/friends", "POST")]
    public class FriendRequest : IReturn<FriendResponse>
    {
        // co dostaje od php w formacie jsona
        public string Login { get; set; }
        public int Friend { get; set; }

    }

    public class FriendResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public List<Int32> Get(FriendRequest request)
        {
            List<Int32> friends = new List<Int32>();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                // sprawdzanie czy uzytkownik istnieje
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                command = "Select * from Users where Login=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                // jesli jest taki uzytkownik, to szukamy jego znajomych
                if (dataReader.HasRows)
                {
                    // zczytujemy id uzytkownika ktorego przyjaciol wyszukujemy
                    dataReader.Read();
                    Int32 id = dataReader.GetInt32(0);
                    dataReader.Close();

                    // szukamy jego/jej przyjaciół
                    paramsList.Clear();
                    SqlParameter authorParam = new SqlParameter("@id", SqlDbType.Int);
                    authorParam.Value = id;
                    paramsList.Add(authorParam);
                    command = "Select Friend from Friends where User1=@id";
                    dataReader = dbConnection.executeCommand(command, paramsList);

                    // pobieranie przyjaciół
                    while (dataReader.Read())
                    {
                        friends.Add(dataReader.GetInt32(0));
                    }
                    return friends;
                }
                else
                {
                    dataReader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public int FriendPost(FriendRequest request)
        {
            FriendshipIsMagic post = new FriendshipIsMagic();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                command = "select id from Users where Login=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                dataReader.Read(); //id naszego usera

                post.User1 = dataReader.GetInt32(0);
                post.Friend = request.Friend;

                dataReader.Close();

                paramsList.Clear();
                SqlParameter tempParam = new SqlParameter("@User1", SqlDbType.Int);
                tempParam.Value = post.User1;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Friend", SqlDbType.Int);
                tempParam.Value = post.Friend;
                paramsList.Add(tempParam);

                command = "INSERT into Friends (User1, Friend) values (@User1, @Friend)";

                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.RecordsAffected > 0)
                {
                    return (int)HttpStatusCode.OK;
                }
                else
                {
                    return (int)HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    };

    public class FriendshipIsMagic
    {
        public int User1 { get; set; }
        public int Friend { get; set; }
    }
}
