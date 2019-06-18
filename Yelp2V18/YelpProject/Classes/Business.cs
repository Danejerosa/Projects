using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.Classes
{
    class Business
    {
        public String business_id { get; set; }
        public String name { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String zipcode { get; set; }
        public String address { get; set; }
        public double latitute { get; set; }
        public double longtitude { get; set; }
        public double stars { get; set; }
        public double review_rating { get; set; }
        public String business_distance { get; set; }
        public double review_count { get; set; }
        public int num_checkins { get; set; } 
        public Boolean is_open { get; set; }


        public Business() { }
    }
}
