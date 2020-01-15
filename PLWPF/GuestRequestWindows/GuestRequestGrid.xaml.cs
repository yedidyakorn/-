using BE;
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

namespace PLWPF.GuestRequestWindows
{
    /// <summary>
    /// Interaction logic for GuestRequestGrid.xaml
    /// </summary>
    public partial class GuestRequestGrid : Window
    {
        List<GuestRequest> guestRequests = new List<GuestRequest>();

        public GuestRequest selectedGR;

        public GuestRequestGrid(List<GuestRequest> gRequests)
        {
            guestRequests = gRequests;

            InitializeComponent();

            GrGrid.ItemsSource = guestRequests;

          //  this.DataContext = guestRequests;

          
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedGR = (GuestRequest)GrGrid.SelectedItem;
            DialogResult = !(selectedGR == null);
            Close();          
        }
    }
}
