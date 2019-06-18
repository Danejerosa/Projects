using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YelpProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeMap();
            InitializeFriendGrid();
            InitializeFriendReviewGrid();
            IntializeSearchBusinessGrid();
            InitializeFavoriteBusinessGrid();
            InitializeFriendBusinessReviewGrid();
            InitializeLogInBusinessReviewGrid();
            LoadState();
        }

        SQL.IllegalInput inputChk = new SQL.IllegalInput();
        GUISetup.QuerySetup query = new GUISetup.QuerySetup();
        GUISetup.DataGridSetup grid = new GUISetup.DataGridSetup();
        Classes.ClassConstants c = new Classes.ClassConstants();

        /**
         * Description: Container class object for friend review.
         *              Temporary for now until a better solution is found. 
         */ 
        private class FriendReview
        {
            public String Rusername { get; set; }
            public String Rname { get; set; }
            public String Rcity { get; set; }
            public String Rtext { get; set; }
            public String Rdate { get; set; }
        }

        private void InitializeMap()
        {
            Map.MapDriver test = new Map.MapDriver(myMap);
            test.ClearMap();
            test.InitializeMap();
        }

        private void AddPin(Location newLocation, String business, Color color)
        {
            Map.MapDriver test = new Map.MapDriver(myMap);
            test.AddPin(newLocation, business, color);
        }

        private void InitializeFriendGrid()
        {
            Dictionary<string, List<string>> binding = new Dictionary<string, List<string>>();
            binding.Add("Name", new List<string>() {c.U_name, "100"});
            binding.Add("Avg Stars", new List<string>() { c.U_avgStars, "100" });
            binding.Add("Yelping Since", new List<string>() { c.U_yelpingSince, "70" });
            grid.AddColumnToGrid(friendDataGrid, binding);
        }

        private void InitializeFriendReviewGrid()
        {
            Dictionary<string, List<string>> binding = new Dictionary<string, List<string>>();
            binding.Add("User Name", new List<string>() { "Rusername", "100" });
            binding.Add("Business", new List<string>() { "Rname", "100" });
            binding.Add("City", new List<string>() { "Rcity", "100" });
            binding.Add("Text", new List<string>() { "Rtext", "500" });
            binding.Add("Date", new List<string>() { "Rdate", "80" });
            grid.AddColumnToGrid(friendReviewDataGrid, binding);
        }

        private void IntializeSearchBusinessGrid()
        {
            Dictionary<string, List<string>> binding = new Dictionary<string, List<string>>();
            binding = new Dictionary<string, List<string>>();
            binding.Add("BusinessName", new List<String> { c.B_name, "150" });
            binding.Add("Address", new List<String> { c.B_address, "150" });
            binding.Add("City", new List<String> { c.B_city, "100" });
            binding.Add("State", new List<String> { c.B_state, "50" });
            binding.Add("Distance (Miles)", new List<String> { c.B_distance, "100" });
            binding.Add("Stars", new List<String> { c.B_stars, "50" });
            binding.Add("# of Reviews", new List<String> { c.B_reviewCount, "50" });
            binding.Add("Avg Review Rating", new List<String> { c.B_reviewRating, "50" });
            binding.Add("Total Checkins", new List<String> { c.B_numCheckins, "50" });
            grid.AddColumnToGrid(searchDataGrid, binding);
        }

        private void InitializeFavoriteBusinessGrid()
        {
            Dictionary<string, List<string>> binding = new Dictionary<string, List<string>>();
            binding.Add("Business Name", new List<String>() { c.B_name, "150" });
            binding.Add("Stars", new List<String> { c.B_stars, "50" });
            binding.Add("City", new List<String> { c.B_city, "150" });
            binding.Add("Zipcode", new List<String> { c.B_zipcode, "75" });
            binding.Add("Address", new List<String> { c.B_address, "150" });
            grid.AddColumnToGrid(favoriteDataGrid, binding);
        }

        private void InitializeFriendBusinessReviewGrid()
        {
            Dictionary<string, List<string>> binding = new Dictionary<string, List<string>>();
            binding.Add("Username", new List<String>() { c.R_username, "150" });
            binding.Add("Date", new List<String> { c.R_actDate, "100" });
            binding.Add("Text", new List<String> { c.R_text, "500" });
            grid.AddColumnToGrid(businessFriendReviewDataGrid, binding);
        }

        public void InitializeLogInBusinessReviewGrid()
        {
            Dictionary<string, List<string>> binding = new Dictionary<string, List<string>>();
            binding.Add("Date", new List<string>() { c.R_date, "100" });
            binding.Add("User Name", new List<string>() { c.R_username, "100" });
            binding.Add("Stars", new List<string>() { c.R_reviewStars, "100" });
            binding.Add("Text", new List<string>() { c.R_text, "500" });
            binding.Add("Funny", new List<string>() { c.R_funny, "65" });
            binding.Add("Useful", new List<string>() { c.R_useful, "65" });
            binding.Add("Cool", new List<string>() { c.R_cool, "65" });
            grid.AddColumnToGrid(businessLogInReviewDataGrid, binding);
        }

        private void UserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUser();
            latTextBox.IsEnabled = false;
            longTextBox.IsEnabled = false;
            //searchDataGrid.Items.Clear();
            //favoriteDataGrid.Items.Clear();
        }

        


        private void BusinessLogInTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadBusiness();
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUserDetails();
            LoadUserFriends();
            LoadUserFriendReviews();
            LoadFavorites();
            latTextBox.IsEnabled = false;
            longTextBox.IsEnabled = false;
            editButton.IsEnabled = true;
            updateButton.IsEnabled = true;
            //searchDataGrid.Items.Clear();
        }

        private void BusinessLogInListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetLoginBusinessDetails();
            GetLoginBusinessReviews();
            GetLoginBusinessCategories();
            GetLoginBusinessAttributes();
            GetLoginBusinessHours(dayComboBox.Text);
        }

        private void StateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCity();
            categoryListBox.Items.Clear();
            categorySearchListBox.Items.Clear();
        }

        private void CityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadZipcode();
            categoryListBox.Items.Clear();
            categorySearchListBox.Items.Clear();
        }

        private void ZipcodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCategories();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (categoryListBox.SelectedIndex > -1 && !categorySearchListBox.Items.Contains(categoryListBox.SelectedItem))
            {
                String item = categoryListBox.SelectedItem.ToString();
                categorySearchListBox.Items.Add(item);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            categorySearchListBox.Items.Remove(categorySearchListBox.SelectedItem);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            latTextBox.IsEnabled = true;
            longTextBox.IsEnabled = true;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        { 
            double userLat;
            double userLong;
            if (double.TryParse(latTextBox.Text, out userLat) && double.TryParse(longTextBox.Text, out userLong)) 
            {
                latTextBox.IsEnabled = false;
                longTextBox.IsEnabled = false;
                int status = query.UpdateUserLocation(new List<string>() { latTextBox.Text, longTextBox.Text });
                GUISetup.DialogSetup newDialog = new GUISetup.DialogSetup(status, "Update Location");
            }
            else
            {
                latTextBox.Focus();
                longTextBox.Focus();
            }
        }

        private void SearchDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            reviewTextBox.IsEnabled = true;
            query.SetCurrentBusiness((Classes.Business)searchDataGrid.SelectedItem);
            GetCurrentBusinessDetails();
            GetCurrentBusinessFriendReviews();
        }

        private void CheckinButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchDataGrid.Items.Count > 0)
            {
                InsertCheckins();
                LoadSearchedBusinesses();
            }
        }

        private void ShowReviewButton_Click(object sender, RoutedEventArgs e)
        {
            GetBusinessReview();
        }

        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchDataGrid.Items.Count > 0)
            {
                AddReview();
                LoadSearchedBusinesses();
            }
        }

        private void AddToFavButton_Click(object sender, RoutedEventArgs e)
        {
            AddToFavorites();
        }

        private void FavButton_Click_1(object sender, RoutedEventArgs e)
        {
            RemoveFromFavorites();
        }

        private void ShowCheckinButton_Click(object sender, RoutedEventArgs e)
        {
            GetCheckins();
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(searchDataGrid.Items.Count > 0)
            {
                LoadSearchedBusinesses();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadSearchedBusinesses();
        }

        private void LoadDayComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //fix this. Doesnt get the correct day
            GetLoginBusinessHours(dayComboBox.Text);
        }

        private void BusinessLogInViewCheckinButton_Click(object sender, RoutedEventArgs e)
        {
            GetLogInBusinessCheckins();
        }

        private void LoadCategories()
        {
            if (stateComboBox.SelectedIndex > -1 && cityListBox.SelectedIndex > -1 && zipcodeListBox.SelectedIndex > -1)
            {
                categoryListBox.Items.Clear();
                categorySearchListBox.Items.Clear();
                List<Object[]> newObject = query.GetCategories(new List<string>() {stateComboBox.SelectedItem.ToString(),
                    cityListBox.SelectedItem.ToString(), zipcodeListBox.SelectedItem.ToString()});
                for (int i = 0; i < newObject.Count; i++)
                {
                    categoryListBox.Items.Add(newObject[i][0]);
                }
            }
        }

        private void LoadState()
        {
            stateComboBox.Items.Clear();
            List<Object[]> newObject = query.GetStates();
            for (int i = 0; i < newObject.Count; i++)
                stateComboBox.Items.Add(newObject[i][0]);
        }

        private void LoadCity()
        {
            if(stateComboBox.SelectedIndex > -1)
            {
                cityListBox.Items.Clear();
                zipcodeListBox.Items.Clear();
                List<Object[]> newObject = query.GetCities(stateComboBox.SelectedItem.ToString());
                for (int i = 0; i < newObject.Count; i++)
                    cityListBox.Items.Add(newObject[i][0]);
            }
        }

        private void LoadZipcode()
        {
            if(stateComboBox.SelectedIndex > -1 && cityListBox.SelectedIndex > -1)
            {
                zipcodeListBox.Items.Clear();
                List<Object[]> newObject = query.GetZipcodes(new List<string>() { stateComboBox.SelectedItem.ToString(),
                    cityListBox.SelectedItem.ToString() });
                for (int i = 0; i < newObject.Count; i++)
                    zipcodeListBox.Items.Add(newObject[i][0]);
            }
        }

        private void LoadUser()
        {
            String username = userTextBox.Text;
            if (inputChk.SQLInjectionCheck(username))
            {
                userListBox.Items.Clear();
                List<Object[]> newObject = query.GetUser(username);
                for (int i = 0; i < newObject.Count; i++)
                    userListBox.Items.Add(newObject[i][0]);
            }
        }

        private void LoadUserDetails()
        {
            if (userListBox.SelectedIndex > -1)
            {
                Classes.User currUser = query.GetUserDetails(userListBox.SelectedItem.ToString());
                nameTextBox.Text = currUser.username;
                starsTextBox.Text = currUser.average_stars.ToString();
                fansTextBox.Text = currUser.fans.ToString();
                yelpingTextBox.Text = currUser.yelping_since.ToString();
                funnyTextBox.Text = currUser.funny.ToString();
                coolTextBox.Text = currUser.cool.ToString();
                usefulTextBox.Text = currUser.useful.ToString();
                latTextBox.Text = currUser.user_latitude.ToString();
                longTextBox.Text = currUser.user_longitude.ToString();
                query.SetCurrentUser(currUser);
            }
        }

        private void LoadUserFriends()
        {
            friendDataGrid.Items.Clear();
            List<Object[]> newObject = query.GetFriends();
            foreach(Object[] item in newObject)
            {
                Classes.User friend = query.GetUserDetails(item[0].ToString());
                friendDataGrid.Items.Add(friend);
            }
        }

        private void LoadUserFriendReviews()
        {
            friendReviewDataGrid.Items.Clear();
            List<Object[]> newObject = query.GetFriendReview(query.GetFriends());
            foreach (Object[] reviewObj in newObject)
            {
                friendReviewDataGrid.Items.Add(new FriendReview() { Rusername = reviewObj[0].ToString(),
                    Rname = reviewObj[1].ToString(), Rcity = reviewObj[2].ToString(), Rtext = reviewObj[3].ToString(),
                    Rdate = DateTime.Parse(reviewObj[4].ToString()).ToString("d")});
            }
        }

        //For business information
        private void LoadBusiness()
        {
            String BUsername = businessLogInTextBox.Text;
            if (inputChk.SQLInjectionCheck(BUsername))
            {
                businessLogInListBox.Items.Clear();
                List<Object[]> newObject = query.GetBusiness(BUsername);
                for (int i = 0; i < newObject.Count; i++)
                    businessLogInListBox.Items.Add(newObject[i][0]);
            }
        }

        private List<Classes.Attributes> LoadAttributes()
        {
            businessTextBox.Text = "";
            List<Classes.Attributes> attr_list = new List<Classes.Attributes>();
            if (priceOneCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_pricerange, attribute_value = "1" }); }
            if (priceTwoCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_pricerange, attribute_value = "2" }); }
            if (priceThreeCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_pricerange, attribute_value = "3" }); }
            if (priceFourCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_pricerange, attribute_value = "4" }); }
            if (creditCardCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_card, attribute_value = "True" }); }
            if (reservationCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_reservation, attribute_value = "True" }); }
            if (wheelchairCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_wheelchair, attribute_value = "True" }); }
            if (outdoorCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_outdoor, attribute_value = "True" }); }
            if (kidsCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_kids, attribute_value = "True" }); }
            if (groupCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_groups, attribute_value = "True" }); }
            if (deliveryCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_delivery, attribute_value = "True" }); }
            if (takeOutCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_takeout, attribute_value = "True" }); }
            if (wifiCheckBox.IsChecked ?? false) {attr_list.Add(new Classes.Attributes()
                { attribute_name = c.ATTR_wifi, attribute_value = "free" }); }
            if (bikeCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_bike, attribute_value = "True" }); }
            if (breakfastCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_breakfast, attribute_value = "True" }); }
            if (brunchCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_brunch, attribute_value = "True" }); }
            if (lunchCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_lunch, attribute_value = "True" }); }
            if (dinnerCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_dinner, attribute_value = "True" }); }
            if (dessertCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_dessert, attribute_value = "True" }); }
            if (lateNightCheckBox.IsChecked ?? false) { attr_list.Add(new Classes.Attributes()
            { attribute_name = c.ATTR_latenight, attribute_value = "True" }); }
            return attr_list;
        }

        

        //for search business 
        private void LoadSearchedBusinesses()
        {
            if (stateComboBox.SelectedIndex > -1 && cityListBox.SelectedIndex > -1 && zipcodeListBox.SelectedIndex > -1)
            {
                InitializeMap();
                searchDataGrid.Items.Clear();
                List<Classes.Business> businessList = query.GetBusinesses(LoadAttributes(), new List<string>() {stateComboBox.SelectedItem.ToString(),
                    cityListBox.SelectedItem.ToString(), zipcodeListBox.SelectedItem.ToString()}, GetSearchCategories(), GetBusinessOrder());
                foreach (Classes.Business business in businessList)
                {
                    searchDataGrid.Items.Add(business);
                    AddPin(new Location(business.latitute, business.longtitude), business.name, Colors.Red);
                }
                businessNumLabel.Content = businessList.Count;
            }

        }

        private void SearchMapButton_Click(object sender, RoutedEventArgs e)
        {
            if(searchMapTextBox.Text.Length > 0)
            {
                SearchMapAddress(searchMapTextBox.Text);
            }
        }

        private void SearchMapAddress(String address)
        {
            Map.MapDriver test = new Map.MapDriver(myMap);
            //test.InitializeMap();
            test.SearchAddressMap(address); 
        }

        
        // try to get this on query setup
        private void GetCurrentBusinessDetails()
        {
            if(searchDataGrid.SelectedIndex > -1)
            {
                Classes.Business currentBusiness = (Classes.Business)searchDataGrid.SelectedItem;
                addressTextBox.Text = currentBusiness.address;
                businessTextBox.Text = currentBusiness.name;
                List<Object[]> newObject = query.GetCurrentBusinessCategories(currentBusiness.business_id);
                StringBuilder sb = new StringBuilder();
                foreach (Object[] item in newObject)
                {
                    sb.Append(item[0] + "\n");
                }
                categoryTextBox.Text = sb.ToString();
                sb = new StringBuilder();
                newObject = query.GetCurrentBusinessAttributes(currentBusiness.business_id);
                foreach (Object[] item in newObject)
                {
                    if (!item[1].ToString().Equals("False"))
                    {
                        if (item[1].ToString().Equals("True"))
                            sb.AppendLine(item[0].ToString());
                        else
                            sb.AppendLine(item[0].ToString() + " : " + item[1].ToString());
                    }
                    //sb.Append(item[0] + " : " + item[1]+ "\n");
                }
                attributesTextBox.Text = sb.ToString();
                sb = new StringBuilder();
                DateTime today = DateTime.Now;
                Classes.Hours newHours = query.GetCurrentBusinessHours(currentBusiness.business_id, today.ToString("dddd"));
                if(newHours != null)
                {
                    sb.Append("Today(" + newHours.hours_day + ")\n");
                    sb.Append("Opens: " + newHours.hours_open.ToString("hh:mm") + "\n");
                    sb.Append("Closed: " + newHours.hours_close.ToString("hh:mm"));
                    
                }
                else
                {
                    sb.Append("Today(" + today.ToString("dddd") + ")\n");
                    sb.Append("Opens: N/A \n");
                    sb.Append("Closed: N/A");
                }
                hoursTextBox.Text = sb.ToString();
                AddPin(new Location(currentBusiness.latitute, currentBusiness.longtitude), currentBusiness.name, Colors.Green);
            }
        }

        private void GetCurrentBusinessFriendReviews()
        {
            if (searchDataGrid.SelectedIndex > -1)
            {
                businessFriendReviewDataGrid.Items.Clear();
                List<Object[]> newObject = query.GetFriends();
                if (newObject != null)
                {
                    Classes.Business tempBusiness = (Classes.Business)favoriteDataGrid.SelectedItem;
                    foreach (Object[] item in newObject)
                    {
                        Classes.Review newReview = query.GetBusinessFriendReviews(item[0].ToString());
                        if(newReview != null)
                            businessFriendReviewDataGrid.Items.Add(newReview);
                    }
                }
            }
        }

        private List<String> GetSearchCategories()
        {
            List<String> catList = new List<String>();
            if (categorySearchListBox.Items.Count > 0)
                foreach (String item in categorySearchListBox.Items)
                    catList.Add(item);
            return catList;
        }


        private void InsertCheckins()
        {
            if (searchDataGrid.SelectedIndex > -1)
            {
                Classes.Business currentBusiness = (Classes.Business)searchDataGrid.SelectedItem;
                query.InsertCheckins(currentBusiness);
            }
        }

        

        private void GetBusinessReview()
        {
            if (searchDataGrid.SelectedIndex > -1)
            {
                Window newWindow = new GUISetup.WindowSetup("Review by Users",
                    query.GetBusinessReviews((Classes.Business)searchDataGrid.SelectedItem)).GetWindow();
                newWindow.Show();
            }
        }


        private void AddReview()
        {
            if (searchDataGrid.SelectedIndex > -1)
            {
                String indexx = (ratingComboBox.SelectedIndex + 1).ToString();
                query.InsertReview(reviewTextBox.Text, (Classes.Business)searchDataGrid.SelectedItem, indexx);
            }
        }

        

        private void AddToFavorites()
        {
            if (searchDataGrid.SelectedIndex > -1)
            {
                if (query.CheckFavoritesBeforeAdd() == 0)
                {
                    query.InsertToFavorites((Classes.Business)searchDataGrid.SelectedItem);
                    LoadFavorites();
                }
                else
                    query.ShowStatusUpdate(0, "Already added, Add To Favorites");
            }
        }

        private void LoadFavorites()
        {
            favoriteDataGrid.Items.Clear();
            List<Object[]> businessList = query.GetUserFavorites();
            if(businessList != null)
            {
                foreach (Object[] item in businessList)
                {
                    Classes.Business tempBusiness = query.GetBusinessDetails(item[0].ToString());
                    favoriteDataGrid.Items.Add(tempBusiness);
                }
            } 
        }

        private void RemoveFromFavorites()
        {
            if(favoriteDataGrid.SelectedIndex > -1)
            {
                Classes.Business tempBusiness = (Classes.Business)favoriteDataGrid.SelectedItem;
                int updated = query.RemoveBusiness(tempBusiness.business_id);
                if (updated == 1)
                    LoadFavorites();
            }
        }

       

        private void GetCheckins()
        {
            if(searchDataGrid.SelectedIndex > -1)
            {
                List<Classes.Checkins> checkinList = new List<Classes.Checkins>();
                List<Object[]> newObject = query.GetBusinessCheckinDays((Classes.Business)searchDataGrid.SelectedItem);
                foreach(Object[] checkinDay in newObject)
                {
                    checkinList.Add(query.GetBusinessCheckins((Classes.Business)searchDataGrid.SelectedItem,checkinDay[0].ToString()));
                }
                GUISetup.WindowSetup createChart = new GUISetup.WindowSetup("My New Chart", checkinList);
                Window newChartWindow = createChart.GetWindow();
                newChartWindow.Show();
            } 
        }

        private List<string> GetBusinessOrder()
        {
            int item = sortByComboBox.SelectedIndex;
            List<string> orderList= query.GetOrder(item);
            return orderList;
        }

        public void GetLoginBusinessDetails()
        {
            if(businessLogInListBox.SelectedIndex > -1)
            {
                Classes.Business tempBusiness = query.GetBusinessDetails(businessLogInListBox.SelectedItem.ToString());
                businessLogInNameTextBox.Text = tempBusiness.name;
                businessLogInAddressTextBox.Text = tempBusiness.address;
                businessLogInStateTextBox.Text = tempBusiness.state;
                businessLogInCityTextBox.Text = tempBusiness.city;
                businessLogInZipTextBox.Text = tempBusiness.zipcode;
                businessLogInStarsTextBox.Text = tempBusiness.stars.ToString();
                businessLogInCountTextBox.Text = tempBusiness.review_count.ToString();
                businessLogInRatingTextBox.Text = tempBusiness.review_rating.ToString();
                businessLogInCheckinsTextBox.Text = tempBusiness.num_checkins.ToString();
                businessLatitudeTextBox.Text = tempBusiness.latitute.ToString();
                businessLongitudeTextBox.Text = tempBusiness.longtitude.ToString();
                query.SetLoggedInBusiness(tempBusiness);
            }
        }

        public void GetLoginBusinessReviews()
        {
            if (businessLogInListBox.SelectedIndex > -1 && query.GetLoggedInBusiness() != null)
            {
                Classes.Business currLoginBusiness = query.GetLoggedInBusiness();
                List<Classes.Review> reviewList = query.GetBusinessReviews(currLoginBusiness);
                if(reviewList.Count > 0)
                {
                    foreach (Classes.Review item in reviewList)
                    {
                        businessLogInReviewDataGrid.Items.Add(item);
                    }
                }
            }
        }

        public void GetLoginBusinessCategories()
        {
            if (businessLogInListBox.SelectedIndex > -1 && query.GetLoggedInBusiness() != null)
            {
                Classes.Business currLoginBusiness = query.GetLoggedInBusiness();
                List<Object[]> newObject = query.GetCategories(new List<String>() {currLoginBusiness.state,
                    currLoginBusiness.city, currLoginBusiness.zipcode });
                if(newObject.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Object[] item in newObject)
                    {
                        sb.AppendLine(item[0].ToString());
                    }
                    loggedInCategories.Text = sb.ToString();
                }
            }
        }

        public void GetLoginBusinessAttributes()
        {
            if (businessLogInListBox.SelectedIndex > -1 && query.GetLoggedInBusiness() != null)
            {
                Classes.Business currLoginBusiness = query.GetLoggedInBusiness();
                List<Object[]> newObject = query.GetCurrentBusinessAttributes(currLoginBusiness.business_id);
                if (newObject.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Object[] item in newObject)
                    {
                        if(!item[1].ToString().Equals("False"))
                        {
                            if(item[1].ToString().Equals("True"))
                                sb.AppendLine(item[0].ToString());
                            else
                                sb.AppendLine(item[0].ToString() + " : " + item[1].ToString());
                        }  
                    }
                    loggedInAttributes.Text = sb.ToString();
                }
            }
        }

        public void GetLoginBusinessHours(String currentDay)
        {
            if (businessLogInListBox.SelectedIndex > -1 && query.GetLoggedInBusiness() != null)
            {
                Classes.Business currLoginBusiness = query.GetLoggedInBusiness();
                Classes.Hours newHour = query.GetCurrentBusinessHours(currLoginBusiness.business_id, currentDay);
                StringBuilder sb = new StringBuilder();
                if (newHour != null)
                {
                    sb.Append("Opens: " + newHour.hours_open.ToString("hh:mm") + "\n");
                    sb.Append("Closed: " + newHour.hours_close.ToString("hh:mm"));
                }
                else
                {
                    sb.Append("Opens: N/A \n");
                    sb.Append("Closed: N/A");
                }
                loggedInHours.Text = sb.ToString();
            }
        }

        public void GetLogInBusinessCheckins()
        {
            if (businessLogInListBox.SelectedIndex > -1 && query.GetLoggedInBusiness() != null)
            {
                Classes.Business currLoginBusiness = query.GetLoggedInBusiness();
                List<Classes.Checkins> checkinList = new List<Classes.Checkins>();
                List<Object[]> newObject = query.GetBusinessCheckinDays(currLoginBusiness);
                foreach (Object[] checkinDay in newObject)
                {
                    checkinList.Add(query.GetBusinessCheckins(currLoginBusiness, checkinDay[0].ToString()));
                }
                GUISetup.WindowSetup createChart = new GUISetup.WindowSetup("My New Chart", checkinList);
                Window newChartWindow = createChart.GetWindow();
                newChartWindow.Show();
            }
        }
    }
}
