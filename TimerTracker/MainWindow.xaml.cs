using System.Windows;
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
			lblActivity.Content = ((Activity)cmbActivities.SelectedItem).Name;
			lblProject.Content = ((Project)cmbProjects.SelectedItem).Name;
			lblStartTime_time.Content = DateTime.Now.ToString("HH:mm:ss");
			lblStartTime_date.Content = DateTime.Now.ToString("dd.MM.yyyy");
		}
	}
}