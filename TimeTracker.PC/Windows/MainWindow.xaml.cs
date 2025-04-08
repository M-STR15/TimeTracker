using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.PC.Models;
using TimeTracker.PC.Services;
using TimeTracker.PC.Stories;
using TimeTracker.PC.ViewModels;
using TimeTracker.PC.Windows.Reports;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.PC.Windows
{
	public partial class MainWindow : Window, IDisposable
	{
		private const string _formTime = @"hh\:mm\:ss";
		private readonly Func<SqliteDbContext> _context;
		private readonly MainStory _mainStory;
		private DispatcherTimer _dispatcherTimer;
		private EventLogService _eventLogService;
		private EventHandler _lastRecordActivityHangler;
		private RecordActivity? _lra;
		private ProjectRepository<SqliteDbContext> _projectProvider;

		private RecordRepository<SqliteDbContext> _recordProvider;

		private ReportRepository<SqliteDbContext> _reportProvider;

		private List<ShiftCmb> _shiftCmbs = new();

		private ShiftRepository<SqliteDbContext> _shiftProvider;

		//private int _totalActivityTimeBeforeInSecond;

		private List<TypeShift> _typeShifts = new();
		private RecordActivity? _lastRecordActivity
		{
			get => _lra;
			set
			{
				_lra = value;
				onLasRecordActivityChange();
			}
		}

		public MainWindow(MainStory mainStory, Func<SqliteDbContext> context)
		{
			_context = context;
			_eventLogService = new EventLogService();
			_mainStory = mainStory;
			inicialization();
		}

		public void Dispose()
		{
			_lastRecordActivityHangler -= onSetLabelsHandler;
		}

		private void _dispatcherTimer_Tick(object? sender, EventArgs e)
		{
			setTotalTimesLabels();
		}

		private async Task<bool> addActiviteAsync(RecordActivity recordActivity)
		{
			return await addActiviteAsync(recordActivity.Activity, recordActivity.TypeShift, recordActivity.Project, recordActivity.SubModule, recordActivity.Shift, recordActivity?.Description ?? "");
		}

		private async Task<bool> addActiviteAsync(Activity activity, TypeShift typeShift, Project? project = null, SubModule? subModule = null, Shift? shift = null, string description = "")
		{
			try
			{
				var startTimeActivity = DateTime.Now;

				var record = new RecordActivity();
				if (shift != null && shift.GuidId != Guid.Empty)
					record = new RecordActivity(startTimeActivity, activity.Id, shift.GuidId, typeShift.Id, project?.Id ?? null, subModule?.Id ?? null, description);
				else
					record = new RecordActivity(startTimeActivity, activity.Id, typeShift?.Id ?? null, project?.Id ?? null, subModule?.Id ?? null, description);

				var result = await _recordProvider.SaveAsync(record);
				if (result != null)
				{
					_lastRecordActivity = new RecordActivity(result.GuidId, startTimeActivity, null, activity, typeShift, shift, project, subModule, description);
				}

				return result != null;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private string getShiftDateText()
		{
			var shift = _lastRecordActivity?.Shift;
			var shiftDate = "";

			if (shift != null)
			{
				var cmdShift = new ShiftCmb(shift);
				shiftDate = cmdShift?.StartDateStr ?? "";
			}
			return shiftDate;
		}

		private string getTextFromRichTextBox(RichTextBox richTextBox)
		{
			var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
			return textRange.Text;
		}

		private async void inicialization()
		{
			try
			{
				this.DataContext = new BaseViewModel("Timer tracker");
				InitializeComponent();
				var containerStore = _mainStory.DIContainerStore;
				_shiftProvider = containerStore.GetShiftProvider();
				_projectProvider = containerStore.GetProjectProvider();
				_recordProvider = containerStore.GetRecordProvider();
				_reportProvider = containerStore.GetReportProvider();

				loadProjects();
				loadShifts();
				loadTypeShifts();

				_dispatcherTimer = new DispatcherTimer();
				_dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
				_dispatcherTimer.Tick += _dispatcherTimer_Tick;

				cmbProjects.DisplayMemberPath = "Name";
				cmbShift.DisplayMemberPath = "StartDateStr";
				cmbSubModule.DisplayMemberPath = "Name";
				cmbTypeShift.DisplayMemberPath = "Name";

				_lastRecordActivityHangler += onSetLabelsHandler;

				_lastRecordActivity = await _recordProvider.GetLastRecordActivityAsync();

				if (_lastRecordActivity != null && _lastRecordActivity.ActivityId != (int)eActivity.Stop)
					_dispatcherTimer.Start();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("def4de22-6fc4-4ab4-9f7e-0aba95e5337c"), ex, "Problém při otevírání hlavního okna.");
			}
		}
		private async void loadProjects()
		{
			cmbProjects.ItemsSource = null;
			cmbProjects.ItemsSource = await _projectProvider.GetAllAsync();
			cmbProjects.SelectedIndex = 0;
		}

		private async void loadShifts()
		{
			var currentDate = DateTime.Now;
			var getList = await _shiftProvider.GetShiftsAsync(currentDate.AddDays(-7), currentDate.AddDays(3));
			_shiftCmbs = getList.Select(x => new ShiftCmb(x)).OrderByDescending(x => x.StartDate).ToList();
			_shiftCmbs.Add(new ShiftCmb());
			cmbShift.ItemsSource = _shiftCmbs.OrderByDescending(x => x.StartDate);
			cmbShift.SelectedIndex = 0;
		}

		private async void loadTypeShifts()
		{
			_typeShifts = await _shiftProvider.GetTypeShiftsForMainWindowAsync();
			cmbTypeShift.ItemsSource = _typeShifts;
			cmbTypeShift.SelectedIndex = 0;
		}

		private void mbtnActivitiesOverDays_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var window = new ActivitiesOverDaysWindow(_reportProvider);
				window.Show();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("1a0e5889-4d8f-4560-8d18-51040ff5e4a4"), ex, "Problém při otvírání reportu.");
			}
		}

		private void mbtnPlanVsRealitaWorkHours_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var window = new PlanVsRealitaWorkHoursWindow(_context);
				window.Show();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("ebb2c705-f5dd-4cc9-81b6-99caf462292a"), ex, "Problém při otvírání reportu.");
			}
		}

		private async void onActionAfterClickActivate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var startTimeActivity = DateTime.Now;
				var activity = new Activity()
				{
					Id = (int)eActivity.Start,
					Name = eActivity.Start.ToString()
				};

				var selProject = (Project)cmbProjects.SelectedItem;
				var selShift = (ShiftCmb)cmbShift.SelectedItem;
				var selSubmodule = (SubModule)cmbSubModule.SelectedItem;
				var selTypeShift = (TypeShift)cmbTypeShift.SelectedItem;

				var description = getTextFromRichTextBox(rtbDescription);
				var selRecordActivity = new RecordActivity(startTimeActivity, activity, selTypeShift, selShift, selProject, selSubmodule, description);

				var result = await addActiviteAsync(selRecordActivity);
				if (result)
				{
					rtbDescription.Document.Blocks.Clear();
					setLastSettingLabels();
					setTotalTimesLabels();
					startTimer();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("51905c9e-51c5-4fa4-bac2-fe60543bc170"), ex, "Problém při vkláání akivity.");
			}
		}

		private async void onActionAfterClickEndShift_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var startTimeActivity = DateTime.Now;
				var activity = new Activity()
				{
					Id = (int)eActivity.Stop,
					Name = eActivity.Stop.ToString()
				};

				var selRecordActivity = new RecordActivity(startTimeActivity, activity);

				var result = await addActiviteAsync(selRecordActivity);
				if (result)
				{
					setLastSettingLabels();
					setTotalTimesLabels();
					_dispatcherTimer.Stop();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("d769b7d8-adea-4011-babe-4415f3258467"), ex, "Problém při ukládání konce směny.");
			}
		}

		private async void onActionAfterClickPause_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var startTimeActivity = DateTime.Now;
				var activity = new Activity()
				{
					Id = (int)eActivity.Pause,
					Name = eActivity.Pause.ToString()
				};

				var selRecordActivity = new RecordActivity(startTimeActivity, activity);

				var result = await addActiviteAsync(selRecordActivity);
				if (result)
				{
					setLastSettingLabels();
					setTotalTimesLabels();
					startTimer();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("aa2467f9-bddd-4c6b-a3bd-b4554936314f"), ex, "Problém při ukládání pauzy.");
			}
		}

		private void onLasRecordActivityChange()
		{
			_lastRecordActivityHangler.Invoke(this, EventArgs.Empty);
		}

		private async void onLoadDataAfterChangeProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (cmbProjects.SelectedItem != null)
				{
					var projectId = ((Project)cmbProjects.SelectedItem).Id;
					var subModules = await _projectProvider.GetSubModulesAsync(projectId);
					cmbSubModule.ItemsSource = subModules;
					if (subModules != null && subModules.Count > 0)
						cmbSubModule.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("01c4c769-2c67-48ab-9c15-156609f49e0d"), ex, "Problém při přepnutí projectu.");
			}
		}

		private void onOpenWindowReportRecords_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var report = new RecordListWindow(_mainStory, _context);
				report.Show();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("9d0de4e8-d44e-4404-bf96-ca312a7556fa"), ex, "Problém při otvírání okna s reportem.");
			}
		}

		private void onOpenWindowSetting_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var window = new SettingWindow(_mainStory);
				window.ShowDialog();

				loadProjects();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("d070dfde-f70c-4aa3-a486-3d97d4f1688a"), ex, "Problém při otvírání okna s nastavením.");
			}
		}

		private void onOpenWindowShifts_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var window = new ShiftsPlanWindow(_mainStory);
				var result = window.ShowDialog();

				loadShifts();

				if (_lastRecordActivity != null && _lastRecordActivity.Shift != null)
				{
					var curretnSeletDate = _lastRecordActivity.Shift.StartDate;
					cmbShift.SelectedItem = _shiftCmbs.FirstOrDefault(x => x.StartDate == curretnSeletDate);
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("1234980a-9768-40b0-bbb6-9ab6927adb1a"), ex, "Problém při otvírání okna se směnami.");
			}
		}

		private void onSetLabelsHandler(object sender, EventArgs eventArgs) => setLastSettingLabels();

		private void setLastSettingLabels()
		{
			if (_lastRecordActivity != null)
			{
				lblActivity.Text = _lastRecordActivity?.Activity?.Name ?? "";
				lblProject.Text = _lastRecordActivity?.Project?.Name ?? "";
				lblSubModule.Text = _lastRecordActivity?.SubModule?.Name ?? "";
				lblStartTime_time.Text = _lastRecordActivity?.StartDateTime.ToString("HH:mm:ss");
				lblStartTime_date.Text = _lastRecordActivity?.StartDateTime.ToString("dd.MM.yy");
				lblShift_date.Text = getShiftDateText();
			}
		}
		private void setTotalTimesLabels()
		{
			var shiftGuidId = _lastRecordActivity?.Shift?.GuidId ?? Guid.Empty;

			var calcHours_forToday_fromDb = _reportProvider.GetActualSumaryHours();
			var calcHours_forShift_fromDb = _reportProvider.GetSumaryHoursShift(shiftGuidId);

			var totalTimes = TotalTimesService.Get(calcHours_forToday_fromDb, calcHours_forShift_fromDb, _lastRecordActivity);

			lblTime_time.Text = totalTimes.ActualTime.ToString(_formTime);
			lblTotalTime.Content = totalTimes.ActualTime.ToString(_formTime);

			lblWorkerTime.Content = totalTimes.WorkTime.ToString(_formTime);
			lblPauseTime.Content = totalTimes.PauseTime.ToString(_formTime);
			lblTotalTime.Content = totalTimes.TotalTime.ToString(_formTime);

			lblWorkShiftTime.Content = totalTimes.WorkShiftTime.ToString(_formTime);
			lblPauseShiftTime.Content = totalTimes.PauseShiftTime.ToString(_formTime);
			lblTotalShiftTime.Content = totalTimes.TotalShiftTime.ToString(_formTime);
		}

		private void startTimer()
		{
			if (!_dispatcherTimer.IsEnabled)
				_dispatcherTimer.Start();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var screenWidth = SystemParameters.WorkArea.Width;
			var screenHeight = SystemParameters.WorkArea.Height;

			// Umístění okna na pravý dolní roh
			this.Left = screenWidth - this.Width;
			this.Top = screenHeight - this.Height;
		}
	}
}