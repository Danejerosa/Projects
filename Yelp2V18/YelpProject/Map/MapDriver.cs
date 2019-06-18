using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Windows.Media;
using BingMapsRESTToolkit;

namespace YelpProject.Map
{
    class MapDriver
    {

        private MapCore newMap;
        private System.Windows.Controls.Label newLabel;
        private double distance;

        public MapDriver(MapCore map) {
            newMap = map;
        }

        public MapDriver() { }

        public void InitializeMap()
        {
            newMap.Mode = new AerialMode(true);
            newMap.ZoomLevel = 2;
        }

        public void AddPin(Microsoft.Maps.MapControl.WPF.Location newLocation, String businessName, Color color)
        {
            SolidColorBrush brushColor = new SolidColorBrush(color);
            newMap.ZoomLevel = 15;
            newMap.Center = newLocation;
            Pushpin newPin = new Pushpin()
            {
                Location = newLocation,
                Content = businessName,
                Background = brushColor,
                Foreground = brushColor,
                FontSize = 1
            };
            newPin.MouseEnter += NewPin_MouseEnter;
            newPin.MouseLeave += NewPin_MouseLeave;
            newPin.MouseDown += NewPin_MouseDown;
            newPin.MouseDoubleClick += NewPin_MouseDoubleClick;

            newMap.Children.Add(newPin);
        }

        public void SetUserLocation(Microsoft.Maps.MapControl.WPF.Location newLocation)
        {
            Pushpin newPin = new Pushpin()
            {
                Location = newLocation,
                Content = "YOU ARE HERE",
                Background = Brushes.DarkOrange,
                Foreground = Brushes.DarkOrange,
                FontSize = 1
            };
            newPin.MouseEnter += NewPin_MouseEnter;
            newMap.Children.Add(newPin);
        }



        private void NewPin_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Pushpin tempPin = sender is Pushpin ? (Pushpin)sender : null;
            AddPin(tempPin.Location, tempPin.Content.ToString(), Colors.Red);
            newMap.Children.Remove(tempPin);
            newLabel.Content = "";
            newLabel.Background = null;
            newLabel = new System.Windows.Controls.Label();
        }

        private void NewPin_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Pushpin tempPin = sender is Pushpin ? (Pushpin)sender : null;
            tempPin.MouseEnter -= NewPin_MouseEnter;
            tempPin.MouseLeave -= NewPin_MouseLeave;
            tempPin.Background = Brushes.Green;
            newLabel.Content = tempPin.Content + "\n (DOUBLE CLICK TO REMOVE)";
        }

        private void NewPin_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Pushpin tempPin = sender is Pushpin ? (Pushpin)sender : null;
            tempPin.Background = Brushes.Red;
            newLabel.Content = "";
            newLabel.Background = null;
        }

        private void NewPin_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Pushpin tempPin = sender is Pushpin ? (Pushpin)sender : null;
            tempPin.Background = Brushes.Aqua;
            newLabel = new System.Windows.Controls.Label();
            newLabel.Content = tempPin.Content + "\n (CLICK TO PIN)";
            newLabel.Background = new SolidColorBrush(Colors.White);
            MapLayer.SetPosition(newLabel, tempPin.Location);
            newMap.Children.Add(newLabel);
        }

        public void ClearMap()
        {
            newMap.Children.Clear();
        }

        public async void SearchAddressMap(String query)
        {
            var request = new GeocodeRequest()
            {
                Query = query,
                IncludeIso2 = true,
                IncludeNeighborhood = true,
                MaxResults = 25,
                BingMapsKey = "Akg1Bxeo9S0_umkUKYCmip6n0g37vn12fuoNW8ZKJfbGbBYaW8ngs1HCEzcVRs83"
            };

            //Process the request by using the ServiceManager.
            var response = await ServiceManager.GetResponseAsync(request);

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)

            {
                var result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;

                AddPin(new Microsoft.Maps.MapControl.WPF.Location(result.Point.GetCoordinate().Latitude,
                    result.Point.GetCoordinate().Longitude), "", Colors.Blue);
            }
        }

        public async void CalculateDistance(Microsoft.Maps.MapControl.WPF.Location a, Microsoft.Maps.MapControl.WPF.Location b)
        {
            SimpleWaypoint place1 = new SimpleWaypoint(a.Latitude,a.Longitude);
            SimpleWaypoint place2 = new SimpleWaypoint(b.Latitude, b.Longitude);
            

            var request = new DistanceMatrixRequest()
            {
                Origins = new List<SimpleWaypoint>() { place1 },
                Destinations = new List<SimpleWaypoint>() { place2 },
                DistanceUnits = DistanceUnitType.Miles,
                BingMapsKey = "Akg1Bxeo9S0_umkUKYCmip6n0g37vn12fuoNW8ZKJfbGbBYaW8ngs1HCEzcVRs83"
            };
 
            var testDistc = await request.GetEuclideanDistanceMatrix();
            distance = testDistc.GetDistance(0, 0);
            
        }

        public double GetDistance()
        {
            return distance;
        }

        
    }
}
