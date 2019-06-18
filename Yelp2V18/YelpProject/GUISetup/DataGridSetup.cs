using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace YelpProject.GUISetup
{
    class DataGridSetup
    {
        public void AddColumnToGrid(DataGrid grid, Dictionary<string, List<String>> bindingDictionary)
        {
            foreach (String item in bindingDictionary.Keys)
            {
                DataGridTextColumn col = new DataGridTextColumn();
                col.Header = item;
                col.Binding = new Binding(bindingDictionary[item][0]);
                col.Width = Int32.Parse(bindingDictionary[item][1]);
                col.IsReadOnly = true;
                grid.Columns.Add(col);
            }
        }
    }
}
