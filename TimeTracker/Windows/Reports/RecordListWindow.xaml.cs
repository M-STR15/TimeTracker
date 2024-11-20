using System;
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
		public new string? StartDate { get; set; }
		public new string? StartTime { get; set; }
		public new string? EndDate { get; set; }

		public int ActivityIndex { get; set; }
		public int ProjectIndex { get; set; }
		public int ShiftIndex { get; set; }
		public int SubModuleIndex { get; set; }
		public int TypeShiftIndex { get; set; }

		public new string? EndTime { get; set; }

		public new TimeSpan? TotalTime { get; set; }
	}

	public partial class RecordListWindow : Window
	{
		private MainStory _mainStoru;

		public ICollection<Activity> Activities { get; set; }
		public ICollection<Project> Projects { get; set; }
		public ICollection<Shift> Shifts { get; set; }
		public ICollection<TypeShift> TypeShifts { get; set; }

		private RecordProvider _recordProvider { get; set; }
		private ActivityProvider _activityProvider { get; set; }

		public RecordListWindow(MainStory mainStore)
		{
			InitializeComponent();
			_mainStoru = mainStore;
			_recordProvider = _mainStoru.ContainerStore.GetRecordProvider();
			_activityProvider = _mainStoru.ContainerStore.GetActivityProvider();

			Activities = _activityProvider.GetActivities();
			new ObservableCollection<string>(Enum.GetNames<eActivity>());
			cmbMonth.ItemsSource = new ReportParameterService().Monts;
			cmbMonth.SelectedIndex = 0;

			var projectProvider = new ProjectProvider();
			var projects = projectProvider.GetProjects();
			Projects = new ObservableCollection<Project>(from pr in projects select pr);
			var shiftProvider = new ShiftProvider();
			var shifts = shiftProvider.GetShifts();
			Shifts = new ObservableCollection<Shift>(from sh in shifts select sh);

			var typeShifts = shiftProvider.GetTypeShifts();
			TypeShifts = new ObservableCollection<TypeShift>(from ts in typeShifts select ts);
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
				var origList = _recordProvider.GetRecords(startTime, endTime);
				if (origList != null)
				{
					var list = origList.Select((record, index) =>
					{
						var repObj = new RecordActivityReport(record);
						repObj.ActivityIndex = getIndex(Activities, repObj.ActivityId);
						repObj.ProjectIndex = getIndex(Projects, repObj.ProjectId);
						repObj.ShiftIndex = getIndex(Shifts, repObj.ShiftGuidId);
						repObj.TypeShiftIndex = getIndex(TypeShifts, repObj.TypeShiftId);
						return repObj;
						;
					}).ToList();
					dtgRecordActivities.ItemsSource = list;
				}
				lblCount.Content = (origList?.Count ?? 0).ToString();
			}
		}

		private int getIndex<T>(IEnumerable<T> collection, int? objectId)
			where T : IIdentifiable
		{
			return collection.Select((radek, index) => new { radek, index }).FirstOrDefault(x => x.radek.Id == objectId)?.index ?? -1;
		}

		private int getIndex<T>(IEnumerable<T> collection, Guid? objectId)
			where T : IIdentifiableGuid
		{
			return collection.Select((radek, index) => new { radek, index }).FirstOrDefault(x => x.radek.GuidId == objectId)?.index ?? -1;
		}

		private void dtgRecordActivities_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			var row = e.Row.Item as RecordActivityReport;
			if (row == null) // Vlastnost CanEdit určuje, zda lze řádek upravit
			{
				e.Cancel = true;
			}
		}

		private void dtgRecordActivities_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
		{
			// Zpracujte úpravy řádku
			var editedRow = e.Row.Item as RecordActivityReport;
			if (editedRow == null) // Vlastnost CanEdit určuje, zda lze řádek upravit
			{
				editedRow.ProjectId = 1;
				//editedRow.ActivityId = Activities.ToList().FirstOrDefault(x=>x);
				editedRow.ShiftGuidId = Guid.Empty;
				editedRow.SubModuleId = 1;
				editedRow.TypeShiftId = 1;
				_recordProvider.SaveRecord(editedRow);
			}
		}

		private void dtgRecordActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgRecordActivities.SelectedItem is RecordActivity selectedRow)
			{

			}
		}
	}
}