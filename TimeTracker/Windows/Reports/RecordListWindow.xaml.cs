using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;
using TimeTracker.Stories;
using TimeTracker.Windows.Reports.Services;

namespace TimeTracker.Windows.Reports
{
	internal class ReportObj
	{
		public string StartDate { get; set; }
		public string StartTime { get; set; }
		public string EndDate { get; set; }
		public string EndTime { get; set; }
		public string Activity { get; set; }
		public string TotalTime { get; set; }
		public string Project { get; set; }
		protected DateTime EndTimeDt { get; set; }
		protected DateTime StartTimeDt { get; set; }
		protected TimeSpan TotalTimeTs { get; private set; }
		public string Description { get; set; }
		public bool CanEdit { get; set; }

		internal ReportObj(DateTime startTimeDt, DateTime endTimeDt, string activity, string project, string description)
		{
			StartTimeDt = startTimeDt;
			EndTimeDt = endTimeDt;

			StartDate = StartTimeDt.ToString("dd.MM.yyyy");
			StartTime = StartTimeDt.ToString("HH:mm:ss");
			EndDate = EndTimeDt.ToString("dd.MM.yyyy");
			EndTime = EndTimeDt.ToString("HH:mm:ss");
			TotalTime = TotalTimeTs.ToString(@"hh\:mm\:ss");
			Activity = activity;
			Project = project;
			TotalTimeTs = EndTimeDt - StartTimeDt;
			TotalTime = TotalTimeTs.ToString(@"hh\:mm\:ss");
		}
	}

	public partial class RecordListWindow : Window
	{
		private MainStory _mainStoru;

		public ICollection<string> Activities { get; set; }
		public ICollection<string> Projects { get; set; }

		public RecordListWindow(MainStory mainStore)
		{
			InitializeComponent();
			_mainStoru = mainStore;

			Activities = new ObservableCollection<string>(Enum.GetNames<eActivity>());
			cmbMonth.ItemsSource = new ReportParameterService().Monts;
			cmbMonth.SelectedIndex = 0;

			var projectProvider = new ProjectProvider();
			var projects = projectProvider.GetProjects();
			Projects = new ObservableCollection<string>(from pr in projects select pr.Name);

			createList();

			DataContext = this;
		}
		private void onCmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			createList();
		}

		private void createList()
		{
			if (cmbMonth.Text != "")
			{
				var startTime = Convert.ToDateTime("1." + cmbMonth.SelectedItem);
				var endTime = startTime.AddMonths(1);
				var origList = _mainStoru.ContainerStore.GetRecordProvider().GetRecords(startTime, endTime);
				if (origList != null)
				{
					var list = origList.Select((record, index) =>
					{
						var startTimeDt = record.StartTime;
						var endTimeDt = (origList.Count != (index + 1) ? origList[index + 1].StartTime : DateTime.Now);
						var activity = record.Activity.Name;
						var project = record?.Project?.Name ?? "";
						var description = record?.Description ?? "";

						return new ReportObj(startTimeDt, endTimeDt, activity, project, description)
						;
					});
					dtgRecordActivities.ItemsSource = list.ToList();
				}
				lblCount.Content = (origList?.Count ?? 0).ToString();
			}
		}


		private void dtgRecordActivities_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			var row = e.Row.Item as ReportObj;
			if (row == null || !row.CanEdit) // Vlastnost CanEdit určuje, zda lze řádek upravit
			{
				e.Cancel = true;
			}
		}

		private void dtgRecordActivities_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
		{
			// Zpracujte úpravy řádku
			var editedRow = e.Row.Item; // Upravená data
										// Zde můžete uložit změny do databáze nebo kolekce
										//MessageBox.Show($"Řádek byl upraven: {editedRow}");
		}

		private void dtgRecordActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgRecordActivities.SelectedItem is ReportObj selectedRow)
			{
				selectedRow.CanEdit = true;
			}
		}
	}
}