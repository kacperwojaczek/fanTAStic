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
    [Route("/users/{Login}/posts", "GET")]
    [Route("/users/{Login}/posts", "POST")]
    public class UserPostsRequest : IReturn<UserPostResponse>
    {
        public string Login { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class UserPostsResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public List<Int32> Get(UserPostsRequest request)
        {
            List<Int32> posts = new List<Int32>();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                command = "Select * from Users where Login=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    Int32 authorId = dataReader.GetInt32(0);
                    dataReader.Close();

                    paramsList.Clear();
                    SqlParameter authorParam = new SqlParameter("@Author", SqlDbType.Int);
                    authorParam.Value = authorId;
                    paramsList.Add(authorParam);
                    command = "Select id from Wall where Autor=@Author";
                    dataReader = dbConnection.executeCommand(command, paramsList);

                    while (dataReader.Read())
                    {
                        posts.Add(dataReader.GetInt32(0));
                    }

                    return posts;
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

        public int Post(UserPostsRequest request)
        {
            UserPost post = new UserPost();
            List<Int32> posts = new List<Int32>();
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

                dataReader.Read();

                post.authorId = dataReader.GetInt32(0);
                post.title = request.Title;
                post.text = request.Content;
                post.tags = 0;
                post.reblog = 0;
                post.attachment = "empty";

                dataReader.Close();

                command = "INSERT into Wall (Autor, Tytul, reblog, Tresc, Tagi, attachment, DataPosta) values (@AuthorId, @Title, @Reblog, @Text, @Tags, @Attach, GETDATE())";
                // post.authorId, post.title, post.reblog, post.text, post.tags, post.attachment);

                paramsList.Clear();
                SqlParameter tempParam = new SqlParameter("@AuthorId", SqlDbType.Int);
                tempParam.Value = post.authorId;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Title", SqlDbType.VarChar, post.title.Length);
                tempParam.Value = post.title;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Reblog", SqlDbType.Int, post.reblog);
                tempParam.Value = post.reblog;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Text", SqlDbType.VarChar, post.text.Length);
                tempParam.Value = post.text;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Tags", SqlDbType.Binary);
                tempParam.Value = BitConverter.GetBytes(post.tags);
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Attach", SqlDbType.VarChar, post.attachment.Length);
                tempParam.Value = post.attachment;
                paramsList.Add(tempParam);

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
    }
}
