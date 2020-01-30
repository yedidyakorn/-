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
    /// Interaction logic for OrderMng.xaml
    /// </summary>
    public partial class OrderMng : Window
    {
        public OrderMng()
        {
            InitializeComponent();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            OrdersSearch ordersSearch = new OrdersSearch();
            ordersSearch.ShowDialog();
        }
    }
}
