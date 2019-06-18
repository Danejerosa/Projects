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
using System.Windows.Shapes;

namespace YelpProject.Map
{
    /// <summary>
    /// Interaction logic for AnyMap.xaml
    /// </summary>
    public partial class AnyMap : Window
    {
        public AnyMap()
        {
            InitializeComponent();
            IntializeMap();
        }

        public MapCore IntializeMap()
        {
            newMap.Mode = new RoadMode();
            newMap.Center = new Location(15.10, 16.0);
            newMap.ZoomLevel = 5;
            Pushpin newPin = new Pushpin();
            newPin.Location = new Location(15.10, 16.0);
            newMap.Children.Add(newPin);
            return newMap;
        }
    }


}
