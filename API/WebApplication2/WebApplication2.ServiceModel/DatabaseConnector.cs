using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.ServiceModel
{
    public class DatabaseConnector
    {
        public static string connectionString = "Server=tcp:fantastic.database.windows.net,1433;Database=restAPI;User ID=qacpiweb@fantastic;Password=nhm554WW;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public SqlConnection connection;
        public string command;
        public SqlCommand sqlCommand;
        public SqlDataReader dataReader;

        public DatabaseConnector()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public SqlDataReader executeCommand(string command, List<SqlParameter> parameters)
        {
            sqlCommand = new SqlCommand(command, connection);
            foreach (var parameter in parameters)
            {
                sqlCommand.Parameters.Add(parameter);
            }
            dataReader = sqlCommand.ExecuteReader();
            return dataReader;
        }

        ~DatabaseConnector()
        {
            connection.Close();
        }

    }
}
