using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace innovationDAL
{
    public class DAL : IDisposable
    {
        private const int SQL_LOCK_TIMEOUT_DEFAULT = 0;
        private const string SQL_LOCK_DEFAULT_STMT = "SET LOCK_TIMEOUT {0}";

        private String pcon;
        public SqlConnection scon;

        public DAL()
        {
            try
            {
                scon = null;
                pcon = GetDefaultConString();
                scon = new SqlConnection(pcon);
                scon.Open();
            }
            catch (SqlException odbe)
            {
                scon.Close();
                scon.Dispose();
                scon = null;
            }
            finally
            {

            }
        }
        
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            int iCntAffectedRows = 0;
            try
            {
                if (cmd.Connection == null)
                {
                    cmd.Connection = scon;
                }

                iCntAffectedRows = cmd.ExecuteNonQuery();
            }
            catch (SqlException odbe)
            {
                throw odbe;
            }

            return iCntAffectedRows;
        }
        
        public bool Execute(params String[] query)
        {
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trn;

            Int32 sqlNO = 0;
            Int32 sqlTrn = 0;
            cmd.CommandTimeout = 0;

            trn = scon.BeginTransaction();
            cmd.Connection = scon;
            cmd.Transaction = trn;
            sqlNO = query.Length;
            try
            {
                while (sqlTrn < sqlNO)
                {
                    cmd.CommandText = query.GetValue(sqlTrn).ToString();
                    cmd.ExecuteNonQuery();
                    sqlTrn += 1;
                }
                trn.Commit();

                cmd = null;

                return true;
            }
            catch (Exception ex)
            {
                trn.Rollback();
                return false;
                throw ex;
            }
        }
       
        public bool Execute(ref SqlTransaction trn, params String[] query)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 sqlNO = 0;
            Int32 sqlTrn = 0;
            cmd.CommandTimeout = 0;
            cmd.Connection = scon;
            cmd.Transaction = trn;
            sqlNO = query.Length;
            try
            {
                while (sqlTrn < sqlNO)
                {
                    cmd.CommandText = query.GetValue(sqlTrn).ToString();
                    cmd.ExecuteNonQuery();
                    sqlTrn += 1;
                }

                cmd = null;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ExecuteDS(DataSet ds)
        {
            SqlTransaction trn;
            trn = scon.BeginTransaction();
            try
            {
                foreach (DataTable dt in ds.Tables)
                {
                    string strSelectedColumns = "";
                    string strTablename = dt.TableName;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strSelectedColumns = strSelectedColumns + "," + dc.ColumnName;
                    }
                    strSelectedColumns = strSelectedColumns.Substring(1);
                    string strSelect = "SELECT " + strSelectedColumns + " FROM " + strTablename + " WHERE 1=0";

                    SqlDataAdapter da = new SqlDataAdapter(strSelect, scon);
                    da.SelectCommand.Transaction = trn;
                    SqlCommandBuilder scb = new SqlCommandBuilder(da);
                    da = scb.DataAdapter;
                    da.Update(ds, strTablename);
                }
                trn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trn.Rollback();
                return false;
                throw ex;
            }
        }

        public bool ExecuteDS(DataSet ds, ref SqlTransaction trn)
        {
            try
            {
                foreach (DataTable dt in ds.Tables)
                {
                    string strSelectedColumns = "";
                    string strTablename = dt.TableName;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strSelectedColumns = strSelectedColumns + "," + dc.ColumnName;
                    }
                    strSelectedColumns = strSelectedColumns.Substring(1);
                    string strSelect = "SELECT " + strSelectedColumns + " FROM " + strTablename + " WHERE 1=0";

                    SqlDataAdapter da = new SqlDataAdapter(strSelect, scon);
                    da.SelectCommand.Transaction = trn;
                    SqlCommandBuilder scb = new SqlCommandBuilder(da);
                    da = scb.DataAdapter;
                    da.Update(ds, strTablename);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ExecuteDS(DataSet ds, ref SqlTransaction trn, SqlConnection con)
        {
            try
            {
                foreach (DataTable dt in ds.Tables)
                {
                    string strSelectedColumns = "";
                    string strTablename = dt.TableName;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strSelectedColumns = strSelectedColumns + "," + dc.ColumnName;
                    }
                    strSelectedColumns = strSelectedColumns.Substring(1);
                    string strSelect = "SELECT " + strSelectedColumns + " FROM " + strTablename + " WHERE 1=0";

                    SqlDataAdapter da = new SqlDataAdapter(strSelect, con);
                    da.SelectCommand.Transaction = trn;
                    SqlCommandBuilder scb = new SqlCommandBuilder(da);
                    da = scb.DataAdapter;
                    da.Update(ds, strTablename);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ReturnDataset(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da;
                if (cmd.Connection == null)
                {
                    cmd.Connection = scon;
                }
                cmd.CommandTimeout = 0;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public DataSet ReturnDataset(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da;
                SqlCommand cmd = new SqlCommand(strQry, scon);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ReturnDataTable(string strQry)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da;
                SqlCommand cmd = new SqlCommand(strQry, scon);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ReturnDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da;
                cmd.Connection = scon;
                cmd.CommandTimeout = 0;

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            object objResult;
            try
            {
                if (cmd.Connection == null)
                {
                    cmd.Connection = scon;
                }

                objResult = cmd.ExecuteScalar();
            }
            catch (SqlException odbe)
            {
                throw odbe;
            }
            return objResult;
        }

        public object ReturnValue(string strQry)
        {
            object objResult;
            try
            {
                SqlCommand cmd = new SqlCommand(strQry, scon);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                objResult = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objResult;
        }

        public string GetConnectionString(string servername, string userid, string pwd, string dbname, bool trustedconnection)
        {
            string connectionstring = "";
            try
            {
                if (trustedconnection)
                {
                    connectionstring = "Data Source=" + servername + ";Initial Catalog=" + dbname + ";Trusted_connection=true;";
                }
                else
                {
                    connectionstring = "Data Source=" + servername + ";Initial Catalog=" + dbname + ";uid=" + userid + ";pwd=" + pwd + ";Trusted_connection=false;";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return connectionstring;
        }

        public string GetDefaultConString()
        {
            string connectionstring = "";
            try
            {
                String sServer, sDataBase, sUid, sPwd, sTrustedConnection;
                DataSet ds = new DataSet();

                ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\connection");
                sServer = ds.Tables["DBConnection"].Rows[0]["SERVER"].ToString();
                sDataBase = ds.Tables["DBConnection"].Rows[0]["DATABASE"].ToString();
                sUid = ds.Tables["DBConnection"].Rows[0]["USER_NAME"].ToString();
                sPwd = ds.Tables["DBConnection"].Rows[0]["PASSWORD"].ToString();
                sTrustedConnection = ds.Tables["DBConnection"].Rows[0]["TrustedConnection"].ToString();

                connectionstring = GetConnectionString(sServer, sUid, sPwd, sDataBase, Convert.ToBoolean(sTrustedConnection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return connectionstring;
        }
        
        internal void ExecuteNonQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (scon.State == ConnectionState.Open)
            {
                scon.Close();
                scon.Dispose();
                SqlConnection.ClearPool(scon);
            }
        }
    }
}