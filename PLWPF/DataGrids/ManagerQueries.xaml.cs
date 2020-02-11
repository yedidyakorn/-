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

namespace PLWPF.DataGrids
{
    /// <summary>
    /// Interaction logic for ManagerQueries.xaml
    /// </summary>
    public partial class ManagerQueries : Window
    {
        public ManagerQueries()
        {
            InitializeComponent();
        }

        private void btnOrderSts_Click(object sender, RoutedEventArgs e)
        {
            OrdersChart ordersChart = new OrdersChart();
            ordersChart.ShowDialog();
        }

        private void btnOrderDate_Click(object sender, RoutedEventArgs e)
        {
            OrdersSumChart ordersSumChart = new OrdersSumChart();
            ordersSumChart.ShowDialog();
        }
    }
}
