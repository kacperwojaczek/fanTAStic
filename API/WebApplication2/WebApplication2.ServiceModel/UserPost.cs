using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.ServiceModel
{
    [Route("/posts/{Id}", "GET")]
    [Route("/posts/{Id}", "PATCH")]
    public class UserPostRequest : IReturn<UserPostResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class UserPostResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public List<UserPost> Get(UserPostRequest request)
        {
            var userPosts = new List<UserPost>();
            UserPost post = new UserPost();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                if (request.Id == 0)
                {
                    command = "Select * from Wall";
                    dataReader = dbConnection.executeCommand(command, paramsList);
                    
                    while(dataReader.Read())
                    {
                        userPosts.Add(new UserPost
                        {
                            id = dataReader.GetInt32(0),
                            authorId = dataReader.GetInt32(1),
                            title = dataReader.GetString(2),
                            reblog = dataReader.GetInt32(3),
                            text = dataReader.GetString(4),
                            attachment = dataReader.GetString(6),
                            date = dataReader.GetDateTime(7)
                        });
                    }
                    dataReader.Close();

                    return userPosts;
                }
                else
                {
                    paramsList.Clear();

                    SqlParameter loginParam = new SqlParameter("@Id", SqlDbType.Int);
                    loginParam.Value = request.Id;
                    paramsList.Add(loginParam);
                    command = "Select * from Wall where id=@Id";
                    dataReader = dbConnection.executeCommand(command, paramsList);
                    
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        userPosts.Add(new UserPost
                        {
                            id = dataReader.GetInt32(0),
                            authorId = dataReader.GetInt32(1),
                            title = dataReader.GetString(2),
                            reblog = dataReader.GetInt32(3),
                            text = dataReader.GetString(4),
                            attachment = dataReader.GetString(6),
                            date = dataReader.GetDateTime(7)
                        });

                        dataReader.Close();

                        return userPosts;
                    }
                    else
                    {
                        dataReader.Close();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<UserPost> Patch(UserPostRequest request)
        {
            var userPosts = new List<UserPost>();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                SqlParameter loginParam = new SqlParameter("@Id", SqlDbType.Int);
                loginParam.Value = request.Id;
                paramsList.Add(loginParam);
                command = "Select * from Wall where id=@Id";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    userPosts.Add(new UserPost
                    {
                        id = dataReader.GetInt32(0),
                        authorId = dataReader.GetInt32(1),
                        title = dataReader.GetString(2),
                        reblog = dataReader.GetInt32(3),
                        text = dataReader.GetString(4),
                        attachment = dataReader.GetString(6),
                        date = dataReader.GetDateTime(7)
                    });

                    dataReader.Close();

                    //update post

                    userPosts.ElementAt(0).title = request.Title;
                    userPosts.ElementAt(0).text = request.Content;

                    command = "UPDATE Wall set Tytul=@Title, Tresc=@Content where id=@IId";
                    paramsList.Clear();

                    SqlParameter tempParam = new SqlParameter("@Title", SqlDbType.VarChar, request.Title.Length);
                    tempParam.Value = request.Title;
                    paramsList.Add(tempParam);
                    tempParam = new SqlParameter("@Content", SqlDbType.VarChar, request.Content.Length);
                    tempParam.Value = request.Content;
                    paramsList.Add(tempParam);
                    tempParam = new SqlParameter("@IId", SqlDbType.Int);
                    tempParam.Value = request.Id;
                    paramsList.Add(tempParam);

                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.RecordsAffected > 0)
                    {
                        dataReader.Close();
                        return userPosts;
                    }
                    else
                    {
                        dataReader.Close();
                        return null;
                    }

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
    }

    public class UserPost
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public string title { get; set; }
        public int reblog { get; set; }
        public string text { get; set; }
        public int tags { get; set; }
        public string attachment { get; set; }
        public DateTime date { get; set; }

    }
}
