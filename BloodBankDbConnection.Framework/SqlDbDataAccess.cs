using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BloodBankDbConnection.Framework
{
    public class SqlDbDataAccess
    {
        const string connectionString = "Data Source=DESKTOP-S9CM2S5\\SQLEXPRESS;Initial Catalog=\"Blood Bank\";Integrated Security=True";

        public SqlCommand GetCommand(String query)
        {
            var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = connection;
            return cmd;
        }
    }
}
