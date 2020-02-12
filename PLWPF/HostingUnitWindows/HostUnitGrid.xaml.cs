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
using System.Reflection;

namespace PLWPF.HostingUnitWindows
{
    /// <summary>
    /// Interaction logic for HostUnitGrid.xaml
    /// </summary>
    public partial class HostUnitGrid : Window
    {
        List<BE.HostingUnit> list;
   
        public BE.HostingUnit selectedHU;

        public HostUnitGrid(List<BE.HostingUnit> List)
        {
            list = List;

            InitializeComponent();

             HuGrid.ItemsSource = list;

             this.DataContext = list;


        }


        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedHU = (BE.HostingUnit)HuGrid.SelectedItem;
            DialogResult = !(selectedHU == null);
            Close();
        }
    }
}
