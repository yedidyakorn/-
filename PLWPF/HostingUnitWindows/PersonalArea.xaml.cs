using BL;
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
using System.Windows.Shapes;
using Util;

namespace PLWPF.HostingUnitWindows
{
    /// <summary>
    /// Interaction logic for PersonalArea.xaml
    /// </summary>
    public partial class PersonalArea : Window
    {
        BE.HostingUnit hostingUnit = new BE.HostingUnit();

        public PersonalArea(BE.HostingUnit HostingUnit)
        {
            hostingUnit = HostingUnit;
            this.DataContext = hostingUnit;
            InitializeComponent();
        }

        private void addHobutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.HostingUnit host = new BE.HostingUnit
                {
                    Owner = hostingUnit.Owner
                };

                HostingUnit hostingUnitWin = new HostingUnit(Mode.Add, host);
                hostingUnitWin.ShowDialog();
            }
            catch (BE.LogicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("general error");
            }
        }

        private void updatHoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BE.HostingUnit> hostingUnits = BL_Singletone.Instance.GetHostingUnitsByOwnerId(hostingUnit.Owner.ID);

                HostUnitGrid hostUnitGrid = new HostUnitGrid(hostingUnits);
                hostUnitGrid.ShowDialog();

                if (hostUnitGrid.DialogResult == true)
                {
                    HostingUnit hostingUnit = new HostingUnit(Mode.Update, hostUnitGrid.selectedHU);
                    hostingUnit.ShowDialog();
                }
            }
            catch (BE.LogicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("general error");
            }
        }

        private void btnOrdr_Click(object sender, RoutedEventArgs e)
        {
            List<BE.Order> Orders = new List<BE.Order>();

            try { 

            List<BE.HostingUnit> hostingUnits = BL_Singletone.Instance.GetHostingUnitsByOwnerId(hostingUnit.Owner.ID);

             Orders = BL_Singletone.Instance.GetOrderList().Where(o =>
            {
                return hostingUnits.Any(hu => hu.HostingUnitKey == o.HostingUnitKey);
            }).ToList();
            }
            catch(BE.LogicException ex)
            {
                MessageBox.Show(ex.Message);
            }

            OrdersGrid ordersGrid = new OrdersGrid(Orders);
            ordersGrid.ShowDialog();

            if (ordersGrid.DialogResult == true)
            {
                Order order = new Order(Mode.Update, ordersGrid.selectedOrder);
                order.ShowDialog();
            }

        }
    }
}
