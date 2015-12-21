using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.ServiceModel
{
    [Route("/users/{Login}/posts", "POST")]

    public class AddUserPostRequest : IReturn<AddUserPostResponse>
    {
        public int Login { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class AddUserPostResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }

        public int Respond(AddUserPostRequest request)
        {
            UserPost post = new UserPost();
            string connectionString = null;
            SqlConnection cnn;
            connectionString = "Server=tcp:fantastic.database.windows.net,1433;Database=fantastic;User ID=qacpiweb@fantastic;Password=nhm554WW;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            cnn = new SqlConnection(connectionString);
            try
            {
                string cmd = String.Format("select id from Users where Login='{0}'", request.Login);
                SqlCommand polecenie = new SqlCommand(cmd, cnn);
                SqlDataReader dataReader;
                cnn.Open();
                dataReader = polecenie.ExecuteReader();
                dataReader.Read();

                post.authorId = dataReader.GetInt32(0);
                post.title = request.Title;
                post.text = request.Content;
                post.tags = 0;
                post.reblog = 0;

                dataReader.Close();

                string command = String.Format("INSERT into Wall (Autor, Tytul, reblog, Tresc, Tagi, attachment, DataPosta) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', GETDATE())", post.authorId, post.title, post.reblog, post.text, post.tags, "");
                polecenie = new SqlCommand(command, cnn);
                cnn.Open();
                dataReader = polecenie.ExecuteReader();

                return 200;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
