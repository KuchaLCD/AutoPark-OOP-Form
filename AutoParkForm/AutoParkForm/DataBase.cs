using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AutoParkForm
{
    class DataBase
    {
        public static string connectionString = @"Data Source=localhost;" + "Initial Catalog=AutoPark;" + "Integrated Security=True;";
        SqlConnection sqlConnectionString = new SqlConnection(connectionString);
        
        public void openConnection()
        {
            if (sqlConnectionString.State == System.Data.ConnectionState.Closed)
                sqlConnectionString.Open();
        }
        public void closeConnection()
        {
            if (sqlConnectionString.State == System.Data.ConnectionState.Open)
                sqlConnectionString.Close();
        }
        public SqlConnection getConnection()
        {
            return sqlConnectionString;
        }
    }
}
