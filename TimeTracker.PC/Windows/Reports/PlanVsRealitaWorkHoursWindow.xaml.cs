using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Media;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.PC.Services;
using TimeTracker.PC.Windows.Reports.Services;

namespace TimeTracker.PC.Windows.Reports
{
	/// <summary>
	/// Interaction logic for PlanVsRealitaWorkHoursWindow.xaml
	/// </summary>
	[ObservableObject]
	public partial class PlanVsRealitaWorkHoursWindow : Window
	{
		[ObservableProperty]
		private AxesCollection _axisYCollection;

		[ObservableProperty]
		private string[] _labels;

		private ReportRepository<SqliteDbContext> _reportRepository;

		[ObservableProperty]
		private SeriesCollection _seriesCollection;

		[ObservableProperty]
		private Func<double, string> _yFormatter;

		private readonly Func<SqliteDbContext> _context;
		private readonly EventLogService _eventLogService;
		public PlanVsRealitaWorkHoursWindow(Func<SqliteDbContext> context)
		{
			InitializeComponent();
			_context = context;
			_reportRepository = new ReportRepository<SqliteDbContext>(_context);
			var reportParametersService = new ReportParameterService();
			cmbMonth.ItemsSource = reportParametersService.Monts;
			_eventLogService = new EventLogService();
			if (reportParametersService.Monts.Count > 0)
				cmbMonth.SelectedItem = reportParametersService.Monts.First();

			createChart();

			DataContext = this;
		}

		private void createChart()
		{
			try
			{
				var selectMonth = cmbMonth.SelectedItem as string;
				var selectDate = Convert.ToDateTime("1." + selectMonth);

				var month = selectDate.Month;
				var year = selectDate.Year;
				var workplaceHours = _reportRepository.GetWorkplaceHours(year, month);

				SeriesCollection = new SeriesCollection
				{
					new StackedColumnSeries
					{
						Title = "Work hours-Real",
						Values = new ChartValues<double>(workplaceHours.OfficeWorkHourslist.Select(x => x.DateHours).ToArray()),
						PointGeometry = DefaultGeometries.Circle,
						//Fill= new SolidColorBrush(Colors.Pink),
						 ScalesYAt = 1
					},
					new StackedColumnSeries
					{
						Title = "Home office-Real",
						Values = new ChartValues<double>(workplaceHours.HomeOfficeWorkHourslist.Select(x => x.DateHours).ToArray()),
						PointGeometry = DefaultGeometries.Circle,
						Stroke = Brushes.Black,
						Fill=null,
						ScalesYAt = 1
						//Fill= new SolidColorBrush(Colors.Blue),
					},
					new LineSeries
					{
						Title = "Cum.-Work hours-Real",
						Values = new ChartValues<double>(workplaceHours.OfficeWorkHourslist.Select(x => x.CumHours).ToArray()),
						PointGeometry = DefaultGeometries.Circle,
						LineSmoothness = 0,
						ScalesYAt = 0
						//Fill= new SolidColorBrush(Colors.Pink),
					},
					new LineSeries
					{
						Title = "Cum.-Home office-Real",
						Values = new ChartValues<double>(workplaceHours.HomeOfficeWorkHourslist.Select(x => x.CumHours).ToArray()),
						PointGeometry = DefaultGeometries.Circle,
						LineSmoothness = 0,
						ScalesYAt = 0
						//Fill= new SolidColorBrush(Colors.Blue),
					},
					new LineSeries
					{
						Title = "Cum.-Work hours-Plan",
						Values = new ChartValues<double>(workplaceHours.PlanWorkHoursList.Select(x => x.CumHours).ToArray()),
						PointGeometry = DefaultGeometries.Diamond,
						LineSmoothness = 0,
						ScalesYAt = 0
						//Fill= new SolidColorBrush(Colors.Pink),
					},
					new LineSeries
					{
						Title = "Cum.-Home office-Plan",
						Values = new ChartValues<double>(workplaceHours.PlanHomeOfficeWorkHoursList.Select(x => x.CumHours).ToArray()),
						PointGeometry = DefaultGeometries.Diamond,
						LineSmoothness = 0,
						ScalesYAt = 0
						//Fill= new SolidColorBrush(Colors.Blue),
					}
				};

				Labels = workplaceHours.OfficeWorkHourslist.Select(x => x.Date.ToString("dd.MM [ddd}")).ToArray();
				YFormatter = value => value.ToString();

				AxisYCollection = new AxesCollection
				{
					new Axis { Title = "Cum hours", Foreground = Brushes.Gray, Position= AxisPosition.RightTop , MinValue=0 },
					new Axis { Title = "Hours", Foreground = Brushes.Red , Position= AxisPosition.LeftBottom , MinValue=0 },
				};
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("cbc06536-b0f0-45bf-8a8e-eb986f045d02"), ex, "Problém při vytváření reportu.");
			}
		}

		private void onCmbMonth_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			createChart();
		}
	}
}