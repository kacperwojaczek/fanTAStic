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

        public List<UserPost> Respond(UserPostRequest request)
        {
            var userPosts = new List<UserPost>();
            UserPost post = new UserPost();
            string connectionString = null;
            SqlConnection cnn;
            connectionString = "Server=tcp:fantastic.database.windows.net,1433;Database=fantastic;User ID=qacpiweb@fantastic;Password=nhm554WW;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            cnn = new SqlConnection(connectionString);
            try
            {
                string command;
                if (request.Id == 0)
                {
                    command = "Select * from Wall";
                    SqlCommand polecenie = new SqlCommand(command, cnn);
                    SqlDataReader dataReader;
                    cnn.Open();
                    dataReader = polecenie.ExecuteReader();
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
                    cnn.Close();
                    return userPosts;
                }
                else
                {
                    command = String.Format("Select * from Wall where id='{0}'", request.Id);
                    SqlCommand polecenie = new SqlCommand(command, cnn);
                    SqlDataReader dataReader;
                    cnn.Open();
                    dataReader = polecenie.ExecuteReader();
                    
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

                        cnn.Close();
                        return userPosts;
                    }
                    else
                    {
                        dataReader.Close();
                        cnn.Close();
                        return null;
                    }
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
