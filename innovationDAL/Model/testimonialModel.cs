using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovationDAL.Model
{
    public class testimonialModel
    {
        public Int64 id { get; set; }
        public string title { get; set; }
        public string degree { get; set; }
        public string description { get; set; }
        public Boolean doctors_testimonial { get; set; }
        public Boolean onHomePage { get; set; }
        public string imgfilepath { get; set; }
        public string docfilepath { get; set; }
        public string SEOtitle { get; set; }
        public string SEOmeta { get; set; }
        public Boolean active { get; set; }
        public Boolean mode { get; set; }
    }
}
