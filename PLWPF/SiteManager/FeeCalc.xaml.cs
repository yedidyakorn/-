using BL;
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
    /// Interaction logic for FeeCalc.xaml
    /// </summary>
    public partial class FeeCalc : Window
    {
        public FeeCalc()
        {
            InitializeComponent();
        }

        private void feeTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            calc();
        }

        private void feeFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            calc();
        }

        private void calc() {

            if (checkIfValidDates())
            {
                DateTime from = feeFrom.SelectedDate.Value;
                DateTime to = feeTo.SelectedDate.Value;

                LblSum.Content = BL_Singletone.Instance.CalcFeeBetweenDates(from, to);
            } else
            {
                LblSum.Content = 0;
            }
        }

        private bool checkIfValidDates()
        {
            DateTime? from = feeFrom.SelectedDate;
            DateTime? to = feeTo.SelectedDate;

            if (from == null || to == null)
                return false;

            if (from.Value > to.Value) {
                MessageBox.Show("To date must be greater then From date");
                return false;
            }

            return true;           
        }
    }
}
