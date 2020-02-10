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

namespace PLWPF.HostingUnitWindows
{
    /// <summary>
    /// Interaction logic for PersonalArea.xaml
    /// </summary>
    public partial class PersonalArea : Window
    {
        //List<BE.HostingUnit> hostingUnits;

        long Id;

        public PersonalArea(long id)
        {
            Id = id;

            InitializeComponent();
        }

        private void addHobutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.HostingUnit host = new BE.HostingUnit
                {
                    Owner = BL_Singletone.Instance.GetHostingUnitsByOwnerId(Id).First().Owner
                };

                HostingUnit hostingUnit = new HostingUnit(Mode.Add, host);
                hostingUnit.ShowDialog();
            }
            catch (LogicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("general error");
            }
        }

        private void updatHoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BE.HostingUnit> hostingUnits = BL_Singletone.Instance.GetHostingUnitsByOwnerId(Id);

                HostUnitGrid hostUnitGrid = new HostUnitGrid(hostingUnits);
                hostUnitGrid.ShowDialog();

                if (hostUnitGrid.DialogResult == true)
                {
                    HostingUnit hostingUnit = new HostingUnit(Mode.Update, hostUnitGrid.selectedHU);
                    hostingUnit.ShowDialog();
                }
            }
            catch (LogicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("general error");
            }
        }
    }
}
