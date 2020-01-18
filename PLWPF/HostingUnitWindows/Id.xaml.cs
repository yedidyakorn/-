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

namespace PLWPF.HostingUnitWindows
{
    /// <summary>
    /// Interaction logic for Id.xaml
    /// </summary>
    public partial class Id : Window
    {
        BE.HostingUnit hostingUnit = new BE.HostingUnit();
 

        public Id()
        {
            hostingUnit.Owner = new BE.Host();

            InitializeComponent();

            this.DataContext = hostingUnit;
        }

        #region events

        private void idButton_Click(object sender, RoutedEventArgs e)
        {

            if (hostingUnit.Owner.ID == 0)
            {
                MessageBox.Show("Enter id number", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            List<BE.HostingUnit> hostingUnits = BL_Singletone.Instance.GetHostingUnitsByOwnerId(hostingUnit.Owner.ID);

            if (hostingUnits.Count == 0)
            {
                MessageBox.Show("id not recognized", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            BE.HostingUnit host = new BE.HostingUnit
            {
                Owner = hostingUnits.First().Owner
            };

            PersonalArea personalArea = new PersonalArea(host);
            personalArea.ShowDialog();

        }
        
        #endregion

    }
}
