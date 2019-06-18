using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting; 

namespace YelpProject.GUISetup
{
    class WindowSetup
    {
        private Window newWindow;
        Chart newChart;
        List<KeyValuePair<string, int>> chartData;

        public WindowSetup(String title, List<Classes.Review> review) {
            newWindow = new Window();
            newWindow.Title = title;
            newWindow.ResizeMode = 0;
            newWindow.SizeToContent = SizeToContent.WidthAndHeight;
            AddDataGrid(review);
        }

        public WindowSetup(String title, List<Classes.Checkins> checkinList)
        {
            newWindow = new Window();
            newWindow.Title = title;
            newWindow.ResizeMode = 0;
            newWindow.SizeToContent = SizeToContent.WidthAndHeight;
            MakeChart(checkinList);
        }

        private void AddDataGrid(List<Classes.Review> review)
        {
            DataGrid reviewDataGrid = new DataGrid();
            StackPanel panel = new StackPanel();
            SetDataBinding(reviewDataGrid);
            foreach (Classes.Review item in review)
                reviewDataGrid.Items.Add(item);
            panel.Children.Add(reviewDataGrid);
            newWindow.Content = panel;
        }

        private void SetDataBinding(DataGrid dg)
        {
            Classes.ClassConstants c = new Classes.ClassConstants();
            GUISetup.DataGridSetup gridSetup = new DataGridSetup();
            Dictionary<string, List<string>> binding = new Dictionary<string, List<string>>();
            binding.Add("Date", new List<string>() { c.R_date, "100" });
            binding.Add("User Name", new List<string>() { c.R_username, "100" });
            binding.Add("Stars", new List<string>() { c.R_reviewStars, "100" });
            binding.Add("Text", new List<string>() { c.R_text, "500" });
            binding.Add("Funny", new List<string>() { c.R_funny, "65" });
            binding.Add("Useful", new List<string>() { c.R_useful, "65" });
            binding.Add("Cool", new List<string>() { c.R_cool, "65" });
            gridSetup.AddColumnToGrid(dg, binding);
        }

        public void MakeChart(List<Classes.Checkins> checkinList)
        {
            newChart = new Chart() {
                Height = 400,
                Width = 1100,
                Title = "Number of Checkins Per Day of the Week"
            };
            ColumnChart( checkinList);
            ColumnSeries newSeries = new ColumnSeries()
            {
                ItemsSource = chartData,
                DependentValuePath = "Value",
                IndependentValuePath = "Key", 
                Title = "# of Checkins"
            };    
            newChart.Series.Add(newSeries);
            StackPanel panel = new StackPanel();
            panel.Children.Add(newChart);
            newWindow.Content = panel;
        }

        public void ColumnChart(List<Classes.Checkins> checkinList)
        {
            Classes.Checkins GetSort = new Classes.Checkins();
            checkinList = GetSort.SortDays(checkinList);
            chartData = new List<KeyValuePair<string, int>>();
            if(checkinList != null)
            {
                foreach (Classes.Checkins item in checkinList)
                {
                    chartData.Add(new KeyValuePair<string, int>(item.checkin_day, item.checkin_count));
                }
            }
            newChart.DataContext = chartData;   
        }


        public Window GetWindow()
        {
            return newWindow;
        }
    }
}
