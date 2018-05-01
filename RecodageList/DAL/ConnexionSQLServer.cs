using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DbAccess;
using System.Configuration;

namespace RecodageList.DAL
{
    class ConnexionSQLServer
    {
        public SqlConnection sql_con;
        public SqlCommand sql_cmd;
        public SqlDataAdapter DB;

        public void SetConnection()
        {
            Console.WriteLine("ConfigurationManager.AppSettings['SQLServerConnectionString']" + ConfigurationManager.AppSettings["SQLServerConnectionString"]);
            sql_con = new SqlConnection(ConfigurationManager.AppSettings["SQLServerConnectionString"]);
        }

        public void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();

            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;

            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
    }
}
