using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using TimerTracker.BE.DB.Models;
using TimerTracker.BE.DB.Models.Enums;
using TimerTracker.Models;
using TimerTracker.Stories;
using TimerTracker.ViewModels;

namespace TimerTracker.Windows
{
	public partial class MainWindow : Window
	{
		private readonly MainStory _mainStory;
		private DispatcherTimer _dispatcherTimer;
		private Project? _prj;
		private ShiftCmb? _selectShift;
		private Activity? _selectActivity;
		private DateTime _startTimeActivity;
		private List<ShiftCmb> _shiftCmbs;
		public MainWindow(MainStory mainStory)
		{
			this.DataContext = new BaseViewModel("Timer tracker");
			InitializeComponent();
			_mainStory = mainStory;

			//loadActivities();
			loadProjects();
			loadShifts();

			_dispatcherTimer = new DispatcherTimer();
			_dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
			_dispatcherTimer.Tick += _dispatcherTimer_Tick;
		}

		//private void loadActivities()
		//{
		//	cmbActivities.ItemsSource = _mainStory.ContainerStore.GetActivityProvider().GetActivities();
		//	cmbActivities.DisplayMemberPath = "Name";
		//	cmbActivities.SelectedIndex = 0;
		//}
		private void loadProjects()
		{
			cmbProjects.ItemsSource = null;
			cmbProjects.ItemsSource = _mainStory.ContainerStore.GetProjectProvider().GetProjects();
			cmbProjects.DisplayMemberPath = "Name";
			cmbProjects.SelectedIndex = 0;
		}
		private void loadShifts()
		{
			var currentDate = DateTime.Now;
			var getList = _mainStory.ContainerStore.GetShiftProvider().GetShifts(currentDate.AddDays(-7), currentDate);
			_shiftCmbs = getList.Select(x => new ShiftCmb(x)).OrderByDescending(x => x.StartDate).ToList();
			_shiftCmbs.Add(new ShiftCmb());
			cmbShift.ItemsSource = _shiftCmbs.OrderByDescending(x => x.StartDate);
			cmbShift.DisplayMemberPath = "StartDateStr";
			cmbShift.SelectedIndex = 0;
		}

		private Project? _selectProject
		{
			get => _prj;
			set
			{
				_prj = value;
				var isEditBtn = (value != null);
				btnPause.IsEnabled = isEditBtn;
				btnEndShift.IsEnabled = isEditBtn;
			}
		}
		private void _dispatcherTimer_Tick(object? sender, EventArgs e)
		{
			var time = (DateTime.Now - _startTimeActivity);
			lblTime_time.Content = time.ToString(@"hh\:mm\:ss");
		}

		private bool addActivite(Activity activity, Project? project = null, ShiftCmb? shift = null, string description = "")
		{
			var startTimeActivity = DateTime.Now;

			var record = new RecordActivity();
			if (shift != null && shift.GuidId != Guid.Empty)
				record = new RecordActivity(startTimeActivity, activity.Id, shift.GuidId, project?.Id ?? null, description);
			else
				record = new RecordActivity(startTimeActivity, activity.Id, project?.Id ?? null, description);

			var result = _mainStory.ContainerStore.GetRecordProvider().SaveRecord(record);
			if (true)
			{
				_selectActivity = activity;
				_selectProject = project;
				_startTimeActivity = startTimeActivity;
			}

			return result;
		}

		private void changeLabels()
		{
			lblActivity.Content = _selectActivity?.Name ?? "";
			lblProject.Content = _selectProject?.Name ?? "";
			lblStartTime_time.Content = _startTimeActivity.ToString("HH:mm:ss");
			lblStartTime_date.Content = _startTimeActivity.ToString("dd.MM.yy");
			lblShift_date.Content = _selectShift.StartDateStr;
		}

		private void btnActivate_Click(object sender, RoutedEventArgs e)
		{
			var activity = new Activity()
			{
				Id = (int)eActivity.Start,
				Name = eActivity.Start.ToString()
			};

			_selectProject = (Project)cmbProjects.SelectedItem;
			_selectShift = (ShiftCmb)cmbShift.SelectedItem;

			var description = getTextFromRichTextBox(rtbDescription);
			var result = addActivite(activity, _selectProject, _selectShift, description);
			if (result)
			{
				changeLabels();
				_dispatcherTimer.Start();
			}
		}
		private void btnEndShift_Click(object sender, RoutedEventArgs e)
		{
			var activity = new Activity()
			{
				Id = (int)eActivity.Stop,
				Name = eActivity.Stop.ToString()
			};

			var result = addActivite(activity);
			if (result)
			{
				changeLabels();
				_dispatcherTimer.Stop();
			}
		}

		private void btnPause_Click(object sender, RoutedEventArgs e)
		{
			var activity = new Activity()
			{
				Id = (int)eActivity.Pause,
				Name = eActivity.Pause.ToString()
			};

			var result = addActivite(activity);
			if (result)
			{
				changeLabels();
				_dispatcherTimer.Start();
			}
		}

		private string getTextFromRichTextBox(RichTextBox richTextBox)
		{
			var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
			return textRange.Text;
		}

		private void mbtnRecordList_Click(object sender, RoutedEventArgs e)
		{
			var report = new RecordListWindow(_mainStory);
			report.Show();
		}
		private void mbtnShifts_Click(object sender, RoutedEventArgs e)
		{
			var window = new ShiftsPlanWindow(_mainStory);
			var result = window.ShowDialog();

			loadShifts();

			if (_selectShift != null)
			{
				var curretnSeletDate = _selectShift.StartDate;
				cmbShift.SelectedItem = _shiftCmbs.FirstOrDefault(x => x.StartDate == curretnSeletDate);
			}
		}

		private void mbtnSettings_Click(object sender, RoutedEventArgs e)
		{
			var window = new SettingWindow(_mainStory);
			window.ShowDialog();
		}
	}
}