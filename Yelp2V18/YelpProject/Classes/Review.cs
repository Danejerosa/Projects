using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.Classes
{
    class Review
    {
        public String review_id { get; set; }
        public String review_text { get; set; }
        public String review_date { get; set; }
        public String user_id { get; set; }
        public String review_username { get; set; }
        public int review_stars { get; set; }
        public int useful_vote { get; set; }
        public int funny_vote { get; set; }
        public int cool_vote { get; set; }
        public String actual_review_date { get; set; }
    

        public Review() { }

        
    }
}
