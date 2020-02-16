using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    /// Interaction logic for HostingUnit.xaml
    /// </summary>
    public partial class HostingUnit : Window
    {

        BE.HostingUnit hostingUnit = new BE.HostingUnit();

        public List<ValidationError> _errors = new List<ValidationError>();

        private List<BankBranch> bankBranches = null;

        List<int> branchesNumbersForBank = new List<int>();

        public HostingUnit(Mode mode)
        {
            SetBankDetails();

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

            for (var i = 0; i < bnNameBox.Items.Count; i++)
            {
                if (bnNameBox.Items[i].ToString() == hostingUnit.Owner.BankBranchDetails.BankName)
                {
                    bnNameBox.SelectedIndex = i;
                }
            }

            for (var i = 0; i < braNumBox.Items.Count; i++)
            {
                if (braNumBox.Items[i].ToString() == hostingUnit.Owner.BankBranchDetails.BankNumber.ToString())
                {
                    braNumBox.SelectedIndex = i;
                }
            }

            updateBtn.Visibility = Visibility.Visible;
            findGRBtn.Visibility = Visibility.Visible;
            deleteBtn.Visibility = Visibility.Visible;
        }

        public void SetTakenDatesInCalender()
        {
            DateTime curDate = DateTime.Now.Date;

            for (var i = 0; i < 12; i++)
            {
                for (var j = 0; j < 31; j++)
                {
                    if (hostingUnit.Diary[i, j])
                    {
                        try
                        {
                            int calcYear = i > curDate.Month ? curDate.Year + 1 : curDate.Year;
                            clndr.BlackoutDates.Add(new CalendarDateRange(new DateTime(calcYear, i + 1, j + 1)));
                        }catch(Exception ex)
                        {

                        }
                    }
                }
            }
        }

        public void GetValueFromEnums() {

            hostingUnit.Area = (VecationAreas)areaBox.SelectedItem;

        }

        public void SetEnumsValues()
        {
            areaBox.ItemsSource = Enum.GetValues(typeof(VecationAreas));
            areaBox.SelectedIndex = 0;
     
            bnNameBox.ItemsSource = bankBranches.Select(b => b.BankName.Trim()).Distinct().ToList();
            bnNameBox.SelectedIndex = 0;

            braNumBox.ItemsSource = branchesNumbersForBank;
            braNumBox.SelectedIndex = 0;

        }

        private void SetBankDetails()
        {
            int counter = 0;

            while (bankBranches == null)
            {

                try
                {
                    bankBranches = BL_Singletone.Instance.GetBankBranchesList();

                }
                catch (LogicException ex)
                {
                    if (counter == 10)
                    {
                        MessageBox.Show(ex.Message);

               
                        Close();
                    }
                    Thread.Sleep(2 * 1000);
                }

                counter++;
            }

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

        private void bnNameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<BankBranch> bankBranch = bankBranches.Where(b => b.BankName == bnNameBox.SelectedValue.ToString()).ToList();

            branchesNumbersForBank = bankBranch.Select(b => b.BranchNumber).ToList();
            braNumBox.ItemsSource = branchesNumbersForBank;
            braNumBox.SelectedItem = braNumBox.Items[0];
            braNumBox.SelectedValue = branchesNumbersForBank.First();
            braNumBox.Text = branchesNumbersForBank.First().ToString();
           
        }    

        private void braNumBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (braNumBox.SelectedItem != null)
            {
                BankBranch bankBranch = bankBranches.Where(b => b.BankName.Trim() == bnNameBox.SelectedValue.ToString().Trim() && b.BranchNumber.ToString() == braNumBox.SelectedItem.ToString()).FirstOrDefault();

                hostingUnit.Owner.BankBranchDetails.BankName = bankBranch.BankName;
                hostingUnit.Owner.BankBranchDetails.BankNumber = bankBranch.BankNumber;
                hostingUnit.Owner.BankBranchDetails.BranchNumber = bankBranch.BranchNumber;
                hostingUnit.Owner.BankBranchDetails.BranchCity = bankBranch.BranchCity;
                hostingUnit.Owner.BankBranchDetails.BranchAddress = bankBranch.BranchAddress;
            }

        }

        #endregion
    }
}
