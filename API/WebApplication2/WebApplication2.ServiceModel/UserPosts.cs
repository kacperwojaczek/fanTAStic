using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.ServiceModel
{
    [Route("/users/{Login}/posts", "GET")]
    public class UserPostsRequest : IReturn<UserPostResponse>
    {
        public string Login{get; set;}
        public int Id {get;set;}
    }

    public class UserPostsResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public List<Int32> Respond(UserPostsRequest request)
        {
            List<Int32> posts = new List<Int32>();
            string connectionString = null;
            SqlConnection cnn;
            connectionString = "Server=tcp:fantastic.database.windows.net,1433;Database=fantastic;User ID=qacpiweb@fantastic;Password=nhm554WW;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            cnn = new SqlConnection(connectionString);
            try
            {
                string command = String.Format("Select * from Users where Login='{0}'", request.Login);
                SqlCommand polecenie = new SqlCommand(command, cnn);
                SqlDataReader dataReader;
                cnn.Open();
                dataReader = polecenie.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    Int32 authorId = dataReader.GetInt32(0);
                    dataReader.Close();

                    command = String.Format("Select id from Wall where Autor={0}", authorId);
                    polecenie = new SqlCommand(command, cnn);
                    using (dataReader = polecenie.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            posts.Add(dataReader.GetInt32(0));
                        }
                    }

                    cnn.Close();
                    return posts;
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
}
