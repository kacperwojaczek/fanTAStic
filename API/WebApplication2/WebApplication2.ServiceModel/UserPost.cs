using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.ServiceModel
{
    [Route("/posts/{Id}", "GET")]
    public class UserPostRequest : IReturn<UserPostResponse>
    {
        public int Id { get; set; }
    }

    public class UserPostResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public UserPost Respond(UserPostRequest request)
        {
            UserPost post = new UserPost();
            string connectionString = null;
            SqlConnection cnn;
            connectionString = "workstation id=fantastic.mssql.somee.com;packet size=4096;user id=qacpiweb_SQLLogin_1;pwd=cfzqlqsobm;data source=fantastic.mssql.somee.com;persist security info=False;initial catalog=fantastic";
            cnn = new SqlConnection(connectionString);
            try
            {
                string command = String.Format("Select * from Wall where id='{0}'", request.Id);
                SqlCommand polecenie = new SqlCommand(command, cnn);
                SqlDataReader dataReader;
                cnn.Open();
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    post.id = dataReader.GetInt32(0);
                    post.authorId = dataReader.GetInt32(1);
                    post.title = dataReader.GetString(2);
                    post.reblog = dataReader.GetInt32(3);
                    post.text = dataReader.GetString(4);
                    post.attachment = dataReader.GetString(6);
                    post.date = dataReader.GetString(7);

                    dataReader.Close();

                    cnn.Close();
                    return post;
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

    public class UserPost
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public string title { get; set; }
        public int reblog { get; set; }
        public string text { get; set; }
        public int tags { get; set; }
        public string attachment { get; set; }
        public string date { get; set; }

    }
}
