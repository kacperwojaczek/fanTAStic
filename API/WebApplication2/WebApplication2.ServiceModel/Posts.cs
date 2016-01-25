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
    [Route("/posts/", "GET")]
    [Route("/posts/{Id}", "GET")]
    [Route("/posts/{Id}", "PUT")]
    public class PostsRequest : IReturn<PostResponse>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class PostResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public string Result { get; set; }
        public UserPostErrorWrapper Get(PostsRequest request)
        {
            UserPost userPost;
            var retVal = new UserPostErrorWrapper();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                paramsList.Clear();

                SqlParameter loginParam = new SqlParameter("@Id", SqlDbType.VarChar, request.Id.Length);
                loginParam.Value = request.Id;
                paramsList.Add(loginParam);
                command = "Select * from Wall where Id=@Id";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    userPost = new UserPost
                    {
                        id = dataReader.GetString(0),
                        authorId = dataReader.GetString(1),
                        title = dataReader.GetString(2),
                        content = dataReader.GetString(3),
                        date = dataReader.GetDateTime(4)
                    };

                    dataReader.Close();

                    retVal.userPost = userPost;
                    retVal.status = HttpStatusCode.OK;

                    return retVal;
                }
                else
                {
                    dataReader.Close();

                    retVal.userPost = null;
                    retVal.status = HttpStatusCode.NotFound;
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
        public UserPostErrorWrapper GetAll(PostsRequest request)
        {
            var retVal = new UserPostErrorWrapper();
            var userPosts = new List<UserPost>();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;


            try
            {
                command = "Select * from Wall";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        userPosts.Add(new UserPost
                            {
                                id = dataReader.GetString(0),
                                authorId = dataReader.GetString(1),
                                title = dataReader.GetString(2),
                                content = dataReader.GetString(3),
                                date = dataReader.GetDateTime(4)
                            });
                    }

                    dataReader.Close();

                    retVal.userPosts = userPosts;
                    retVal.status = HttpStatusCode.OK;
                    return retVal;
                }
                else
                {

                    dataReader.Close();

                    retVal.userPosts = null;
                    retVal.status = HttpStatusCode.NoContent;
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
        public UserPostErrorWrapper Put(PostsRequest request)
        {
            var retVal = new UserPostErrorWrapper();
            UserPost userPost;
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                SqlParameter loginParam = new SqlParameter("@Id", SqlDbType.VarChar, request.Id.Length);
                loginParam.Value = request.Id;
                paramsList.Add(loginParam);
                command = "Select * from Wall where Id=@Id";
                dataReader = dbConnection.executeCommand(command, paramsList);

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    userPost = new UserPost
                    {
                        id = dataReader.GetString(0),
                        authorId = dataReader.GetString(1),
                        title = dataReader.GetString(2),
                        content = dataReader.GetString(3),
                        date = dataReader.GetDateTime(4)
                    };
                    retVal.userPost = userPost;
                }
                else
                {
                    retVal.userPost = null;
                    retVal.status = HttpStatusCode.NotFound;
                    return retVal;
                }

                //update post

                if (!String.IsNullOrEmpty(request.Title))
                {
                    dataReader.Close();

                    command = "UPDATE Wall set Title=@Title where Id=@IId";
                    paramsList.Clear();

                    SqlParameter tempParam = new SqlParameter("@Title", SqlDbType.VarChar, request.Title.Length);
                    tempParam.Value = request.Title;
                    paramsList.Add(tempParam);
                    tempParam = new SqlParameter("@IId", SqlDbType.VarChar, request.Id.Length);
                    tempParam.Value = request.Id;
                    paramsList.Add(tempParam);

                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.RecordsAffected > 0)
                    {
                        dataReader.Close();
                        userPost.title = request.Title;
                        retVal.status = HttpStatusCode.OK;
                    }
                    else
                    {
                        dataReader.Close();
                        retVal.userPost = null;
                        retVal.status = HttpStatusCode.InternalServerError; 
                        return retVal;
                    }
                }

                if(!String.IsNullOrEmpty(request.Content))
                {
                    dataReader.Close();

                    command = "UPDATE Wall set Content=@Content where Id=@IId";
                    paramsList.Clear();

                    SqlParameter tempParam = new SqlParameter("@Content", SqlDbType.VarChar, request.Content.Length);
                    tempParam.Value = request.Content;
                    paramsList.Add(tempParam);
                    tempParam = new SqlParameter("@IId", SqlDbType.VarChar, request.Id.Length);
                    tempParam.Value = request.Id;
                    paramsList.Add(tempParam);

                    dataReader = dbConnection.executeCommand(command, paramsList);

                    if (dataReader.RecordsAffected > 0)
                    {
                        dataReader.Close();
                        userPost.content = request.Content;
                        retVal.status = HttpStatusCode.OK;
                    }
                    else
                    {
                        dataReader.Close();
                        retVal.userPost = null;
                        retVal.status = HttpStatusCode.InternalServerError; 
                        return retVal;
                    }
                }

                retVal.userPost.date = DateTime.Now;

                return retVal;

            }
            catch (Exception ex)
            {
                retVal.userPost = null;
                retVal.status = HttpStatusCode.InternalServerError;
                return retVal;
            }
        }
    }
}
