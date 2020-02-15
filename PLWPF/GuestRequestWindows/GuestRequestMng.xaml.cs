using BE;
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
using Util;

namespace PLWPF.GuestRequestWindows
{
    /// <summary>
    /// Interaction logic for GuestRequestMng.xaml
    /// </summary>
    public partial class GuestRequestMng : Window
    {
        GuestRequest guestRequest = new GuestRequest();

        public GuestRequestMng(GuestRequest GuestRequest)
        {
            guestRequest = GuestRequest;
            this.DataContext = guestRequest;
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            guestRequest = BL_Singletone.Instance.GetGuestRequestsById(guestRequest.Id).First();

            var newGuestRequest = new GuestRequest()
            {
                Id = guestRequest.Id,
                PrivateName = guestRequest.PrivateName,
                FamilyName = guestRequest.FamilyName,
                MailAddress = guestRequest.MailAddress
            };

            GuestRequestWindow guestRequestWin = new GuestRequestWindow(Mode.Add, newGuestRequest);

            guestRequestWin.ShowDialog();

          
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var grList = BL_Singletone.Instance.GetGuestRequestsById(guestRequest.Id);

            if (grList.Count() == 0)
            {
                MessageBox.Show("No Items Found", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            GuestRequestGrid guestRequestGrid = new GuestRequestGrid(grList);
            guestRequestGrid.ShowDialog();

                if (guestRequestGrid.DialogResult == true)
                {
                    var guestRequestToUpdate = guestRequestGrid.selectedGR;
                    GuestRequestWindow guestRequestWindow = new GuestRequestWindow(Mode.Update, guestRequestToUpdate);
                    guestRequestWindow.ShowDialog();
                }
            }
            catch (LogicException ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("general error");
            }

            // GuestRequestWindow guestRequest = new GuestRequestWindow(Mode.Update);
            // guestRequest.ShowDialog();


        }

     
    }
}
