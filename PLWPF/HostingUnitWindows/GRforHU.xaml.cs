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

namespace PLWPF.HostingUnitWindows
{
    /// <summary>
    /// Interaction logic for GRforHU.xaml
    /// </summary>
    public partial class GRforHU : Window
    {
        List<GuestRequest> guestRequests = new List<GuestRequest>();

        long HostUnitKey;

        public GuestRequest selectedGR;

        public GRforHU(List<GuestRequest> gRequests, long hostUnitKey)
        {
            HostUnitKey = hostUnitKey;

            guestRequests = gRequests;

            InitializeComponent();

            GrGrid.ItemsSource = guestRequests;

            //  this.DataContext = guestRequests;


        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            GuestRequest guestRequest = ((FrameworkElement)sender).DataContext as GuestRequest;

            Order newOrder = new Order
            {
                GuestRequestKey = guestRequest.GuestRequestKey,
                HostingUnitKey = HostUnitKey,
                Status = OrderStatuses.MailSent,
                OrderDate = DateTime.Now

            };

            bool result = false;

            try
            {
                 result = BL_Singletone.Instance.AddOrder(newOrder);
            }
            catch (LogicException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (result)
                MessageBox.Show("mail has been sent to potential guest");
            else
             MessageBox.Show("error while adding an order");

        }
    }
}
