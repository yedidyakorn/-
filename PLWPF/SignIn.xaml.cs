using BL;
using PLWPF.GuestRequestWindows;
using PLWPF.HostingUnitWindows;
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
using Util;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                GuestRequestWindow guestRequest = new GuestRequestWindow(Mode.Add);
                guestRequest.ShowDialog();
            }
        }

        private void btnHost_Click(object sender, RoutedEventArgs e)          
        {
            if (validate())
            {

                HostingUnit hostingUnit = new HostingUnit(Mode.Add);
                hostingUnit.ShowDialog();
            }
        }

        private bool validate() {

            long.TryParse(txtId.Text, out long id);

            if (id == 0)
            {
                MessageBox.Show(".יש להזין מספר זהות");

                return false;

            }

            var hosts = BL_Singletone.Instance.GetHostingUnitsByOwnerId(id);
            var guestRequests = BL_Singletone.Instance.GetGuestRequestsById(id);

            if (hosts.Count > 0 || guestRequests.Count > 0)
            {
                string type = (hosts.Count > 0) ? "מארח" : "אורח";
                MessageBox.Show($".{type} :) אנחנו כבר מכירים ");

                Close();

                return false;
            }

            return true;

        }
    }
}
