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
    /// Interaction logic for OrdersGrid.xaml
    /// </summary>
    public partial class OrdersGrid : Window
    {
        List<BE.Order> list;

        public BE.Order selectedOrder;

        public OrdersGrid(List<BE.Order> List)
        {
            list = List;

            InitializeComponent();

            ordersGrid.ItemsSource = list;

            this.DataContext = list;

        }


        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedOrder = (BE.Order)ordersGrid.SelectedItem;
            DialogResult = !(selectedOrder == null);
            Close();
        }
    }
}
