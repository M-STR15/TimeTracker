using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using TimerTracker.Models;
using TimerTracker.Models.Database;
using TimerTracker.Models.Database.Enums;
using TimerTracker.Stories;
using TimerTracker.Windows;

namespace TimerTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainStory _mainStory;
		private DispatcherTimer _dispatcherTimer;
		private Project _prj;
		private ShiftCmb _selectShift;
		private Activity _selectActivity;
		private DateTime _startTimeActivity;
		private List<ShiftCmb> _shiftCmbs;
		public MainWindow(MainStory mainStory)
		{
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

		private Project _selectProject
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

		private void addActivite(Activity activity, Project? project = null, ShiftCmb? shift = null, string description = "")
		{
			_startTimeActivity = DateTime.Now;

			lblActivity.Content = activity.Name;
			lblProject.Content = project?.Name ?? "";
			lblStartTime_time.Content = _startTimeActivity.ToString("HH:mm:ss");
			lblStartTime_date.Content = _startTimeActivity.ToString("dd.MM.yy");
			lblShift_date.Content = _selectShift.StartDateStr;

			var record = new RecordActivity();
			if (shift == null)
				record = new RecordActivity(_startTimeActivity, activity.Id, project?.Id ?? null, description);
			else
				record = new RecordActivity(_startTimeActivity, activity.Id, shift.GuidId, project?.Id ?? null, description);

			_mainStory.ContainerStore.GetRecordProvider().SaveRecord(record);
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
			addActivite(activity, _selectProject, _selectShift, description);

			_dispatcherTimer.Start();
		}
		private void btnEndShift_Click(object sender, RoutedEventArgs e)
		{
			var activity = new Activity()
			{
				Id = (int)eActivity.Stop,
				Name = eActivity.Stop.ToString()
			};

			addActivite(activity, _selectProject);

			_dispatcherTimer.Stop();
		}

		private void btnPause_Click(object sender, RoutedEventArgs e)
		{
			var activity = new Activity()
			{
				Id = (int)eActivity.Pause,
				Name = eActivity.Pause.ToString()
			};

			addActivite(activity, _selectProject);

			_dispatcherTimer.Start();
		}

		private string getTextFromRichTextBox(RichTextBox richTextBox)
		{
			var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
			return textRange.Text;
		}

		private void mbtnRecordList_Click(object sender, RoutedEventArgs e)
		{
			var report = new RecordListWindow(_mainStory);
			report.ShowDialog();
		}
		private void mbtnShifts_Click(object sender, RoutedEventArgs e)
		{
			var window = new ShiftsPlanWindow(_mainStory);
			var result = window.ShowDialog();

			var curretnSeletDate = _selectShift.StartDate;
			loadShifts();

			cmbShift.SelectedItem = _shiftCmbs.FirstOrDefault(x => x.StartDate == curretnSeletDate);
		}
	}
}