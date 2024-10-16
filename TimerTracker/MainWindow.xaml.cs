using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TimerTracker.Models;
using TimerTracker.Providers;

namespace TimerTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DatabaseProvider _databaseProvider;
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
		}

		private void btnActivate_Click(object sender, RoutedEventArgs e)
		{
			var dateActivity = DateTime.Now;
			var activity = ((Activity)cmbActivities.SelectedItem);
			var project = ((Project)cmbProjects.SelectedItem);

			lblActivity.Content = activity.Name;
			lblProject.Content = project.Name;
			lblStartTime_time.Content = dateActivity.ToString("HH:mm:ss");
			lblStartTime_date.Content = dateActivity.ToString("dd.MM.yyyy");

			var description = getTextFromRichTextBox(rtbDescription);

			var record = new RecordActivity(dateActivity, activity.Id, project.Id, description);
			_databaseProvider.SaveRecord(record);
		}

		private string getTextFromRichTextBox(RichTextBox richTextBox)
		{
			TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
			return textRange.Text;
		}
	}
}