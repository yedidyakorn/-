using BL;
using PLWPF.Helpers;
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

namespace PLWPF.Orders
{
    /// <summary>
    /// Interaction logic for OrdersSearch.xaml
    /// </summary>
    public partial class OrdersSearch : Window
    {
        public OrdersSearch()
        {
            InitializeComponent();
        }

        private void findBtn_Click(object sender, RoutedEventArgs e)
        { 
            if (string.IsNullOrEmpty(idBox.Text))
            {
                MessageBox.Show("Id required");
                return;
            }

            long.TryParse(idBox.Text, out long id);

            List<BE.Order> Orders = new List<BE.Order>();

            if (hostRadio.IsChecked.Value)
            {

                List<BE.HostingUnit> hostingUnits = BL_Singletone.Instance.GetHostingUnitsByOwnerId(id);

                Orders = BL_Singletone.Instance.GetOrderList().Where(o =>
                {
                    return hostingUnits.Any(hu => hu.HostingUnitKey == o.HostingUnitKey);
                }).ToList();

            }
            else if (guestRadio.IsChecked.Value)
            {

                List<BE.GuestRequest> guestRequests = BL_Singletone.Instance.GetGuestRequestsById(id);

                Orders = BL_Singletone.Instance.GetOrderList().Where(o =>
                {
                    return guestRequests.Any(gr => gr.GuestRequestKey == o.GuestRequestKey);
                }).ToList();

            }

            OrdersGrid ordersGrid = new OrdersGrid(Orders);
            ordersGrid.ShowDialog();

            if(ordersGrid.DialogResult == true)
            {
                Order order = new Order(Mode.Update,  ordersGrid.selectedOrder);
                order.ShowDialog();
            }

        }
    }
}
