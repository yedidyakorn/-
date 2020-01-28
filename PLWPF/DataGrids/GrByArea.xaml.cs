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

namespace PLWPF.DataGrids
{
    /// <summary>
    /// Interaction logic for GrByArea.xaml
    /// </summary>
    public partial class GrByArea : Window
    {
        List<GuestRequest> guestRequests = new List<GuestRequest>();

        public GrByArea()
        {

            guestRequests = BL_Singletone.Instance.GetGuestRequestsList();

            InitializeComponent();

            ListCollectionView col = new ListCollectionView(guestRequests);
            col.GroupDescriptions.Add(new PropertyGroupDescription("Area"));

            GRdataGrid.ItemsSource = col;

            this.DataContext = guestRequests;

            InitializeComponent();

        }

    }
}
