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

        public void DB_SOURCE(string IP, string DBNAME, string ACCOUNT, string PS)
        {
            textBox5.Text = "";
            string MESS = "";

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
                    MESS = "成功 連線";
                    textBox5.Text = MESS;
                }
                catch (Exception ex)
                {
                    MESS = "失敗 連線:" + ex.Message;
                    textBox5.Text = MESS;
                    MessageBox.Show(MESS);
                }
            }

        }

        public void DB_TARGET(string IP, string DBNAME, string ACCOUNT, string PS)
        {
            textBox10.Text = "";
            string MESS = "";

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
                    MESS = "成功 連線";
                    textBox10.Text = MESS;
                }
                catch (Exception ex)
                {
                    MESS = "失敗 連線:" + ex.Message;
                    textBox10.Text = MESS;
                    MessageBox.Show(MESS);
                }
            }

        }

        public void ADD_TO_DB(string SOURCE_IP, string SOURCE_DBNAME, string SOURCE_ACCOUNT, string SOURCE_PS, string TARGET_IP, string TARGET_DBNAME, string TARGET_ACCOUNT, string TARGET_PS)
        {
            //來源資料庫
            DataTable DT = SEARCHT_SOURCE(SOURCE_IP, SOURCE_DBNAME, SOURCE_ACCOUNT,  SOURCE_PS);

            if(DT!=null && DT.Rows.Count>=1)
            {
                try
                {
                    //目地資料庫
                    StringBuilder connectionString = new StringBuilder();
                    connectionString.AppendFormat(@"
                                            Data Source={0};Initial Catalog={1};
                                             Persist Security Info=True;User ID={2};Password={3}
                                            ", TARGET_IP, TARGET_DBNAME, TARGET_ACCOUNT, TARGET_PS);



                    SqlConnectionStringBuilder sqlsb = new SqlConnectionStringBuilder(connectionString.ToString());

                    sqlConn = new SqlConnection(sqlsb.ConnectionString);

                    using (SqlConnection connection = sqlConn)
                    {
                       
                        // 建立 SqlBulkCopy 物件
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            // 設定資料庫目標表格的名稱
                            bulkCopy.DestinationTableName = "POSTA";

                            // 設定要寫入的資料行對應關係
                            bulkCopy.ColumnMappings.Add("TA001", "TA001");
                            bulkCopy.ColumnMappings.Add("TA002", "TA002");
                            bulkCopy.ColumnMappings.Add("TA003", "TA003");
                            bulkCopy.ColumnMappings.Add("TA006", "TA006");

                            // 開始寫入
                            connection.Open();
                            bulkCopy.WriteToServer(DT);
                        }
                    }
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        public DataTable SEARCHT_SOURCE(string IP, string DBNAME, string ACCOUNT, string PS)
        {
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            SqlCommandBuilder sqlCmdBuilder1 = new SqlCommandBuilder();
            DataSet ds1 = new DataSet();

            //THISYEARS = "21";

            try
            {
                StringBuilder connectionString = new StringBuilder();
                connectionString.AppendFormat(@"
                                            Data Source={0};Initial Catalog={1};
                                             Persist Security Info=True;User ID={2};Password={3}
                                            ", IP, DBNAME, ACCOUNT, PS);



                SqlConnectionStringBuilder sqlsb = new SqlConnectionStringBuilder(connectionString.ToString());
                              
                sqlConn = new SqlConnection(sqlsb.ConnectionString);


                sbSql.Clear();
                sbSqlQuery.Clear();



                //核準過TASK_RESULT='0'
                //AND DOC_NBR  LIKE 'QC1002{0}%'

                sbSql.AppendFormat(@"  
                                    SELECT  TOP 10 *
                                    FROM [COSMOS_POS].[dbo].[POSTA]
                                    WHERE REPLACE(TA001+TA002+TA003+TA006,' ','')  COLLATE Chinese_Taiwan_Stroke_CI_AS NOT IN (SELECT REPLACE(TA001+TA002+TA003+TA006,' ','') FROM  [TK].[dbo].[POSTA]) 
                                    ORDER BY TA001 DESC


                                        ");


                adapter1 = new SqlDataAdapter(@"" + sbSql, sqlConn);

                sqlCmdBuilder1 = new SqlCommandBuilder(adapter1);
                sqlConn.Open();
                ds1.Clear();
                adapter1.Fill(ds1, "ds1");
                sqlConn.Close();

                if (ds1.Tables["ds1"].Rows.Count >= 1)
                {
                    return ds1.Tables["ds1"];

                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                sqlConn.Close();
            }
        }





        #endregion
        #region FUNCTION
        private void button1_Click(object sender, EventArgs e)
        {
            DB_SOURCE(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB_TARGET(textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ADD_TO_DB(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
        }
        #endregion


    }
}
