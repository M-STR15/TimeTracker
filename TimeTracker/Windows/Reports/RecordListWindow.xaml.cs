using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;
using TimeTracker.Stories;
using TimeTracker.Windows.Reports.Services;

namespace TimeTracker.Windows.Reports
{
	internal class RecordActivityReport : RecordActivity
	{

		public RecordActivityReport(RecordActivity recordActivity) : base(recordActivity.GuidId, recordActivity.StartDateTime, recordActivity.EndDateTime, recordActivity.Activity, recordActivity.TypeShift, recordActivity.Shift, recordActivity.Project, recordActivity.SubModule, recordActivity.Description)
		{
			StartDate = base.StartDate;
			StartTime = base.StartTime;
			EndDate = base.EndDate;
			EndTime = base.EndTime;
			TotalTime = TimeSpan.FromSeconds(base.DurationSec);
		}
		public new string? StartDate
		{
			get;
			set;
		}
		public new string? StartTime
		{
			get;
			set;
		}
		public new string? EndDate
		{
			get;
			set;
		}

		public new string? EndTime
		{
			get;
			set;
		}

		public new TimeSpan? TotalTime
		{
			get;
			set;
		}
	}

	public partial class RecordListWindow : Window
	{
		private MainStory _mainStoru;

		public ICollection<string> Activities { get; set; }
		public ICollection<string> Projects { get; set; }

		public ICollection<string> Shifts { get; set; }

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
			var shiftProvider = new ShiftProvider();
			var shifts = shiftProvider.GetShifts();
			Shifts = new ObservableCollection<string>(from sh in shifts select sh.StartDate.ToString("dd.MM.yyyy"));

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
						return new RecordActivityReport(record)
						;
					}).ToList();
					dtgRecordActivities.ItemsSource = list;
				}
				lblCount.Content = (origList?.Count ?? 0).ToString();
			}
		}

		private void dtgRecordActivities_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			var row = e.Row.Item as RecordActivity;
			if (row == null) // Vlastnost CanEdit určuje, zda lze řádek upravit
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
			if (dtgRecordActivities.SelectedItem is RecordActivity selectedRow)
			{

			}
		}
	}
}