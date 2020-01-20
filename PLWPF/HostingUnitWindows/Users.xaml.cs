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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
        }

        private void newUsButton_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit hostingUnit = new HostingUnit(Mode.Add);
            hostingUnit.ShowDialog();
        }

        private void oldUsButton_Click(object sender, RoutedEventArgs e)
        {
            Id id = new Id();
            id.ShowDialog();
        }
    }
}
