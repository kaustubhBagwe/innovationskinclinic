using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using innovationDAL.Model;

namespace innovationDAL.ActionMethod
{
    class mediaController
    {
        public List<mediaModel> getUsersMediaList(mediaModel mediaParams)
        {
            using (DAL db = new DAL())
            {
                List<mediaModel> lsmedia = new List<mediaModel>();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetUsersMediaList";
                    cmd.Parameters.AddWithValue("@showOnHomePage", Convert.ToBoolean(mediaParams.onHomePage));
                    DataSet ds = db.ReturnDataset(cmd);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        mediaModel data = new mediaModel();
                        lsmedia.Add(data);
                    }
                    return (lsmedia);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<mediaModel> getAdminMediaList()
        {
            using (DAL db = new DAL())
            {
                List<mediaModel> lsmedia = new List<mediaModel>();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetAdminMediaList";
                    DataSet ds = db.ReturnDataset(cmd);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        mediaModel data = new mediaModel();
                        lsmedia.Add(data);
                    }
                    return (lsmedia);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
