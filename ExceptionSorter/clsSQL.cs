using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceptionSorter
{
    public class clsSQL
    {
        public SqlConnection cnSQLData;
        public clsConfig oConfig;
        public string sVS3DB = "VS3";
        public string sError = "";

        public clsSQL(clsConfig myConfig)
        {
            oConfig = myConfig;

            cnSQLData = new SqlConnection("Server=" + oConfig.sDataServer +
                                           ";DataBase=" + oConfig.sDataDBName +
                                           ";UID=" + oConfig.sUID +
                                           ";PWD=" + oConfig.sPWD +
                                           ";Application Name=AppointmentProviderList");
        }
        public DataTable GetPracticeList()
        {
            DataTable dtCustomers = new DataTable();
            string sSQL = "Select PracticeIdentifier, RTRIM(LTRIM(CorporateName)) as Name, id, ServerIP ";
            sSQL += "From dbo.Practice Where PracticeIdentifier != '998' ";
            sSQL += "Order by PracticeIdentifier ASC";
            SqlCommand cmdSQL = new SqlCommand(sSQL, cnSQLData);
            SqlDataAdapter daSQL;
            try
            {
                if (cnSQLData.State != ConnectionState.Open) { cnSQLData.Open(); }
                daSQL = new SqlDataAdapter(cmdSQL);
                daSQL.Fill(dtCustomers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in GetPracticeList (Practice), Error = " + ex.Message + " " + cmdSQL.CommandText + " " + cnSQLData.ConnectionString, "Error Retrieving Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cnSQLData.State != ConnectionState.Closed) { cnSQLData.Close(); }
            return dtCustomers;
        }
        public int doInsertTiffReq(string sPrac, string sPat, string sPracNum, string sFileName)
        {
            try
            {
                //SqlConnection cn = new SqlConnection(cnSQLData);
                SqlCommand cmd = new SqlCommand("InsertTifMetaData", cnSQLData);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;
                string sRetCode = "";
                Int32 iErrCode = 0;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Filename", sFileName);
                cmd.Parameters.AddWithValue("@PracticeID", sPrac);
                cmd.Parameters.AddWithValue("@PracticeIdentifier", sPracNum.Substring(0, 3));
                cmd.Parameters.AddWithValue("@PatientID", DBNull.Value);
                cmd.Parameters.AddWithValue("@ItemType", 0);
                cmd.Parameters.AddWithValue("@ImageCreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue(@"Data", "");
                cmd.Parameters.AddWithValue(@"Result", sRetCode);
                cmd.Parameters.AddWithValue(@"ErrorCode", iErrCode);
                cnSQLData.Open();
                cnSQLData.ChangeDatabase(sVS3DB);
                cmd.ExecuteNonQuery();
                cnSQLData.Close();
                return 0;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return 1;
            }
        }
    }
}
