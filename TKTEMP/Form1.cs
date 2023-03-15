using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.HSSF.Util;
using NPOI.HSSF.Extractor;
using System.IO;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using System.Configuration;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;
using FastReport;
using FastReport.Data;
using TKITDLL;
using System.Data.OleDb;

namespace TKTEMP
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConn = new SqlConnection();

        SqlCommand sqlComm = new SqlCommand();
        string connectionString;
        StringBuilder sbSql = new StringBuilder();
        StringBuilder sbSqlQuery = new StringBuilder();
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder();
        SqlTransaction tran;
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string tablename = null;
        int rownum = 0;
        DataGridViewRow row;
        int result;


        public Form1()
        {
            InitializeComponent();
        }

        #region FUNCTION

        public void DB_SOURCE(string IP,string DBNAME,string ACCOUNT,string PS)
        {
            textBox5.Text = "";

            StringBuilder connectionString = new StringBuilder();
            connectionString.AppendFormat(@"
                                            Data Source={0};Initial Catalog={1};
                                             Persist Security Info=True;User ID={2};Password={3}
                                            ", IP, DBNAME, ACCOUNT, PS);
     
            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                try
                {
                    connection.Open();               

                    textBox5.Text = "成功 連線";
                }
                catch (Exception ex)
                {
                    textBox5.Text = "失敗 連線:" + ex.Message;
                }
            }
            
        }

        #endregion
        #region FUNCTION
        private void button1_Click(object sender, EventArgs e)
        {
            DB_SOURCE(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
        }
        #endregion
    }
}
