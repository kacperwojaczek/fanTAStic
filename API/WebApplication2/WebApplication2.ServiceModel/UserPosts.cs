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
    public class UserPostsRequest : IReturn<UserPostsResponse>
    {
        public string Login { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class UserPostsResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public UserPostErrorWrapper GetAll(UserPostsRequest request)
        {
            var retVal = new UserPostErrorWrapper();
            List<UserPost> posts = new List<UserPost>();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                command = "Select * from Users where Id=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    string authorId = dataReader.GetString(0);
                    dataReader.Close();

                    paramsList.Clear();
                    SqlParameter authorParam = new SqlParameter("@Author", SqlDbType.VarChar, authorId.Length);
                    authorParam.Value = authorId;
                    paramsList.Add(authorParam);
                    command = "Select * from Wall where AutorId=@Author";
                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            posts.Add(new UserPost
                            {
                                id = dataReader.GetString(0),
                                authorId = dataReader.GetString(1),
                                title = dataReader.GetString(2),
                                content = dataReader.GetString(3),
                                date = dataReader.GetDateTime(4)
                            });
                        }

                        dataReader.Close();

                        retVal.userPosts = posts;
                        retVal.status = HttpStatusCode.OK;
                        return retVal;
                    }
                    else
                    {
                        retVal.userPosts = null;
                        retVal.status = HttpStatusCode.NoContent;
                        return retVal;
                    }
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
                        dataReader.Read();
                        string authorId = dataReader.GetString(0);
                        dataReader.Close();

                        paramsList.Clear();
                        SqlParameter authorParam = new SqlParameter("@Author", SqlDbType.VarChar, authorId.Length);
                        authorParam.Value = authorId;
                        paramsList.Add(authorParam);
                        command = "Select * from Wall where AuthorId=@Author";
                        dataReader = dbConnection.executeCommand(command, paramsList);

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                posts.Add(new UserPost
                                {
                                    id = dataReader.GetString(0),
                                    authorId = dataReader.GetString(1),
                                    title = dataReader.GetString(2),
                                    content = dataReader.GetString(3),
                                    date = dataReader.GetDateTime(4)
                                });
                            }

                            dataReader.Close();

                            retVal.userPosts = posts;
                            retVal.status = HttpStatusCode.OK;
                            return retVal;
                        }
                        else
                        {
                            retVal.userPosts = null;
                            retVal.status = HttpStatusCode.NoContent;
                            return retVal;
                        }
                    }
                    else
                    {
                        retVal.status = HttpStatusCode.NotFound;
                        retVal.userPosts = null;
                        return retVal;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal.userPosts = null;
                retVal.status = HttpStatusCode.InternalServerError;
                return retVal;
            }
        }
        
        public UserPostErrorWrapper Post(UserPostsRequest request)
        {
            UserPostErrorWrapper retVal = new UserPostErrorWrapper();
            UserPost post = new UserPost();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                SqlParameter loginParam = new SqlParameter("@Login", SqlDbType.VarChar, request.Login.Length);
                loginParam.Value = request.Login;
                paramsList.Add(loginParam);
                command = "select Id from Users where Login=@Login";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    post.authorId = dataReader.GetString(0);
                    post.title = request.Title;
                    post.content = request.Content;
                    post.date = DateTime.Now;
                }
                else
                {
                    dataReader.Close();
                    paramsList.Clear();

                    SqlParameter loginParam2 = new SqlParameter("@Id", SqlDbType.VarChar, request.Login.Length);
                    loginParam.Value = request.Login;
                    paramsList.Add(loginParam2);
                    command = "select Id from Users where Id=@Id";
                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        post.authorId = dataReader.GetString(0);
                        post.title = request.Title;
                        post.content = request.Content;
                        post.date = DateTime.Now;
                    }
                    else
                    {
                        retVal.status = HttpStatusCode.NotFound;
                        retVal.userPosts = null;

                        return retVal;
                    }
                }

                dataReader.Close();

                post.id = System.Guid.NewGuid().ToString();

                command = "INSERT into Wall values (@Id, @AuthorId, @Title, @Content, @Date)";

                paramsList.Clear();
                SqlParameter tempParam = new SqlParameter("@Id", SqlDbType.VarChar, post.id.Length);
                tempParam.Value = post.id;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@AuthorId", SqlDbType.VarChar, post.authorId.Length);
                tempParam.Value = post.authorId;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Title", SqlDbType.VarChar, post.title.Length);
                tempParam.Value = post.title;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Content", SqlDbType.VarChar, post.content.Length);
                tempParam.Value = post.content;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@Date", SqlDbType.DateTime);
                tempParam.Value = post.date;
                paramsList.Add(tempParam);

                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.RecordsAffected > 0)
                {
                    retVal.userPost = post;
                    retVal.status = HttpStatusCode.Created;

                    return retVal;
                }
                else
                {
                    retVal.userPost = null;
                    retVal.status = HttpStatusCode.InternalServerError;
                    return retVal;
                }
            }
            catch (Exception ex)
            {
                retVal.userPost = null;
                retVal.status = HttpStatusCode.InternalServerError;
                return retVal;
            }
        }
    }

    public class UserPost
    {
        public string id { get; set; }
        public string authorId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
    }

    public class UserPostErrorWrapper
    {
        public UserPost userPost;
        public List<UserPost> userPosts;
        public HttpStatusCode status;
    }
}
