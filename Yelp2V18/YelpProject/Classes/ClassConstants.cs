using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.Classes
{
    class ClassConstants
    {
        //Business class constants 
        public readonly String B_id = "business_id";
        public readonly String B_name = "name";
        public readonly String B_city = "city";
        public readonly String B_state = "state";
        public readonly String B_zipcode = "zipcode";
        public readonly String B_latitude = "latitude";
        public readonly String B_longitude = "longitude";
        public readonly String B_address = "address";
        public readonly String B_reviewCount = "review_count";
        public readonly String B_numCheckins = "num_checkins";
        public readonly String B_reviewRating = "review_rating";
        public readonly String B_isOpen = "is_open";
        public readonly String B_stars = "stars";
        public readonly String B_distance = "business_distance";
        //Business table name 
        public readonly String B_table = "business";
        //Business special case
        public readonly String SPEC_id = "B.business_id";

        //User class constants 
        public readonly String U_id = "user_id";
        public readonly String U_name = "username";
        public readonly String U_avgStars = "average_stars";
        public readonly String U_cool = "cool";
        public readonly String U_funny = "funny";
        public readonly String U_useful = "useful";
        public readonly String U_fans = "fans";
        public readonly String U_reviewCount = "review_count";
        public readonly String U_yelpingSince = "yelping_since";
        public readonly String U_latitude = "user_latitude";
        public readonly String U_longitude = "user_longitude";
        //User table name 
        public readonly String U_table = "userinfo";

        //Review class constants 
        public readonly String R_id = "review_id";
        public readonly String R_reviewStars = "review_stars";
        public readonly String R_date = "review_date";
        public readonly String R_actDate = "actual_review_date";
        public readonly String R_user = "user_id";
        public readonly String R_username = "review_username";
        public readonly String R_text = "review_text";
        public readonly String R_cool = "cool_vote";
        public readonly String R_funny = "funny_vote";
        public readonly String R_useful = "useful_vote";
        //Review table name
        public readonly String R_table = "review";

        //Category class constants 
        public readonly String CAT_name = "category_name";
        //Category table name 
        public readonly String CAT_table = "categories";

        //Attribute class constants 
        public readonly String ATTR_name = "attr_name";
        public readonly String ATTR_val = "attr_value";
        //Attribute table name 
        public readonly String ATTR_table = "attributes";

        //Hours class constants 
        public readonly String HRS_day = "hours_day";
        public readonly String HRS_close = "hours_closed";
        public readonly String HRS_open = "hours_open";
        //Hours table name 
        public readonly String HRS_table = "hours";

        //Checkins class constants 
        public readonly String CHK_day = "checkin_day";
        public readonly String CHK_time = "checkin_time";
        public readonly String CHK_count = "checkin_count";
        //Checkins table name
        public readonly String CHK_table = "checkins";

        //Friends class constants 
        public readonly String FR_uid = "user_id";
        public readonly String FR_id = "friend_id";
        public readonly String FR_table = "friends";

        //Favorites class constants
        public readonly String FAV_uid = "user_id";
        public readonly String FAV_bid = "business_id";
        public readonly String FAV_table = "favorites";

        //Attribute attr_names
        public readonly String ATTR_card = "BusinessAcceptsCreditCards";
        public readonly String ATTR_reservation = "RestaurantsReservations";
        public readonly String ATTR_wheelchair = "WheelchairAccessible";
        public readonly String ATTR_outdoor = "OutdoorSeating";
        public readonly String ATTR_kids = "GoodForKids";
        public readonly String ATTR_groups = "RestaurantsGoodForGroups";
        public readonly String ATTR_delivery = "RestaurantsDelivery";
        public readonly String ATTR_takeout = "RestaurantsTakeOut";
        public readonly String ATTR_wifi = "WiFi";
        public readonly String ATTR_bike = "BikeParking";
        public readonly String ATTR_breakfast = "breakfast";
        public readonly String ATTR_brunch = "brunch";
        public readonly String ATTR_lunch = "lunch";
        public readonly String ATTR_dinner = "dinner";
        public readonly String ATTR_dessert = "dessert";
        public readonly String ATTR_latenight = "latenight";
        public readonly String ATTR_pricerange = "RestaurantsPriceRange2";

        //Business Order Combo Box constants
        public readonly String ORD_name = "name";
        public readonly String ORD_stars = "stars";
        public readonly String ORD_reviwed = "review_count";
        public readonly String ORD_rating = "review_rating";
        public readonly String ORD_checkin = "num_checkins";
        public readonly String ORD_distance = "business_distance"; 
    }
}
