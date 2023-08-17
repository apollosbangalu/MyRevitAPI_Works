using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TestWorks
{
    public class SQLDBconnect
    {
        public static SqlConnection connect;
        public void ConnecDB()
        {
            connect = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=RevitSQLTrail;Integrated Security=True;Pooling=False");
            connect.Open();
        }
        public SqlCommand Query(string sqlQuery)
        {
            SqlCommand command = new SqlCommand(sqlQuery, connect);
            return command;
        }
    }
}
