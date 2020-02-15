using BE;
using BL;
using OxyPlot;
using OxyPlot.Series;
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
    /// Interaction logic for OrdersChart.xaml
    /// </summary>
    public partial class OrdersChart : Window
    {
        public PlotModel PieModel { get; set; }

        public OrdersChart()
        {
            try
            {
                var tmp = new PlotModel();
                tmp.Title = "Orders status chart";

                var ps = new PieSeries();

                BL_Singletone.Instance.GetOrdersGroupByStatus().ToList().ForEach(o => {

                    System.Type type = o.GetType();
                    string status = ((OrderStatuses)type.GetProperty("status").GetValue(o, null)).ToString();
                    int count = (int)type.GetProperty("count").GetValue(o, null);
                
                    ps.Slices.Add(new PieSlice(status, count) { IsExploded = true });

                });

                ps.InnerDiameter = 0;
                ps.ExplodedDistance = 0;
                ps.Stroke = OxyColors.Black;
                ps.StrokeThickness = 1.0;
                ps.AngleSpan = 360;
                ps.StartAngle = 0;
                tmp.Series.Add(ps);

                PieModel = tmp;
                DataContext = this;
            }
            catch (LogicException ex){
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {   
                MessageBox.Show("general error");
            }

            InitializeComponent();
        }
    }
}
