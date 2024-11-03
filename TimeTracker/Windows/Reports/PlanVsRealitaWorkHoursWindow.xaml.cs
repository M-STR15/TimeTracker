using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Media;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;

namespace TimeTracker.Windows.Reports
{
    /// <summary>
    /// Interaction logic for PlanVsRealitaWorkHoursWindow.xaml
    /// </summary>
    public partial class PlanVsRealitaWorkHoursWindow : Window
    {
        private ReportProvider _reportProvider = new ReportProvider();
        public PlanVsRealitaWorkHoursWindow()
        {
            InitializeComponent();

            createChart();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void createChart()
        {
            var start = new DateTime(2024, 10, 1);
            var end = new DateTime(2024, 11, 1);
            var typeShifts = new eTypeShift[] { eTypeShift.Office };
            var officeWorkHourslist = _reportProvider.GetPlanVsRealitaWorkHours(start, end, typeShifts);

            var typeShifts2 = new eTypeShift[] { eTypeShift.HomeOffice , eTypeShift.Others };
            var homeOfficeWorkHourslist = _reportProvider.GetPlanVsRealitaWorkHours(start, end, typeShifts2);

            SeriesCollection = new SeriesCollection
            {

                new StackedColumnSeries
                {
                    Title = "Work hours-Real",
                    Values = new ChartValues<double>(officeWorkHourslist.Select(x => x.DateHours).ToArray()),
                    PointGeometry = DefaultGeometries.Circle,
                    //Fill= new SolidColorBrush(Colors.Pink),
                     ScalesYAt = 1
                },
                new StackedColumnSeries
                {
                    Title = "Home office-Real",
                    Values = new ChartValues<double>(homeOfficeWorkHourslist.Select(x => x.DateHours).ToArray()),
                    PointGeometry = DefaultGeometries.Circle,
                    Stroke = Brushes.Black,
                    Fill=null,
                    ScalesYAt = 1
                    //Fill= new SolidColorBrush(Colors.Blue),
                },
                new LineSeries
                {
                    Title = "Cum.-Work hours-Real",
                    Values = new ChartValues<double>(officeWorkHourslist.Select(x => x.CumHours).ToArray()),
                    PointGeometry = DefaultGeometries.Circle,
                    LineSmoothness = 0,
                    ScalesYAt = 0
                    //Fill= new SolidColorBrush(Colors.Pink),
                },
                new LineSeries
                {
                    Title = "Cum.-Home office-Real",
                    Values = new ChartValues<double>(homeOfficeWorkHourslist.Select(x => x.CumHours).ToArray()),
                    PointGeometry = DefaultGeometries.Circle,
                    LineSmoothness = 0,
                    ScalesYAt = 0
                    //Fill= new SolidColorBrush(Colors.Blue),
                },
                new LineSeries
                {
                    Title = "Cum.-Work hours-Plan",
                    Values = new ChartValues<double>(officeWorkHourslist.Select(x => x.CumHours).ToArray()),
                    PointGeometry = DefaultGeometries.Diamond,
                    LineSmoothness = 0,
                    ScalesYAt = 0
                    //Fill= new SolidColorBrush(Colors.Pink),
                },
                new LineSeries
                {
                    Title = "Cum.-Home office-Plan",
                    Values = new ChartValues<double>(homeOfficeWorkHourslist.Select(x => x.CumHours).ToArray()),
                    PointGeometry = DefaultGeometries.Diamond,
                    LineSmoothness = 0,
                    ScalesYAt = 0
                    //Fill= new SolidColorBrush(Colors.Blue),
                }
            };

            Labels = officeWorkHourslist.Select(x => x.Date.ToString("dd.MM [ddd}")).ToArray();
            YFormatter = value => value.ToString();

            AxisYCollection = new AxesCollection
        {
            new Axis { Title = "Cum hours", Foreground = Brushes.Gray, Position= AxisPosition.RightTop , MinValue=0 },
            new Axis { Title = "Hours", Foreground = Brushes.Red , Position= AxisPosition.LeftBottom , MinValue=0 },
        };
        }

        public AxesCollection AxisYCollection { get; set; }
    }
}
