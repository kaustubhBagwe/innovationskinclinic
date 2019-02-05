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
    class testimonialController
    {        
        public List<testimonialModel> getTestimonials() {
            using (DAL db = new DAL()) {
                List<testimonialModel> lstestimonial = new List<testimonialModel>();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetTestimonials";
                    DataSet ds = db.ReturnDataset(cmd);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        testimonialModel data = new testimonialModel();
                        lstestimonial.Add(data);
                    }
                    return (lstestimonial);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public results saveTestimonials(testimonialModel testimonialparams) {
            using (DAL db = new DAL()) {
                results results = new results();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SaveTestimonials";
                    cmd.Parameters.AddWithValue("@Id",testimonialparams.id);
                    cmd.Parameters.AddWithValue("@Title",testimonialparams.title);
                    cmd.Parameters.AddWithValue("@Degree",testimonialparams.degree);
                    cmd.Parameters.AddWithValue("@Description",testimonialparams.description);
                    cmd.Parameters.AddWithValue("@DoctorsTestimonial",testimonialparams.doctors_testimonial);
                    cmd.Parameters.AddWithValue("@onHomePage",testimonialparams.onHomePage);
                    cmd.Parameters.AddWithValue("@imgFilepath",testimonialparams.imgfilepath);
                    cmd.Parameters.AddWithValue("@docFilepath",testimonialparams.docfilepath);
                    cmd.Parameters.AddWithValue("@SEOTitle",testimonialparams.SEOtitle);
                    cmd.Parameters.AddWithValue("@SEOMeta",testimonialparams.SEOmeta);
                    cmd.Parameters.AddWithValue("@Active",testimonialparams.active);
                    cmd.Parameters.AddWithValue("@Mode", testimonialparams.mode);

                    DataSet ds = db.ReturnDataset(cmd);

                    results.flag = Convert.ToString(ds.Tables[0].Rows[0]["Flag"]);
                    results.msg = Convert.ToString(ds.Tables[0].Rows[0]["msg"]);

                    return results;

                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }

        public results deleteTestimonials(testimonialModel testimonialparams)
        {
            using (DAL db = new DAL())
            {
                results results = new results();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_DeleteTestimonials";
                    cmd.Parameters.AddWithValue("@Id", testimonialparams.id);
                    DataSet ds = db.ReturnDataset(cmd);
                    results.flag = Convert.ToString(ds.Tables[0].Rows[0]["Flag"]);
                    results.msg = Convert.ToString(ds.Tables[0].Rows[0]["msg"]);

                    return results;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public class results {
            public string flag { get; set; }
            public string msg { get; set; }
        
        }
    }
}
