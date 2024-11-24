using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;
using TimeTracker.Stories;
using TimeTracker.Windows.Models;
using TimeTracker.Windows.Reports.Services;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.Windows.Reports
{
	public partial class RecordListWindow : Window
	{
		private MainStory _mainStoru;
		public ICollection<Activity> Activities { get; set; }
		public ICollection<Project> Projects { get; set; }
		public ICollection<Shift> Shifts { get; set; }
		public ICollection<TypeShift> TypeShifts { get; set; }
		private RecordProvider _recordProvider { get; set; }
		private ActivityProvider _activityProvider { get; set; }
		public ICollection<RecordActivityReport> RecordActivityReportList { get; set; }

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
			Projects = convertCollection<Project>(projects);

			var shiftProvider = new ShiftProvider();
			var shifts = shiftProvider.GetShifts();
			Shifts = convertCollection<Shift>(shifts);

			var typeShifts = shiftProvider.GetTypeShifts();
			TypeShifts = convertCollection<TypeShift>(typeShifts, false);

			createList();

			DataContext = this;
		}

		private ICollection<T> convertCollection<T>(ICollection<T> collection, bool emptyValue = true)
			where T : new()
		{
			var list = new ObservableCollection<T>();

			if (emptyValue)
				list.Add(new());

			foreach (var item in collection)
			{
				list.Add(item);
			}

			return list;
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
						var repObj = new RecordActivityReport(record, Activities, Projects, Shifts, TypeShifts);
						repObj.ActivityIndex = getIndex(Activities, repObj.ActivityId);
						repObj.ProjectIndex = getIndex(Projects, repObj.ProjectId);
						repObj.ShiftIndex = getIndex(Shifts, repObj.ShiftGuidId);
						repObj.TypeShiftIndex = getIndex(TypeShifts, repObj.TypeShiftId);
						return repObj;
						;
					}).ToList();

					RecordActivityReportList = list;
					dtgRecordActivities.ItemsSource = RecordActivityReportList;
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

		private void dtgRecordActivities_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			var editedRow = e.Row.Item as RecordActivityReport;
			if (editedRow != null)
			{
				var convertDataTrimeStringStart = Convert.ToDateTime(editedRow.StartDate).ToString("dd.MM.yyyy");
				var startDateTime = Convert.ToDateTime(convertDataTrimeStringStart	+ " " + editedRow.StartTime);
				var convertDataTrimeStringEnd = Convert.ToDateTime(editedRow.EndDate).ToString("dd.MM.yyyy");
				var endDateTime = Convert.ToDateTime(convertDataTrimeStringEnd + " " + editedRow.EndTime);

				var recordActivity = new RecordActivity(editedRow.GuidId, startDateTime, endDateTime, editedRow.Activity, editedRow.TypeShift, editedRow.Shift, editedRow.Project, editedRow.SubModule, editedRow.Description);
				_recordProvider.SaveRecord(recordActivity);
				var newRecordActivityReport = new RecordActivityReport(recordActivity)
				{
					Activity = editedRow.Activity,
					ActivityIndex = getIndex(Activities, editedRow?.Activity?.Id),
					TypeShift = editedRow.TypeShift,
					TypeShiftIndex = getIndex(TypeShifts, editedRow?.TypeShift?.Id),
					Shift = editedRow.Shift,
					ShiftIndex = getIndex(Shifts, editedRow?.Shift?.GuidId),
					Project = editedRow.Project,
					ProjectIndex = getIndex(Projects, editedRow?.Project?.Id),
					SubModule = editedRow.SubModule,
					SubModuleIndex = 0
				};

				RecordActivityReportList.Where(x => x.GuidId == editedRow.GuidId).Select(x => x = newRecordActivityReport);
			}
		}
	}
}