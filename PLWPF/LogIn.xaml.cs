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

            try {
              //  BL_Singletone.Instance.LoadHostingUnitsDairy();
            }
            catch { }

            SetGreeting();
        }


        private void SetGreeting()
        {
            string smily = " (: ";
            string partOfDay = "";
            TimeSpan morning = new TimeSpan(5,0,0);
            TimeSpan noon = new TimeSpan(12, 0, 0);
            TimeSpan evening = new TimeSpan(17, 0, 0);
            TimeSpan night = new TimeSpan(21, 0, 0);

            var currentTime = DateTime.Now.TimeOfDay;

            if (currentTime < morning || currentTime >= night)
            {

                partOfDay = "לילה טוב";

            }
            else if (currentTime >= morning && currentTime < noon)
            {
                partOfDay = "בוקר טוב";
            }
            else if (currentTime >= noon && currentTime < evening)
            {
                partOfDay = "צהריים טובים";
            }
            else if (currentTime >= evening)
            {
                partOfDay = "ערב טוב";
            }
            lblTime.Content = smily + partOfDay;

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
                Close();

            }
            else if(hosts.Count > 0)
            {

                PersonalArea personalArea = new PersonalArea(hosts.First());
                personalArea.ShowDialog();
                Close();

            }
            else if (guestRequests.Count > 0)
            {

                GuestRequestMng guestRequestMng = new GuestRequestMng(guestRequests.First());
                guestRequestMng.ShowDialog();
                Close();

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
