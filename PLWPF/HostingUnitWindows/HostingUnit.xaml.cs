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
    /// Interaction logic for HostingUnit.xaml
    /// </summary>
    public partial class HostingUnit : Window
    {
        BE.HostingUnit hostingUnit = new BE.HostingUnit();

        public HostingUnit()
        {
            InitializeComponent();

            this.DataContext = hostingUnit;
        }

        public HostingUnit(BE.HostingUnit hostingWithOwner)
        {
            InitializeComponent();
 
            for(var i = 0; i< 12; i++)
            {
                for(var j =0; j< 31; i++)
                {
                    if (hostingUnit.Diary[i, j])
                    {
                      // clndr.BlackoutDates.Add(new)
                    }
                }
            }


                //foreach (DateTime date in CurrentHostingUnit._allOrders)
                //    MyCalendar.BlackoutDates.Add(new CalendarDateRange(date));

                this.DataContext = hostingWithOwner;
        }
    }
}
