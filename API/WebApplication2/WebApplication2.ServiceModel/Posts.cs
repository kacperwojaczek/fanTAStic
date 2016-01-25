using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.ServiceModel
{
    [Route("/posts/", "GET")]
    [Route("/posts/{Id}", "GET")]
    [Route("/posts/{Id}", "PUT")]
    public class PostsRequest : IReturn<PostResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class PostResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public string Result { get; set; }
        public UserPostErrorWrapper Get(PostsRequest request)
        {
            var userPost = new UserPost();
            var retVal = new UserErrorWrapper();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
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

            return null;
        }
    
    }
}
