using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Alfursan.Repository
{
    public class DapperHelper : IDisposable
    {
        private static SqlConnection sqlConnection;
        public static SqlConnection CreateConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alfursan"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
        public void Dispose()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}
