using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.Classes
{
    class User
    {
        public String user_Id { get; set; }
        public String username { get; set; }
        public String yelping_since { get; set; }
        public double average_stars { get; set; }
        public double user_latitude { get; set; }
        public double user_longitude { get; set; }
        public int fans { get; set; }
        public int cool { get; set; } 
        public int funny { get; set; }
        public int useful { get; set; }
        public int review_count { get; set; }
        List<String> friends;

        public User() { }

        public void PostReviews() { }

        public List<String> FriendsList() {
            friends = new List<String>();  
            return friends; 
        }
    }
}
