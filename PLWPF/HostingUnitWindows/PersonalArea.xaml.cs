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

namespace PLWPF.HostingUnitWindows
{
    /// <summary>
    /// Interaction logic for PersonalArea.xaml
    /// </summary>
    public partial class PersonalArea : Window
    {
        List<BE.HostingUnit> hostingUnits;

        public PersonalArea(List<BE.HostingUnit> HostingUnits)
        {
            hostingUnits = HostingUnits;

            InitializeComponent();
        }

        private void addHobutton_Click(object sender, RoutedEventArgs e)
        {
            BE.HostingUnit host = new BE.HostingUnit
            {
                Owner = hostingUnits.First().Owner
            };

            HostingUnit hostingUnit = new HostingUnit(Mode.Add, host);
            hostingUnit.ShowDialog();
        }

        private void updatHoButton_Click(object sender, RoutedEventArgs e)
        {
            HostUnitGrid hostUnitGrid = new HostUnitGrid(hostingUnits);
            hostUnitGrid.ShowDialog();

            if (hostUnitGrid.DialogResult == true)
            {
                HostingUnit hostingUnit = new HostingUnit(Mode.Update, hostUnitGrid.selectedHU);
                hostingUnit.ShowDialog();
            }
        }
    }
}
