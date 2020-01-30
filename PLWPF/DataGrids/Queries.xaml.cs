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
    /// Interaction logic for Queries.xaml
    /// </summary>
    public partial class Queries : Window
    {
        public Queries()
        {
            InitializeComponent();
        }

        private void GRArea_Click(object sender, RoutedEventArgs e)
        {
            GrByArea grByArea = new GrByArea();
            grByArea.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OrdersChart ordersChart = new OrdersChart();
            ordersChart.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OrdersSumChart ordersSumChart = new OrdersSumChart();
            ordersSumChart.ShowDialog();
        }
    }
}
