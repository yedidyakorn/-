using BE;
using BL;
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
    /// Interaction logic for HostingUnit.xaml
    /// </summary>
    public partial class HostingUnit : Window
    {
        BE.HostingUnit hostingUnit = new BE.HostingUnit();

        public List<ValidationError> _errors = new List<ValidationError>();

        public HostingUnit(Mode mode)
        {
            InitializeComponent();

            if (mode == Mode.Add)
                Add();

            SetEnumsValues(); 

            this.DataContext = hostingUnit;

        }
        
        public HostingUnit(Mode mode, BE.HostingUnit host): this(mode)
        {
            hostingUnit = host;

            if (mode == Mode.Update)
                Update();
          
            SetTakenDatesInCalender();

            this.DataContext = hostingUnit;

        }

        public void Add()
        {
            addBtn.Visibility = Visibility.Visible;
        }

        public void Update()
        {
            for (var i = 0; i < areaBox.Items.Count; i++)
            {
                if ((VecationAreas)areaBox.Items[i] == hostingUnit.Area)
                {
                    areaBox.SelectedIndex = i;
                }
            }

            updateBtn.Visibility = Visibility.Visible;

            findGRBtn.Visibility = Visibility.Visible;

            deleteBtn.Visibility = Visibility.Visible;
        }

        public void SetTakenDatesInCalender()
        {
            DateTime curDate = DateTime.Now;

            for (var i = 0; i < 12; i++)
            {
                for (var j = 0; j < 31; j++)
                {
                    if (hostingUnit.Diary[i, j])
                    {
                        int calcYear = i > curDate.Month ? curDate.Year + 1 : curDate.Year;
                        clndr.BlackoutDates.Add(new CalendarDateRange(new DateTime(calcYear, i + 1, j + 1)));
                    }
                }
            }
        }

        public void GetValueFromEnums() {

            hostingUnit.Area = (VecationAreas)areaBox.SelectedItem;

            //int.TryParse(banNumBox.SelectedItem.ToString(), out int bankNum);
            //hostingUnit.Owner.BankBranchDetails.BankNumber = bankNum;

            //int.TryParse(braNumBox.SelectedItem.ToString(), out int branchNum);
            //hostingUnit.Owner.BankBranchDetails.BranchNumber = branchNum;

            //hostingUnit.Owner.BankBranchDetails.BankName = bnNameBox.SelectedItem.ToString();
            //hostingUnit.Owner.BankBranchDetails.BranchAddress = braAddBox.SelectedItem.ToString();
            //hostingUnit.Owner.BankBranchDetails.BranchCity = braCitiBox.SelectedItem.ToString();

        }

        public void SetEnumsValues()
        {
            areaBox.ItemsSource = Enum.GetValues(typeof(VecationAreas));
            areaBox.SelectedIndex = 0;
        }

        #region events

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetValueFromEnums();

                DialogResult = BL_Singletone.Instance.AddHostingUnit(hostingUnit);

                Close();
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

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetValueFromEnums();

                DialogResult = BL_Singletone.Instance.UpdateHostingUnit(hostingUnit);

                Close();
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

        private void Validation_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors.Add(e.Error);
            else
                _errors.Remove(e.Error);

            addBtn.IsEnabled = updateBtn.IsEnabled = !_errors.Any();
        }     

        private void findGRBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<GuestRequest> guestRequests = BL_Singletone.Instance.GetAllGuestRequestsForHostUnit(hostingUnit);

                GRforHU gRforHU = new GRforHU(guestRequests, hostingUnit.HostingUnitKey);

                gRforHU.ShowDialog();
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

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL_Singletone.Instance.DeleteHostingUnit(hostingUnit.HostingUnitKey);

                MessageBox.Show("Host Unit successfully deleted.");
            }
            catch (LogicException ex){
                MessageBox.Show(ex.Message);
            }

            Close();

        }
        
        #endregion
    }
}
