using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;




namespace TestWorks
{
    //I changed the connection class name here to suit the OkoBaudat database but I left all others unchanged.
    public class SqlLcaDbConnect2
    {
        public static SqlConnection connect;
        public void ConnecDB()
        {
            connect = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=OkoData4C;Integrated Security=True");
            connect.Open();
        }
        public SqlCommand Query(string sqlQuery)
        {
            SqlCommand command = new SqlCommand(sqlQuery, connect);
            return command;
        }
    }
}
