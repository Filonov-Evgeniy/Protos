using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Diplom_TEST
{
    class DBConnection
    {
        private readonly SqlConnection sql = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True;");

        public void OpenConn()
        {
            if (sql.State == System.Data.ConnectionState.Closed)
                sql.Open();
        }
        public void CloseConn()
        {
            if (sql.State == System.Data.ConnectionState.Open)
                sql.Close();
        }

        public SqlConnection GetConn()
        {
            return sql;
        }
    }
}
