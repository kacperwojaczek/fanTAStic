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
    [Route("/share", "POST")]
    public class SharePostsRequest : IReturn<SharePostResponse>
    {
        public int Id { get; set; }
        public string Login { get; set; }
    }

    public class SharePostResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public int SharePost(SharePostsRequest request)
        {
            SharedPost post = new SharedPost();
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

                post.authorId = dataReader.GetInt32(0);
                post.id = request.Id;

                dataReader.Close();

                paramsList.Clear();
                SqlParameter tempParam = new SqlParameter("@AuthorId", SqlDbType.Int);
                tempParam.Value = post.authorId;
                paramsList.Add(tempParam);
                tempParam = new SqlParameter("@PostId", SqlDbType.Int);
                tempParam.Value = post.id;
                paramsList.Add(tempParam);

                command = "INSERT into Shared (Autor, PostId, PostDate) values (@AuthorId, @PostId, GETDATE())";

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

    public class SharedPost
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public DateTime date { get; set; }
    }
}
