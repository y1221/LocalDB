using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalDBTest
{
    public partial class Form1 : Form
    {
        private const string DB_SERVER = @"(LocalDB)\MSSQLLocalDB";
        private const string DB_NAME = "SampleDB";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectCustomers_Click(object sender, EventArgs e)
        {
            DBManager dBManager = new DBManager(DB_SERVER, DB_NAME);

            try
            {
                string query = "SELECT * FROM Customer";

                DataTable dt = new DataTable();
                dt = dBManager.ExecuteQuery(query);

                dataGridView1.DataSource = dt;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Number: {0}, Message: {1}", sqlEx.Number, sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dBManager.Close();
            }
        }
    }
}
