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
        public GuestRequestMng()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            GuestRequestWindow guestRequest = new GuestRequestWindow(Mode.Add );
            guestRequest.ShowDialog();

            //if (guestRequest.DialogResult == true)
            //{
            //    instance.AddHost(wnd.Host);
            //}
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            GuestRequestWindow guestRequest = new GuestRequestWindow(Mode.Update);
            guestRequest.ShowDialog();

            //if (guestRequest.DialogResult == true)
            //{
            //    instance.AddHost(wnd.Host);
            //}
        }

     
    }
}
