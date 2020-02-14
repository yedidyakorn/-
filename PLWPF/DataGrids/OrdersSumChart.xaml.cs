using BE;
using BL;
using OxyPlot;
using OxyPlot.Axes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF.DataGrids
{
    /// <summary>
    /// Interaction logic for OrdersSumChart.xaml
    /// </summary>
    public partial class OrdersSumChart : Window
    {
        public PlotModel modelTest { get; set; }


        public OrdersSumChart()
        {
            try
            {

                var points = BL_Singletone.Instance.GetOrdersGroupByCreateDate().Select(o => new { date = o.Key, count = o.Count() }).OrderBy(o => o.date).ToList();


                var model = new PlotModel { Title = "גרף הזמנות לפי תאריך" };
                var minValue = DateTimeAxis.ToDouble(points.First().date.AddMonths(-1));
                var maxValue = DateTimeAxis.ToDouble(points.Last().date.AddMonths(1));

                model.Axes.Add(new DateTimeAxis
                {
                    MinorIntervalType = DateTimeIntervalType.Months,
                    IntervalType = DateTimeIntervalType.Months,
                    Position = AxisPosition.Bottom,
                    Minimum = minValue,
                    Maximum = maxValue,
                    StringFormat = "y/M"
                });

                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Minimum = 0,
                    Maximum = points.Max(o => o.count) + 1
                });

                FunctionSeries fs = new FunctionSeries();

                points.ForEach(o =>
                {
                    fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(o.date), o.count));
                });

                model.Series.Add(fs);
                modelTest = model;
                DataContext = this;
            }
            catch (LogicException ex)
            {
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
