using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovationDAL.Model
{
    public class blogModel
    {
        public Int64 id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string SEOtitle { get; set; }
        public string SEOmeta { get; set; }
        public Boolean onHomePage { get; set; }
        public Boolean active { get; set; }
        public Int64 createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public Int64 updatedBy { get; set; }
        public DateTime updatedOn { get; set; }
        public Boolean mode { get; set; }
    }
}
