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

        public void ADD_TO_DB_POSTA(string SOURCE_IP, string SOURCE_DBNAME, string SOURCE_ACCOUNT, string SOURCE_PS, string TARGET_IP, string TARGET_DBNAME, string TARGET_ACCOUNT, string TARGET_PS)
        {
            //來源資料庫
            DataTable DT_POSTA = SEARCHT_SOURCE_POSTA(SOURCE_IP, SOURCE_DBNAME, SOURCE_ACCOUNT,  SOURCE_PS);

            if(DT_POSTA != null && DT_POSTA.Rows.Count>=1)
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
                            bulkCopy.ColumnMappings.Add("COMPANY", "COMPANY");
                            bulkCopy.ColumnMappings.Add("CREATOR", "CREATOR");
                            bulkCopy.ColumnMappings.Add("USR_GROUP", "USR_GROUP");
                            bulkCopy.ColumnMappings.Add("CREATE_DATE", "CREATE_DATE");
                            bulkCopy.ColumnMappings.Add("MODIFIER", "MODIFIER");
                            bulkCopy.ColumnMappings.Add("MODI_DATE", "MODI_DATE");
                            bulkCopy.ColumnMappings.Add("FLAG", "FLAG");
                            bulkCopy.ColumnMappings.Add("CREATE_TIME", "CREATE_TIME");
                            bulkCopy.ColumnMappings.Add("MODI_TIME", "MODI_TIME");
                            bulkCopy.ColumnMappings.Add("TRANS_TYPE", "TRANS_TYPE");
                            bulkCopy.ColumnMappings.Add("TRANS_NAME", "TRANS_NAME");
                            bulkCopy.ColumnMappings.Add("sync_date", "sync_date");
                            bulkCopy.ColumnMappings.Add("sync_time", "sync_time");
                            bulkCopy.ColumnMappings.Add("sync_mark", "sync_mark");
                            bulkCopy.ColumnMappings.Add("sync_count", "sync_count");
                            bulkCopy.ColumnMappings.Add("DataUser", "DataUser");
                            bulkCopy.ColumnMappings.Add("DataGroup", "DataGroup");
                            bulkCopy.ColumnMappings.Add("TA001", "TA001");
                            bulkCopy.ColumnMappings.Add("TA002", "TA002");
                            bulkCopy.ColumnMappings.Add("TA003", "TA003");
                            bulkCopy.ColumnMappings.Add("TA004", "TA004");
                            bulkCopy.ColumnMappings.Add("TA005", "TA005");
                            bulkCopy.ColumnMappings.Add("TA006", "TA006");
                            bulkCopy.ColumnMappings.Add("TA007", "TA007");
                            bulkCopy.ColumnMappings.Add("TA008", "TA008");
                            bulkCopy.ColumnMappings.Add("TA009", "TA009");
                            bulkCopy.ColumnMappings.Add("TA010", "TA010");
                            bulkCopy.ColumnMappings.Add("TA011", "TA011");
                            bulkCopy.ColumnMappings.Add("TA012", "TA012");
                            bulkCopy.ColumnMappings.Add("TA013", "TA013");
                            bulkCopy.ColumnMappings.Add("TA014", "TA014");
                            bulkCopy.ColumnMappings.Add("TA015", "TA015");
                            bulkCopy.ColumnMappings.Add("TA016", "TA016");
                            bulkCopy.ColumnMappings.Add("TA017", "TA017");
                            bulkCopy.ColumnMappings.Add("TA018", "TA018");
                            bulkCopy.ColumnMappings.Add("TA019", "TA019");
                            bulkCopy.ColumnMappings.Add("TA020", "TA020");
                            bulkCopy.ColumnMappings.Add("TA021", "TA021");
                            bulkCopy.ColumnMappings.Add("TA022", "TA022");
                            bulkCopy.ColumnMappings.Add("TA023", "TA023");
                            bulkCopy.ColumnMappings.Add("TA024", "TA024");
                            bulkCopy.ColumnMappings.Add("TA025", "TA025");
                            bulkCopy.ColumnMappings.Add("TA026", "TA026");
                            bulkCopy.ColumnMappings.Add("TA027", "TA027");
                            bulkCopy.ColumnMappings.Add("TA028", "TA028");
                            bulkCopy.ColumnMappings.Add("TA029", "TA029");
                            bulkCopy.ColumnMappings.Add("TA030", "TA030");
                            bulkCopy.ColumnMappings.Add("TA031", "TA031");
                            bulkCopy.ColumnMappings.Add("TA032", "TA032");
                            bulkCopy.ColumnMappings.Add("TA033", "TA033");
                            bulkCopy.ColumnMappings.Add("TA034", "TA034");
                            bulkCopy.ColumnMappings.Add("TA035", "TA035");
                            bulkCopy.ColumnMappings.Add("TA036", "TA036");
                            bulkCopy.ColumnMappings.Add("TA037", "TA037");
                            bulkCopy.ColumnMappings.Add("TA038", "TA038");
                            bulkCopy.ColumnMappings.Add("TA039", "TA039");
                            bulkCopy.ColumnMappings.Add("TA040", "TA040");
                            bulkCopy.ColumnMappings.Add("TA041", "TA041");
                            bulkCopy.ColumnMappings.Add("TA042", "TA042");
                            bulkCopy.ColumnMappings.Add("TA043", "TA043");
                            bulkCopy.ColumnMappings.Add("TA044", "TA044");
                            bulkCopy.ColumnMappings.Add("TA045", "TA045");
                            bulkCopy.ColumnMappings.Add("TA046", "TA046");
                            bulkCopy.ColumnMappings.Add("TA047", "TA047");
                            bulkCopy.ColumnMappings.Add("TA048", "TA048");
                            bulkCopy.ColumnMappings.Add("TA049", "TA049");
                            bulkCopy.ColumnMappings.Add("TA050", "TA050");
                            bulkCopy.ColumnMappings.Add("TA051", "TA051");
                            bulkCopy.ColumnMappings.Add("TA052", "TA052");
                            bulkCopy.ColumnMappings.Add("TA053", "TA053");
                            bulkCopy.ColumnMappings.Add("TA054", "TA054");
                            bulkCopy.ColumnMappings.Add("TA055", "TA055");
                            bulkCopy.ColumnMappings.Add("TA056", "TA056");
                            bulkCopy.ColumnMappings.Add("TA057", "TA057");
                            bulkCopy.ColumnMappings.Add("TA058", "TA058");
                            bulkCopy.ColumnMappings.Add("TA059", "TA059");
                            bulkCopy.ColumnMappings.Add("TA060", "TA060");
                            bulkCopy.ColumnMappings.Add("TA061", "TA061");
                            bulkCopy.ColumnMappings.Add("TA062", "TA062");
                            bulkCopy.ColumnMappings.Add("TA063", "TA063");
                            bulkCopy.ColumnMappings.Add("TA064", "TA064");
                            bulkCopy.ColumnMappings.Add("TA065", "TA065");
                            bulkCopy.ColumnMappings.Add("TA066", "TA066");
                            bulkCopy.ColumnMappings.Add("TA067", "TA067");
                            bulkCopy.ColumnMappings.Add("TA068", "TA068");
                            bulkCopy.ColumnMappings.Add("TA069", "TA069");
                            bulkCopy.ColumnMappings.Add("TA070", "TA070");
                            bulkCopy.ColumnMappings.Add("TA071", "TA071");
                            bulkCopy.ColumnMappings.Add("TA072", "TA072");
                            bulkCopy.ColumnMappings.Add("TA073", "TA073");
                            bulkCopy.ColumnMappings.Add("TA074", "TA074");
                            bulkCopy.ColumnMappings.Add("TA075", "TA075");
                            bulkCopy.ColumnMappings.Add("TA076", "TA076");
                            bulkCopy.ColumnMappings.Add("TA077", "TA077");
                            bulkCopy.ColumnMappings.Add("TA078", "TA078");
                            bulkCopy.ColumnMappings.Add("TA079", "TA079");
                            bulkCopy.ColumnMappings.Add("TA080", "TA080");
                            bulkCopy.ColumnMappings.Add("TA081", "TA081");
                            bulkCopy.ColumnMappings.Add("TA082", "TA082");
                            bulkCopy.ColumnMappings.Add("TA083", "TA083");
                            bulkCopy.ColumnMappings.Add("TA084", "TA084");
                            bulkCopy.ColumnMappings.Add("TA085", "TA085");
                            bulkCopy.ColumnMappings.Add("TA086", "TA086");
                            bulkCopy.ColumnMappings.Add("TA087", "TA087");
                            bulkCopy.ColumnMappings.Add("TA088", "TA088");
                            bulkCopy.ColumnMappings.Add("TA089", "TA089");
                            bulkCopy.ColumnMappings.Add("TA090", "TA090");
                            bulkCopy.ColumnMappings.Add("TA091", "TA091");
                            bulkCopy.ColumnMappings.Add("TA092", "TA092");
                            bulkCopy.ColumnMappings.Add("TA093", "TA093");
                            bulkCopy.ColumnMappings.Add("TA094", "TA094");
                            bulkCopy.ColumnMappings.Add("TA095", "TA095");
                            bulkCopy.ColumnMappings.Add("TA096", "TA096");
                            bulkCopy.ColumnMappings.Add("TA097", "TA097");
                            bulkCopy.ColumnMappings.Add("TA098", "TA098");
                            bulkCopy.ColumnMappings.Add("TA099", "TA099");
                            bulkCopy.ColumnMappings.Add("TA100", "TA100");
                            bulkCopy.ColumnMappings.Add("TA101", "TA101");
                            bulkCopy.ColumnMappings.Add("TA102", "TA102");
                            bulkCopy.ColumnMappings.Add("TA103", "TA103");
                            bulkCopy.ColumnMappings.Add("TA104", "TA104");
                            bulkCopy.ColumnMappings.Add("TA105", "TA105");
                            bulkCopy.ColumnMappings.Add("TA106", "TA106");
                            bulkCopy.ColumnMappings.Add("TA107", "TA107");
                            bulkCopy.ColumnMappings.Add("TA108", "TA108");
                            bulkCopy.ColumnMappings.Add("TA109", "TA109");
                            bulkCopy.ColumnMappings.Add("TA110", "TA110");
                            bulkCopy.ColumnMappings.Add("TA111", "TA111");
                            bulkCopy.ColumnMappings.Add("TA112", "TA112");
                            bulkCopy.ColumnMappings.Add("TA113", "TA113");
                            bulkCopy.ColumnMappings.Add("TA114", "TA114");
                            bulkCopy.ColumnMappings.Add("TA115", "TA115");
                            bulkCopy.ColumnMappings.Add("TA116", "TA116");
                            bulkCopy.ColumnMappings.Add("TA117", "TA117");
                            bulkCopy.ColumnMappings.Add("UDF01", "UDF01");
                            bulkCopy.ColumnMappings.Add("UDF02", "UDF02");
                            bulkCopy.ColumnMappings.Add("UDF03", "UDF03");
                            bulkCopy.ColumnMappings.Add("UDF04", "UDF04");
                            bulkCopy.ColumnMappings.Add("UDF05", "UDF05");
                            bulkCopy.ColumnMappings.Add("UDF06", "UDF06");
                            bulkCopy.ColumnMappings.Add("UDF07", "UDF07");
                            bulkCopy.ColumnMappings.Add("UDF08", "UDF08");
                            bulkCopy.ColumnMappings.Add("UDF09", "UDF09");
                            bulkCopy.ColumnMappings.Add("UDF10", "UDF10");






                            // 開始寫入
                            connection.Open();
                            bulkCopy.WriteToServer(DT_POSTA);
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

        public DataTable SEARCHT_SOURCE_POSTA(string IP, string DBNAME, string ACCOUNT, string PS)
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

        public void ADD_TO_DB_POSTB(string SOURCE_IP, string SOURCE_DBNAME, string SOURCE_ACCOUNT, string SOURCE_PS, string TARGET_IP, string TARGET_DBNAME, string TARGET_ACCOUNT, string TARGET_PS)
        {
            //來源資料庫
            DataTable DT_POSTB = SEARCHT_SOURCE_POSTB(SOURCE_IP, SOURCE_DBNAME, SOURCE_ACCOUNT, SOURCE_PS);

            if (DT_POSTB != null && DT_POSTB.Rows.Count >= 1)
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
                            bulkCopy.DestinationTableName = "POSTB";

                            // 設定要寫入的資料行對應關係                     

                            bulkCopy.ColumnMappings.Add("COMPANY", "COMPANY");
                            bulkCopy.ColumnMappings.Add("CREATOR", "CREATOR");
                            bulkCopy.ColumnMappings.Add("USR_GROUP", "USR_GROUP");
                            bulkCopy.ColumnMappings.Add("CREATE_DATE", "CREATE_DATE");
                            bulkCopy.ColumnMappings.Add("MODIFIER", "MODIFIER");
                            bulkCopy.ColumnMappings.Add("MODI_DATE", "MODI_DATE");
                            bulkCopy.ColumnMappings.Add("FLAG", "FLAG");
                            bulkCopy.ColumnMappings.Add("CREATE_TIME", "CREATE_TIME");
                            bulkCopy.ColumnMappings.Add("MODI_TIME", "MODI_TIME");
                            bulkCopy.ColumnMappings.Add("TRANS_TYPE", "TRANS_TYPE");
                            bulkCopy.ColumnMappings.Add("TRANS_NAME", "TRANS_NAME");
                            bulkCopy.ColumnMappings.Add("sync_date", "sync_date");
                            bulkCopy.ColumnMappings.Add("sync_time", "sync_time");
                            bulkCopy.ColumnMappings.Add("sync_mark", "sync_mark");
                            bulkCopy.ColumnMappings.Add("sync_count", "sync_count");
                            bulkCopy.ColumnMappings.Add("DataUser", "DataUser");
                            bulkCopy.ColumnMappings.Add("DataGroup", "DataGroup");
                            bulkCopy.ColumnMappings.Add("TB001", "TB001");
                            bulkCopy.ColumnMappings.Add("TB002", "TB002");
                            bulkCopy.ColumnMappings.Add("TB003", "TB003");
                            bulkCopy.ColumnMappings.Add("TB004", "TB004");
                            bulkCopy.ColumnMappings.Add("TB005", "TB005");
                            bulkCopy.ColumnMappings.Add("TB006", "TB006");
                            bulkCopy.ColumnMappings.Add("TB007", "TB007");
                            bulkCopy.ColumnMappings.Add("TB008", "TB008");
                            bulkCopy.ColumnMappings.Add("TB009", "TB009");
                            bulkCopy.ColumnMappings.Add("TB010", "TB010");
                            bulkCopy.ColumnMappings.Add("TB011", "TB011");
                            bulkCopy.ColumnMappings.Add("TB012", "TB012");
                            bulkCopy.ColumnMappings.Add("TB013", "TB013");
                            bulkCopy.ColumnMappings.Add("TB014", "TB014");
                            bulkCopy.ColumnMappings.Add("TB015", "TB015");
                            bulkCopy.ColumnMappings.Add("TB016", "TB016");
                            bulkCopy.ColumnMappings.Add("TB017", "TB017");
                            bulkCopy.ColumnMappings.Add("TB018", "TB018");
                            bulkCopy.ColumnMappings.Add("TB019", "TB019");
                            bulkCopy.ColumnMappings.Add("TB020", "TB020");
                            bulkCopy.ColumnMappings.Add("TB021", "TB021");
                            bulkCopy.ColumnMappings.Add("TB022", "TB022");
                            bulkCopy.ColumnMappings.Add("TB023", "TB023");
                            bulkCopy.ColumnMappings.Add("TB024", "TB024");
                            bulkCopy.ColumnMappings.Add("TB025", "TB025");
                            bulkCopy.ColumnMappings.Add("TB026", "TB026");
                            bulkCopy.ColumnMappings.Add("TB027", "TB027");
                            bulkCopy.ColumnMappings.Add("TB028", "TB028");
                            bulkCopy.ColumnMappings.Add("TB029", "TB029");
                            bulkCopy.ColumnMappings.Add("TB030", "TB030");
                            bulkCopy.ColumnMappings.Add("TB031", "TB031");
                            bulkCopy.ColumnMappings.Add("TB032", "TB032");
                            bulkCopy.ColumnMappings.Add("TB033", "TB033");
                            bulkCopy.ColumnMappings.Add("TB034", "TB034");
                            bulkCopy.ColumnMappings.Add("TB035", "TB035");
                            bulkCopy.ColumnMappings.Add("TB036", "TB036");
                            bulkCopy.ColumnMappings.Add("TB037", "TB037");
                            bulkCopy.ColumnMappings.Add("TB038", "TB038");
                            bulkCopy.ColumnMappings.Add("TB039", "TB039");
                            bulkCopy.ColumnMappings.Add("TB040", "TB040");
                            bulkCopy.ColumnMappings.Add("TB041", "TB041");
                            bulkCopy.ColumnMappings.Add("TB042", "TB042");
                            bulkCopy.ColumnMappings.Add("TB043", "TB043");
                            bulkCopy.ColumnMappings.Add("TB044", "TB044");
                            bulkCopy.ColumnMappings.Add("TB045", "TB045");
                            bulkCopy.ColumnMappings.Add("TB046", "TB046");
                            bulkCopy.ColumnMappings.Add("TB047", "TB047");
                            bulkCopy.ColumnMappings.Add("TB048", "TB048");
                            bulkCopy.ColumnMappings.Add("TB049", "TB049");
                            bulkCopy.ColumnMappings.Add("TB050", "TB050");
                            bulkCopy.ColumnMappings.Add("TB051", "TB051");
                            bulkCopy.ColumnMappings.Add("TB052", "TB052");
                            bulkCopy.ColumnMappings.Add("TB053", "TB053");
                            bulkCopy.ColumnMappings.Add("TB054", "TB054");
                            bulkCopy.ColumnMappings.Add("TB055", "TB055");
                            bulkCopy.ColumnMappings.Add("TB056", "TB056");
                            bulkCopy.ColumnMappings.Add("TB057", "TB057");
                            bulkCopy.ColumnMappings.Add("TB058", "TB058");
                            bulkCopy.ColumnMappings.Add("TB059", "TB059");
                            bulkCopy.ColumnMappings.Add("TB060", "TB060");
                            bulkCopy.ColumnMappings.Add("TB061", "TB061");
                            bulkCopy.ColumnMappings.Add("TB062", "TB062");
                            bulkCopy.ColumnMappings.Add("TB063", "TB063");
                            bulkCopy.ColumnMappings.Add("TB064", "TB064");
                            bulkCopy.ColumnMappings.Add("TB065", "TB065");
                            bulkCopy.ColumnMappings.Add("TB066", "TB066");
                            bulkCopy.ColumnMappings.Add("TB067", "TB067");
                            bulkCopy.ColumnMappings.Add("TB068", "TB068");
                            bulkCopy.ColumnMappings.Add("TB069", "TB069");
                            bulkCopy.ColumnMappings.Add("TB070", "TB070");
                            bulkCopy.ColumnMappings.Add("TB071", "TB071");
                            bulkCopy.ColumnMappings.Add("TB072", "TB072");
                            bulkCopy.ColumnMappings.Add("TB073", "TB073");
                            bulkCopy.ColumnMappings.Add("TB074", "TB074");
                            bulkCopy.ColumnMappings.Add("TB075", "TB075");
                            bulkCopy.ColumnMappings.Add("TB076", "TB076");
                            bulkCopy.ColumnMappings.Add("TB077", "TB077");
                            bulkCopy.ColumnMappings.Add("TB078", "TB078");
                            bulkCopy.ColumnMappings.Add("TB079", "TB079");
                            bulkCopy.ColumnMappings.Add("TB080", "TB080");
                            bulkCopy.ColumnMappings.Add("TB081", "TB081");
                            bulkCopy.ColumnMappings.Add("TB082", "TB082");
                            bulkCopy.ColumnMappings.Add("TB083", "TB083");
                            bulkCopy.ColumnMappings.Add("TB084", "TB084");
                            bulkCopy.ColumnMappings.Add("TB085", "TB085");
                            bulkCopy.ColumnMappings.Add("TB086", "TB086");
                            bulkCopy.ColumnMappings.Add("TB087", "TB087");
                            bulkCopy.ColumnMappings.Add("TB088", "TB088");
                            bulkCopy.ColumnMappings.Add("TB089", "TB089");
                            bulkCopy.ColumnMappings.Add("TB090", "TB090");
                            bulkCopy.ColumnMappings.Add("UDF01", "UDF01");
                            bulkCopy.ColumnMappings.Add("UDF02", "UDF02");
                            bulkCopy.ColumnMappings.Add("UDF03", "UDF03");
                            bulkCopy.ColumnMappings.Add("UDF04", "UDF04");
                            bulkCopy.ColumnMappings.Add("UDF05", "UDF05");
                            bulkCopy.ColumnMappings.Add("UDF06", "UDF06");
                            bulkCopy.ColumnMappings.Add("UDF07", "UDF07");
                            bulkCopy.ColumnMappings.Add("UDF08", "UDF08");
                            bulkCopy.ColumnMappings.Add("UDF09", "UDF09");
                            bulkCopy.ColumnMappings.Add("UDF10", "UDF10");







                            // 開始寫入
                            connection.Open();
                            bulkCopy.WriteToServer(DT_POSTB);
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
        public DataTable SEARCHT_SOURCE_POSTB(string IP, string DBNAME, string ACCOUNT, string PS)
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
                                    FROM [COSMOS_POS].[dbo].[POSTB]
                                    WHERE REPLACE(TB001+TB002+TB003+TB006+TB007,' ','')  COLLATE Chinese_Taiwan_Stroke_CI_AS NOT IN (SELECT REPLACE(TB001+TB002+TB003+TB006+TB007,' ','') FROM  [TK].[dbo].[POSTB]) 
                                    ORDER BY TB001 DESC


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

        public void ADD_TO_DB_POSTC(string SOURCE_IP, string SOURCE_DBNAME, string SOURCE_ACCOUNT, string SOURCE_PS, string TARGET_IP, string TARGET_DBNAME, string TARGET_ACCOUNT, string TARGET_PS)
        {
            //來源資料庫
            DataTable DT_POSTC = SEARCHT_SOURCE_POSTC(SOURCE_IP, SOURCE_DBNAME, SOURCE_ACCOUNT, SOURCE_PS);

            if (DT_POSTC != null && DT_POSTC.Rows.Count >= 1)
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
                            bulkCopy.DestinationTableName = "POSTC";

                            // 設定要寫入的資料行對應關係                     

                            bulkCopy.ColumnMappings.Add("COMPANY", "COMPANY");
                            bulkCopy.ColumnMappings.Add("CREATOR", "CREATOR");
                            bulkCopy.ColumnMappings.Add("USR_GROUP", "USR_GROUP");
                            bulkCopy.ColumnMappings.Add("CREATE_DATE", "CREATE_DATE");
                            bulkCopy.ColumnMappings.Add("MODIFIER", "MODIFIER");
                            bulkCopy.ColumnMappings.Add("MODI_DATE", "MODI_DATE");
                            bulkCopy.ColumnMappings.Add("FLAG", "FLAG");
                            bulkCopy.ColumnMappings.Add("CREATE_TIME", "CREATE_TIME");
                            bulkCopy.ColumnMappings.Add("MODI_TIME", "MODI_TIME");
                            bulkCopy.ColumnMappings.Add("TRANS_TYPE", "TRANS_TYPE");
                            bulkCopy.ColumnMappings.Add("TRANS_NAME", "TRANS_NAME");
                            bulkCopy.ColumnMappings.Add("sync_date", "sync_date");
                            bulkCopy.ColumnMappings.Add("sync_time", "sync_time");
                            bulkCopy.ColumnMappings.Add("sync_mark", "sync_mark");
                            bulkCopy.ColumnMappings.Add("sync_count", "sync_count");
                            bulkCopy.ColumnMappings.Add("DataUser", "DataUser");
                            bulkCopy.ColumnMappings.Add("DataGroup", "DataGroup");
                            bulkCopy.ColumnMappings.Add("TC001", "TC001");
                            bulkCopy.ColumnMappings.Add("TC002", "TC002");
                            bulkCopy.ColumnMappings.Add("TC003", "TC003");
                            bulkCopy.ColumnMappings.Add("TC004", "TC004");
                            bulkCopy.ColumnMappings.Add("TC005", "TC005");
                            bulkCopy.ColumnMappings.Add("TC006", "TC006");
                            bulkCopy.ColumnMappings.Add("TC007", "TC007");
                            bulkCopy.ColumnMappings.Add("TC008", "TC008");
                            bulkCopy.ColumnMappings.Add("TC009", "TC009");
                            bulkCopy.ColumnMappings.Add("TC010", "TC010");
                            bulkCopy.ColumnMappings.Add("TC011", "TC011");
                            bulkCopy.ColumnMappings.Add("TC012", "TC012");
                            bulkCopy.ColumnMappings.Add("TC013", "TC013");
                            bulkCopy.ColumnMappings.Add("TC014", "TC014");
                            bulkCopy.ColumnMappings.Add("TC015", "TC015");
                            bulkCopy.ColumnMappings.Add("TC016", "TC016");
                            bulkCopy.ColumnMappings.Add("TC017", "TC017");
                            bulkCopy.ColumnMappings.Add("TC018", "TC018");
                            bulkCopy.ColumnMappings.Add("TC019", "TC019");
                            bulkCopy.ColumnMappings.Add("TC020", "TC020");
                            bulkCopy.ColumnMappings.Add("TC021", "TC021");
                            bulkCopy.ColumnMappings.Add("TC022", "TC022");
                            bulkCopy.ColumnMappings.Add("TC023", "TC023");
                            bulkCopy.ColumnMappings.Add("TC024", "TC024");
                            bulkCopy.ColumnMappings.Add("TC025", "TC025");
                            bulkCopy.ColumnMappings.Add("TC026", "TC026");
                            bulkCopy.ColumnMappings.Add("TC027", "TC027");
                            bulkCopy.ColumnMappings.Add("TC028", "TC028");
                            bulkCopy.ColumnMappings.Add("TC029", "TC029");
                            bulkCopy.ColumnMappings.Add("TC030", "TC030");
                            bulkCopy.ColumnMappings.Add("TC031", "TC031");
                            bulkCopy.ColumnMappings.Add("TC032", "TC032");
                            bulkCopy.ColumnMappings.Add("TC033", "TC033");
                            bulkCopy.ColumnMappings.Add("TC034", "TC034");
                            bulkCopy.ColumnMappings.Add("TC035", "TC035");
                            bulkCopy.ColumnMappings.Add("TC036", "TC036");
                            bulkCopy.ColumnMappings.Add("TC037", "TC037");
                            bulkCopy.ColumnMappings.Add("TC038", "TC038");
                            bulkCopy.ColumnMappings.Add("TC039", "TC039");
                            bulkCopy.ColumnMappings.Add("TC040", "TC040");
                            bulkCopy.ColumnMappings.Add("TC041", "TC041");
                            bulkCopy.ColumnMappings.Add("TC042", "TC042");
                            bulkCopy.ColumnMappings.Add("TC043", "TC043");
                            bulkCopy.ColumnMappings.Add("TC044", "TC044");
                            bulkCopy.ColumnMappings.Add("TC045", "TC045");
                            bulkCopy.ColumnMappings.Add("TC046", "TC046");
                            bulkCopy.ColumnMappings.Add("TC047", "TC047");
                            bulkCopy.ColumnMappings.Add("TC048", "TC048");
                            bulkCopy.ColumnMappings.Add("TC049", "TC049");
                            bulkCopy.ColumnMappings.Add("TC050", "TC050");
                            bulkCopy.ColumnMappings.Add("TC051", "TC051");
                            bulkCopy.ColumnMappings.Add("TC052", "TC052");
                            bulkCopy.ColumnMappings.Add("TC053", "TC053");
                            bulkCopy.ColumnMappings.Add("TC054", "TC054");
                            bulkCopy.ColumnMappings.Add("TC055", "TC055");
                            bulkCopy.ColumnMappings.Add("TC056", "TC056");
                            bulkCopy.ColumnMappings.Add("TC057", "TC057");
                            bulkCopy.ColumnMappings.Add("UDF01", "UDF01");
                            bulkCopy.ColumnMappings.Add("UDF02", "UDF02");
                            bulkCopy.ColumnMappings.Add("UDF03", "UDF03");
                            bulkCopy.ColumnMappings.Add("UDF04", "UDF04");
                            bulkCopy.ColumnMappings.Add("UDF05", "UDF05");
                            bulkCopy.ColumnMappings.Add("UDF06", "UDF06");
                            bulkCopy.ColumnMappings.Add("UDF07", "UDF07");
                            bulkCopy.ColumnMappings.Add("UDF08", "UDF08");
                            bulkCopy.ColumnMappings.Add("UDF09", "UDF09");
                            bulkCopy.ColumnMappings.Add("UDF10", "UDF10");



                            // 開始寫入
                            connection.Open();
                            bulkCopy.WriteToServer(DT_POSTC);
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

        public DataTable SEARCHT_SOURCE_POSTC(string IP, string DBNAME, string ACCOUNT, string PS)
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
                                    FROM [COSMOS_POS].[dbo].[POSTC]
                                    WHERE REPLACE(TC001+TC002+TC003+TC006+TC007,' ','')  COLLATE Chinese_Taiwan_Stroke_CI_AS NOT IN (SELECT REPLACE(TC001+TC002+TC003+TC006+TC007,' ','') FROM  [TK].[dbo].[POSTC]) 
                                    ORDER BY TC001 DESC


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
            ADD_TO_DB_POSTA(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
            ADD_TO_DB_POSTB(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
            ADD_TO_DB_POSTC(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);

            MessageBox.Show("完成");
        }
        #endregion


    }
}
