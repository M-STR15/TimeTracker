using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Media;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;

namespace TimeTracker.Windows.Reports
{
    /// <summary>
    /// Interaction logic for ActivitiesOverDaysWindow.xaml
    /// </summary>
    [ObservableObject]
    public partial class ActivitiesOverDaysWindow : Window
    {
        public List<string> Monts { get; set; }

        public ActivitiesOverDaysWindow()
        {
            InitializeComponent();
            Monts = getLastSixMonth();

            cmbMonth.ItemsSource = Monts;
            cmbMonth.SelectedIndex = 0;

            createChartData();

            DataContext = this;
        }

        private List<string> getLastSixMonth()
        {
            List<string> lastSixMonths = new List<string>();
            DateTime currentDate = DateTime.Now;

            for (int i = 0; i < 6; i++)
            {
                DateTime month = currentDate.AddMonths(-i);
                string monthYear = month.ToString("MM.yyyy");
                lastSixMonths.Add(monthYear);
            }

            return lastSixMonths;
        }

        private void createChartData()
        {
            var getProvider = new ReportProvider();

            var selectItemCmb = cmbMonth.SelectedItem as string;

            var dateFrom = Convert.ToDateTime("1." + selectItemCmb);
            var dateTo = dateFrom.AddMonths(1);
            var list = getProvider.GetActivityOverDays(dateFrom, dateTo);

            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title ="Work hours Office",
                    Values = new ChartValues<double> (list.Where(x=>x.TypeShift==eTypeShift.Office).Select(x=>x.WorkHours)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill= Brushes.CornflowerBlue
                },
                new StackedColumnSeries
                {
                    Title ="Pauses-Office",
                    Values = new ChartValues<double> (list.Where(x=>x.TypeShift==eTypeShift.Office).Select(x=>x.Pause)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill= Brushes.DarkSeaGreen
                },
                new StackedColumnSeries
                {
                    Title ="Work hours-HomeOffice",
                    Values = new ChartValues<double> (list.Where(x=>x.TypeShift==eTypeShift.HomeOffice).Select(x=>x.WorkHours)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill=Brushes.MediumOrchid
                },
                new StackedColumnSeries
                {
                    Title ="Pauses-HomeOffice",
                    Values = new ChartValues<double> (list.Where(x=>x.TypeShift==eTypeShift.HomeOffice).Select(x=>x.Pause)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill= Brushes.MediumSpringGreen
                },
                new StackedColumnSeries
                {
                    Title ="Work hours-Others",
                    Values = new ChartValues<double> (list.Where(x => x.TypeShift == eTypeShift.Others).Select(x=>x.WorkHours)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill= Brushes.SlateBlue
                },
                new StackedColumnSeries
                {
                    Title ="Pauses-Others",
                    Values = new ChartValues<double> (list.Where(x=>x.TypeShift==eTypeShift.Others).Select(x=>x.Pause)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill= Brushes.YellowGreen
                }
            };

            Labels = list.Select(x => x.Date.ToString("dd.MM") + " [" + x.WeekDay + "]").ToArray();
            Formatter = value => value + " H";
        }

        [ObservableProperty]
        private SeriesCollection _seriesCollection;

        [ObservableProperty]
        private string[] _labels;

        [ObservableProperty]
        public Func<double, string> _formatter;

        private void cmbMonth_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            createChartData();
        }
    }
}