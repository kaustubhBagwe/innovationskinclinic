using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovationDAL.Model
{
    class enquiryModel
    {
        public Int64 id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string queryMessage { get; set; }
        public string createdOn { get; set; }
    }
}
