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
    class blogController
    {
        public List<blogModel> getUsersBlogList(blogModel blogParams)
        {
            using (DAL db = new DAL())
            {
                List<blogModel> lsblog = new List<blogModel>();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetUsersBlogList";
                    cmd.Parameters.AddWithValue("@showOnHomePage", Convert.ToBoolean(blogParams.onHomePage));
                    DataSet ds = db.ReturnDataset(cmd);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        blogModel data = new blogModel();
                        lsblog.Add(data);
                    }
                    return (lsblog);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<blogModel> getAdminBlogsList()
        {
            using (DAL db = new DAL())
            {
                List<blogModel> lsblog = new List<blogModel>();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetAdminBlogsList";
                    DataSet ds = db.ReturnDataset(cmd);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        blogModel data = new blogModel();
                        lsblog.Add(data);
                    }
                    return (lsblog);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
