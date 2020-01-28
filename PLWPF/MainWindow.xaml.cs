using PLWPF.DataGrids;
using PLWPF.HostingUnitWindows;
using PLWPF.Orders;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Helpers.Methods.LoadHostingUnitsDairy();

            InitializeComponent();
        }

        // guest request
        private void grBtn_Click(object sender, RoutedEventArgs e)
        {
            GuestRequestWindows.GuestRequestMng guestRequestMng = new GuestRequestWindows.GuestRequestMng();
            guestRequestMng.ShowDialog();
        }

        //order
        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderMng orderMng = new OrderMng();
            orderMng.ShowDialog();
        }

        //host unit
        private void huBtn_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users();
            users.ShowDialog();
        }

        private void qrBtn_Click(object sender, RoutedEventArgs e)
        {
            Queries queries = new Queries();
            queries.ShowDialog();
        }
    }
}
