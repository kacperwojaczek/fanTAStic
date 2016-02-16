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
    [Route("/tags/{Tag}", "GET")]
   // [Route("/tags/{Tag}", "POST")]
    public class TagSearchRequest : IReturn<TagSearchResponse>
    {
        public string Tag { get; set; }
        //public string Title { get; set; }
        //public string Content { get; set; }
    }
    
    public class TagSearchResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }
        //pobiera posty (danego usera)
        public List<Int32> Get(TagSearchRequest request)
        {
            List<Int32> posts = new List<Int32>();
            var dbConnection = new DatabaseConnector();
            var paramsList = new List<SqlParameter>();
            SqlDataReader dataReader;
            string command;

            try
            {
                SqlParameter loginParam = new SqlParameter("@Tag", SqlDbType.VarChar, request.Tag.Length); 
                loginParam.Value = request.Tag;
                paramsList.Add(loginParam);
                command = "Select * from Wall where Tresc like '%[#]"+request.Tag+"%';";
                //command = "Select * from Wall where Tresc like '%[#]@Tag%'";
                dataReader = dbConnection.executeCommand(command, paramsList);
                
                if (dataReader.HasRows)
                {

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
    }
}