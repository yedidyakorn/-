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
        BE.HostingUnit currHostingUnit;

        public PersonalArea(BE.HostingUnit hostingUnit)
        {
            currHostingUnit = hostingUnit;

            InitializeComponent();
        }

        private void addHobutton_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit hostingUnit = new HostingUnit(currHostingUnit);
            hostingUnit.ShowDialog();
        }
    }
}
