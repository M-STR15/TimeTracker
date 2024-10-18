using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using TimerTracker.Models;
using TimerTracker.Providers;
using TimerTracker.Windows;

namespace TimerTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DatabaseProvider _databaseProvider;
		private Activity _activity;
		private Project _prj;
		private DispatcherTimer _dispatcherTimer;
		private DateTime _startTimeActivity;

		private Project _project
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

		public MainWindow(DatabaseProvider databaseProvider)
		{
			InitializeComponent();
			_databaseProvider = databaseProvider;

			cmbActivities.ItemsSource = _databaseProvider.GetActivities();
			cmbActivities.DisplayMemberPath = "Name";
			cmbActivities.SelectedIndex = 0;

			cmbProjects.ItemsSource = _databaseProvider.GetProjects();
			cmbProjects.DisplayMemberPath = "Name";
			cmbProjects.SelectedIndex = 0;

			_dispatcherTimer = new DispatcherTimer();
			_dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
			_dispatcherTimer.Tick += _dispatcherTimer_Tick;
		}

		private void _dispatcherTimer_Tick(object? sender, EventArgs e)
		{
			var time = (DateTime.Now - _startTimeActivity);
			lblTime_time.Content = time.ToString(@"hh\:mm\:ss");
		}

		private void btnActivate_Click(object sender, RoutedEventArgs e)
		{
			_activity = ((Activity)cmbActivities.SelectedItem);
			_project = ((Project)cmbProjects.SelectedItem);

			var description = getTextFromRichTextBox(rtbDescription);
			addActivite(_activity, _project, description);

			_dispatcherTimer.Start();
		}

		private void addActivite(Activity activity, Project project, string description = "")
		{
			_startTimeActivity = DateTime.Now;

			lblActivity.Content = activity.Name;
			lblProject.Content = project.Name;
			lblStartTime_time.Content = _startTimeActivity.ToString("HH:mm:ss");
			lblStartTime_date.Content = _startTimeActivity.ToString("dd.MM.yyyy");

			var record = new RecordActivity(_startTimeActivity, activity.Id, project.Id, description);
			_databaseProvider.SaveRecord(record);
		}

		private string getTextFromRichTextBox(RichTextBox richTextBox)
		{
			TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
			return textRange.Text;
		}

		private void mbtnRecordList_Click(object sender, RoutedEventArgs e)
		{
			var report = new RecordListWindow(_databaseProvider);
			report.ShowDialog();
		}

		private void btnPause_Click(object sender, RoutedEventArgs e)
		{
			var activity = new Activity()
			{
				Id = (int)eActivity.Pause,
				Name = eActivity.Pause.ToString()
			};
			addActivite(activity, _project);

			_dispatcherTimer.Stop();
		}

		private void btnEndShift_Click(object sender, RoutedEventArgs e)
		{
			var activity = new Activity()
			{
				Id = (int)eActivity.Stop,
				Name = eActivity.Stop.ToString()
			};
			addActivite(activity, _project);

			_dispatcherTimer.Stop();
		}
	}
}