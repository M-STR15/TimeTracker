using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Media;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.Windows.Reports.Services;

namespace TimeTracker.Windows.Reports
{
	/// <summary>
	/// Interaction logic for ActivitiesOverDaysWindow.xaml
	/// </summary>
	[ObservableObject]
	public partial class ActivitiesOverDaysWindow : Window
	{
		[ObservableProperty]
		public Func<double, string> _formatter;

		[ObservableProperty]
		private string[] _labels;

		[ObservableProperty]
		private SeriesCollection _seriesCollection;

		public ActivitiesOverDaysWindow()
		{
			InitializeComponent();
			var reportParametersService = new ReportParameterService();
			cmbMonth.ItemsSource = reportParametersService.Monts;
			cmbMonth.SelectedIndex = 0;

			createChartData();

			DataContext = this;
		}


		private void createChartData()
		{
			var getProvider = new ReportRepository();

			var selectItemCmb = cmbMonth.SelectedItem as string;

			var dateFrom = Convert.ToDateTime("1." + selectItemCmb);
			var dateTo = dateFrom.AddMonths(1);
			var list = getProvider.GetActivityOverDays(dateFrom, dateTo);

			_seriesCollection = new SeriesCollection
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

			_labels = list.Select(x => x.Date.ToString("dd.MM") + " [" + x.WeekDay + "]").ToArray();
			_formatter = value => value.ToString();
		}
		private void onCmbMonth_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			createChartData();
		}
	}
}