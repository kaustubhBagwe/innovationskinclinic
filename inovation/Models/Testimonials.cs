using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inovation.Models
{
    public class Testimonials
    {
        public int id { get; set; }
        public string title { get; set; }
        public string degree { get; set; }
        public string description { get; set; }
        public bool doctors_testimonial { get; set; }
        public bool onHomePage { get; set; }
        public string imgfilepath { get; set; }
        public string docfilepath { get; set; }
        public string SEOtitle { get; set; }
        public string SEOmeta { get; set; }
        public bool active { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public int updatedBy { get; set; }
        public DateTime updatedOn { get; set; }

    }
}