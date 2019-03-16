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
    class enquiryController
    {
        public List<enquiryModel> getAdminEnquiryList()
        {
            using (DAL db = new DAL())
            {
                List<enquiryModel> lsenquiry = new List<enquiryModel>();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetAdminEnquriyList";
                    DataSet ds = db.ReturnDataset(cmd);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        enquiryModel data = new enquiryModel();
                        lsenquiry.Add(data);
                    }
                    return (lsenquiry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public results saveEnquiry(enquiryModel enquryParams)
        {
            using (DAL db = new DAL())
            {
                results results = new results();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SaveEnquiry";
                    cmd.Parameters.AddWithValue("@fullname",enquryParams.fullname);
                    cmd.Parameters.AddWithValue("@email",enquryParams.email);
                    cmd.Parameters.AddWithValue("@contactNumber",enquryParams.contactNumber);
                    cmd.Parameters.AddWithValue("@queryMessage",enquryParams.queryMessage);
                    DataSet ds = db.ReturnDataset(cmd);
                    results.flag = Convert.ToBoolean(ds.Tables[0].Rows[0]["Flag"]);
                    results.msg = Convert.ToString(ds.Tables[0].Rows[0]["msg"]);
                    return results;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public class results
        {
            public Boolean flag { get; set; }
            public string msg { get; set; }

        }

    }
}
