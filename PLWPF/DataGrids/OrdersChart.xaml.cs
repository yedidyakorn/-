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
            var tmp = new PlotModel();
            tmp.Title = "Orders status chart";

            var ps = new PieSeries();

            BL_Singletone.Instance.GetOrderList().GroupBy(o => o.Status).Select(o => new { status = o.Key, count = o.Count() }).ToList().ForEach(o =>
            {
                ps.Slices.Add(new PieSlice(o.status.ToString(), o.count) { IsExploded = true });
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
            InitializeComponent();
        }
    }
}
