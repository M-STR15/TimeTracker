using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.Models;
using TimeTracker.Services;
using TimeTracker.Stories;
using TimeTracker.ViewModels;
using TimeTracker.Windows.Reports;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.Windows
{
	public partial class MainWindow : Window, IDisposable
	{
		private readonly MainStory _mainStory;
		private DispatcherTimer _dispatcherTimer;
		private EventLogService _eventLogService;
		private EventHandler _lastRecordActivityHangler;
		private RecordActivity _lra;
		private ProjectRepository _projectProvider;

		private RecordRepository _recordProvider;

		private ReportRepository _reportProvider;

		private List<ShiftCmb> _shiftCmbs = new();

		private ShiftRepository _shiftProvider;

		private int _totalActivityTimeBeforeInSecond;

		private List<TypeShift> _typeShifts = new();

		public MainWindow(MainStory mainStory)
		{
			_eventLogService = new EventLogService();
			try
			{
				this.DataContext = new BaseViewModel("Timer tracker");
				InitializeComponent();
				_mainStory = mainStory;

				_shiftProvider = _mainStory.ContainerStore.GetShiftProvider();
				_projectProvider = _mainStory.ContainerStore.GetProjectProvider();
				_recordProvider = _mainStory.ContainerStore.GetRecordProvider();
				_reportProvider = _mainStory.ContainerStore.GetReportProvider();

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

				_lastRecordActivity = _recordProvider.GetLastRecordActivity();

				if (_lastRecordActivity != null && _lastRecordActivity.ActivityId != (int)eActivity.Stop)
					_dispatcherTimer.Start();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("def4de22-6fc4-4ab4-9f7e-0aba95e5337c"), ex, "Problém při otevírání hlavního okna.");
			}

		}

		private RecordActivity _lastRecordActivity
		{
			get => _lra;
			set
			{
				_lra = value;
				onLasRecordActivityChange();
			}
		}
		public void Dispose()
		{
			_lastRecordActivityHangler -= onSetLabelsHandler;
		}

		private void _dispatcherTimer_Tick(object? sender, EventArgs e)
		{
			setlblTime();
		}

		private bool addActivite(RecordActivity recordActivity)
		{
			return addActivite(recordActivity.Activity, recordActivity.TypeShift, recordActivity.Project, recordActivity.SubModule, recordActivity.Shift, recordActivity?.Description ?? "");
		}

		private bool addActivite(Activity activity, TypeShift typeShift, Project? project = null, SubModule? subModule = null, Shift? shift = null, string description = "")
		{
			try
			{
				var startTimeActivity = DateTime.Now;

				var record = new RecordActivity();
				if (shift != null && shift.GuidId != Guid.Empty)
					record = new RecordActivity(startTimeActivity, activity.Id, shift.GuidId, typeShift.Id, project?.Id ?? null, subModule?.Id ?? null, description);
				else
					record = new RecordActivity(startTimeActivity, activity.Id, typeShift?.Id ?? null, project?.Id ?? null, subModule?.Id ?? null, description);

				var result = _recordProvider.SaveRecord(record);
				if (result != null)
				{
					_lastRecordActivity = new RecordActivity(result.GuidId, startTimeActivity, null, activity, typeShift, shift, project, subModule, description);
				}

				return result != null;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private string getTextFromRichTextBox(RichTextBox richTextBox)
		{
			var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
			return textRange.Text;
		}

		private double getTimeShift(Shift? shift, eActivity activity, double actualActivityInSeconds)
		{
			return (shift != null && shift.GuidId != Guid.Empty && activity == eActivity.Start) ? actualActivityInSeconds : 0;
		}

		private void loadProjects()
		{
			cmbProjects.ItemsSource = null;
			cmbProjects.ItemsSource = _projectProvider.GetProjects();
			cmbProjects.SelectedIndex = 0;
		}

		private void loadShifts()
		{
			var currentDate = DateTime.Now;
			var getList = _shiftProvider.GetShifts(currentDate.AddDays(-7), currentDate.AddDays(3));
			_shiftCmbs = getList.Select(x => new ShiftCmb(x)).OrderByDescending(x => x.StartDate).ToList();
			_shiftCmbs.Add(new ShiftCmb());
			cmbShift.ItemsSource = _shiftCmbs.OrderByDescending(x => x.StartDate);
			cmbShift.SelectedIndex = 0;
		}

		private void loadTypeShifts()
		{
			_typeShifts = _shiftProvider.GetTypeShiftsForMainWindow();
			cmbTypeShift.ItemsSource = _typeShifts;
			cmbTypeShift.SelectedIndex = 0;
		}

		private void mbtnActivitiesOverDays_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var window = new ActivitiesOverDaysWindow();
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
				var window = new PlanVsRealitaWorkHoursWindow();
				window.Show();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("ebb2c705-f5dd-4cc9-81b6-99caf462292a"), ex, "Problém při otvírání reportu.");
			}
		}

		private void onActionAfterClickActivate_Click(object sender, RoutedEventArgs e)
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

				var result = addActivite(selRecordActivity);
				if (result)
				{
					rtbDescription.Document.Blocks.Clear();
					setLabels();
					startTimer();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("51905c9e-51c5-4fa4-bac2-fe60543bc170"), ex, "Problém při vkláání akivity.");
			}
		}

		private void onActionAfterClickEndShift_Click(object sender, RoutedEventArgs e)
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

				var result = addActivite(selRecordActivity);
				if (result)
				{
					setLabels();
					_dispatcherTimer.Stop();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(new Guid("d769b7d8-adea-4011-babe-4415f3258467"), ex, "Problém při ukládání konce směny.");
			}
		}

		private void onActionAfterClickPause_Click(object sender, RoutedEventArgs e)
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

				var result = addActivite(selRecordActivity);
				if (result)
				{
					setLabels();
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
		private void onLoadDataAfterChangeProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (cmbProjects.SelectedItem != null)
				{
					var projectId = ((Project)cmbProjects.SelectedItem).Id;
					var subModules = _projectProvider.GetSubModules(projectId);
					cmbSubModule.ItemsSource = subModules;
					if (subModules.Count > 0)
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
				var report = new RecordListWindow(_mainStory);
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

		private void onSetLabelsHandler(object sender, EventArgs eventArgs)
		{
			setLabels();
		}
		private void setLabels()
		{
			setlblTime();
			if (_lastRecordActivity != null)
			{
				lblActivity.Text = _lastRecordActivity.Activity?.Name ?? "";
				lblProject.Text = _lastRecordActivity.Project?.Name ?? "";
				lblSubModule.Text = _lastRecordActivity.SubModule?.Name ?? "";
				lblStartTime_time.Text = _lastRecordActivity?.StartDateTime.ToString("HH:mm:ss");
				lblStartTime_date.Text = _lastRecordActivity?.StartDateTime.ToString("dd.MM.yy");

				var shift = _lastRecordActivity?.Shift;

				if (shift != null)
				{
					var cmdShift = new ShiftCmb(shift);
					lblShift_date.Text = cmdShift?.StartDateStr ?? "";
				}
				else
				{
					lblShift_date.Text = "";
				}
			}
		}
		private void setlblTime()
		{
			var actualActivityInSeconds = 0.00;

			if (_lastRecordActivity != null && _lastRecordActivity.ActivityId != (int)eActivity.Stop)
			{
				var time = (DateTime.Now - _lastRecordActivity.StartDateTime);
				actualActivityInSeconds = time.TotalSeconds;
				lblTime_time.Text = time.ToString(@"hh\:mm\:ss");

			}
			else
			{
				lblTime_time.Text = "00:00:00";
			}

			lblTotalTime.Content = TimeSpan.FromSeconds(_totalActivityTimeBeforeInSecond + actualActivityInSeconds).ToString(@"hh\:mm\:ss");

			if (_lastRecordActivity != null)
			{
				var selShift = _lastRecordActivity.Shift;
				var activity = (eActivity)_lastRecordActivity.ActivityId;

				var workHours_actual = (activity == eActivity.Start) ? actualActivityInSeconds : 0;
				var pauseHours_actual = (activity == eActivity.Pause) ? actualActivityInSeconds : 0;

				var filterToday = DateTime.Now;
				var workHours_fromDB = _reportProvider.GetWorkHours(filterToday);
				var pauseHours_fromDB = _reportProvider.GetPauseHours(filterToday);

				var workShiftHours_actual = getTimeShift(selShift, eActivity.Start, actualActivityInSeconds);
				var pauseShifteHours_actual = getTimeShift(selShift, eActivity.Pause, actualActivityInSeconds);

				var workShiftHours_fromDB = selShift != null ? _reportProvider.GetWorkHoursShift(selShift.GuidId) : 0;
				var pauseShiftHours_fromDB = selShift != null ? _reportProvider.GetPauseHoursShift(selShift.GuidId) : 0;

				lblWorkerTime.Content = TimeSpan.FromSeconds(workHours_fromDB + workHours_actual).ToString(@"hh\:mm\:ss");
				lblPauseTime.Content = TimeSpan.FromSeconds(pauseHours_fromDB + pauseHours_actual).ToString(@"hh\:mm\:ss");
				lblTotalTime.Content = TimeSpan.FromSeconds(workHours_fromDB + pauseHours_fromDB + workHours_actual + pauseHours_actual).ToString(@"hh\:mm\:ss");

				lblWorkShiftTime.Content = TimeSpan.FromSeconds(workShiftHours_fromDB + workShiftHours_actual).ToString(@"hh\:mm\:ss");
				lblPauseShiftTime.Content = TimeSpan.FromSeconds(pauseShiftHours_fromDB + pauseShifteHours_actual).ToString(@"hh\:mm\:ss");
				lblTotalShiftTime.Content = TimeSpan.FromSeconds(workShiftHours_fromDB + pauseShiftHours_fromDB + workShiftHours_actual + pauseShifteHours_actual).ToString(@"hh\:mm\:ss");
			}
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