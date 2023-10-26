using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDBTest
{
    class DBManager
    {
        private SqlConnection sqlCon;
        private SqlCommand sqlCom;
        private SqlDataAdapter sqlDA;
        private DataTable returnDB;

        // ********************
        // DB接続（Windows認証）
        // ********************
        public DBManager(string dbServer, string dbName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = dbServer,
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };

            sqlCon = new SqlConnection(builder.ConnectionString);
            sqlCon.Open();
        }

        // ********************
        // DB接続（ユーザ認証）
        // ********************
        public DBManager(string dbServer, string dbName, string dbUser, string dbPass)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = dbServer,
                InitialCatalog = dbName,
                UserID = dbUser,
                Password = dbPass,
                IntegratedSecurity = false
            };

            sqlCon = new SqlConnection(builder.ConnectionString);
            sqlCon.Open();
        }

        // ********************
        // DB切断
        // ********************
        public void Close()
        {
            sqlCon.Close();
            sqlCon.Dispose();
        }

        // ********************
        // クエリ実行（参照系）
        // ********************
        public DataTable ExecuteQuery(string query)
        {
            using (sqlCom = new SqlCommand(query, sqlCon))
            {
                sqlDA = new SqlDataAdapter(sqlCom);

                returnDB = new DataTable();
                sqlDA.Fill(returnDB);
            }

            return returnDB;
        }
    }
}
