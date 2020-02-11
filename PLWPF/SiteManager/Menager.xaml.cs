using PLWPF.DataGrids;
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

namespace PLWPF.SiteManager
{
    /// <summary>
    /// Interaction logic for Menager.xaml
    /// </summary>
    public partial class Menager : Window
    {
        public Menager()
        {
            InitializeComponent();
        }

        private void btnQur_Click(object sender, RoutedEventArgs e)
        {
            ManagerQueries managerQueries = new ManagerQueries();
            managerQueries.ShowDialog(); 
        }

        private void btnFee_Click(object sender, RoutedEventArgs e)
        {
            FeeCalc feeCalc = new FeeCalc();
            feeCalc.ShowDialog();
        }
    }
}
