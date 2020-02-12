using BE;
using BL;
using PLWPF.GuestRequestWindows;
using PLWPF.HostingUnitWindows;
using PLWPF.SiteManager;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            long.TryParse(IdBox.Text, out long id);

            if (id == 0) {

                MessageBox.Show("Enter a valid Id number.");
                return;

            }

            var hosts = BL_Singletone.Instance.GetHostingUnitsByOwnerId(id);
            var guestRequests = BL_Singletone.Instance.GetGuestRequestsById(id);

            if (id == 301637922) {
                Menager menager = new Menager();
                menager.ShowDialog();
            }
            else if(hosts.Count > 0)
            {
                PersonalArea personalArea = new PersonalArea(hosts.First());
                personalArea.ShowDialog();
            }
            else if (guestRequests.Count > 0)
            {
                GuestRequestMng guestRequestMng = new GuestRequestMng(guestRequests.First());
                guestRequestMng.ShowDialog();
            }
            else
            {
                MessageBox.Show(".מצטערים, נראה שלא נפגשנו בעבר. תוכל להרשם בלינק מטה");
            }
            
           
        }

        private void lblSign_MouseLeftButtonUp(object sender, RoutedEventArgs e) {
            SignIn signIn = new SignIn();
            signIn.ShowDialog();
        }
    }
}
