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

namespace PLWPF.Orders
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        BE.Order order = new BE.Order();

        public List<ValidationError> _errors = new List<ValidationError>();

        public Order(Mode mode)
        {
            InitializeComponent();

            SetDataInEnums();

            this.DataContext = order;
        }

        public Order(Mode mode, BE.Order foundOrder) : this(mode)
        {
            order = foundOrder;

            if (mode == Mode.Update)
                Update();        

            this.DataContext = order;
        }

        private void Update()
        {
            updateBtn.Visibility = Visibility.Visible;

            for (var i = 0; i < stsBox.Items.Count; i++)
            {
                if ((OrderStatuses)stsBox.Items[i] == order.Status)
                {
                    stsBox.SelectedIndex = i;
                }
            }
        }

        private void SetDataInEnums()
        {

            stsBox.ItemsSource = Enum.GetValues(typeof(OrderStatuses));
            stsBox.SelectedIndex = 0;

        }

        public void GetDataFromEnums()
        {
            order.Status = (OrderStatuses)stsBox.SelectedItem;
        }

        #region events

        private void Validation_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors.Add(e.Error);
            else
                _errors.Remove(e.Error);

            //addBtn.IsEnabled =
           updateBtn.IsEnabled = !_errors.Any();

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            GetDataFromEnums();

            try
            {
                DialogResult = BL_Singletone.Instance.UpdateOrder(order.OrderKey, order.Status);
            }
            catch (LogicException ex) {
                MessageBox.Show(ex.Message);
            }

            Close();
        }

        #endregion
    }
}
