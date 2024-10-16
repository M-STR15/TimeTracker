using System.Windows;
using TimerTracker.Providers;

namespace TimerTracker.Windows
{
	/// <summary>
	/// Interaction logic for RecordListWindow.xaml
	/// </summary>
	public partial class RecordListWindow : Window
	{
		private DatabaseProvider _databaseProvider;
		public RecordListWindow(DatabaseProvider databaseProvider)
		{
			InitializeComponent();
			_databaseProvider = databaseProvider;

			var list = _databaseProvider.GetRecords();

			dtgRecordActivities.ItemsSource = list;
			lblCount.Content = list.Count.ToString();
		}
	}
}
