using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;
using TimeTracker.Services;
using TimeTracker.Stories;
using TimeTracker.Windows.Models;
using TimeTracker.Windows.Reports.Services;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.Windows.Reports
{
	public partial class RecordListWindow : Window
	{
		private EventLogService _eventLogService;
		private MainStory _mainStoru;
		public RecordListWindow(MainStory mainStore)
		{
			_eventLogService = new EventLogService();
			try
			{
				InitializeComponent();
				_mainStoru = mainStore;
				_recordProvider = _mainStoru.ContainerStore.GetRecordProvider();
				_activityProvider = _mainStoru.ContainerStore.GetActivityProvider();

				Activities = _activityProvider.GetActivities();
				new ObservableCollection<string>(Enum.GetNames<eActivity>());

				var projectProvider = new ProjectProvider();
				var projects = projectProvider.GetProjects();
				Projects = convertCollection<Project>(projects).ToList();

				var shiftProvider = new ShiftProvider();
				var shifts = shiftProvider.GetShifts();
				Shifts = convertCollection<Shift>(shifts).ToList();

				var typeShifts = shiftProvider.GetTypeShifts();
				TypeShifts = convertCollection<TypeShift>(typeShifts, false).ToList();

				cmbMonth.ItemsSource = new ReportParameterService().Monts;
				cmbMonth.SelectedIndex = 0;

				setRecordActivityReportList();
				setRecordActivityReportListcollectionView();

				DataContext = this;

			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("1acd3061-db78-4f29-befa-ea1f72d2a036"), null, "Problém se spouštěním record list window.");
			}
		}

		public List<Activity> Activities { get; set; }
		public List<Project> Projects { get; set; }
		public ObservableCollection<RecordActivityReport> RecordActivityReportList { get; set; }
		//public ICollectionView RecordActivityReportListcollectionView { get; set; }
		public List<Shift> Shifts { get; set; }
		public List<TypeShift> TypeShifts { get; set; }
		private ActivityProvider _activityProvider { get; set; }
		private RecordProvider _recordProvider { get; set; }
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (sender is Button btn)
				{
					var guidDeleteRow = Guid.Parse(btn.Tag.ToString());
					var result = _recordProvider.DeleteRecord(guidDeleteRow);
					if (result)
					{
						var findRow = RecordActivityReportList.FirstOrDefault(x => x.GuidId == guidDeleteRow);
						if (findRow != null)
						{
							RecordActivityReportList.Remove(findRow);
						}
					}
				}
			}
			catch (Exception)
			{

			}
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

		private DateTime? dateTimegroupDateTime(string? date, string? time)
		{
			if (date == null && time == null)
			{
				return null;
			}
			else if (date != null && time == null)
			{
				var convertDataTrimeStringEnd = Convert.ToDateTime(date).ToString("dd.MM.yyyy");
				var endDateTime = Convert.ToDateTime(convertDataTrimeStringEnd + " " + "00:00:00");
				return endDateTime;
			}
			else
			{
				var convertDataTrimeStringEnd = Convert.ToDateTime(date).ToString("dd.MM.yyyy");
				var endDateTime = Convert.ToDateTime(convertDataTrimeStringEnd + " " + time);
				return endDateTime;
			}
		}

		private void dtgRecordActivities_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			var editedRow = e.Row.Item as RecordActivityReport;
			if (editedRow != null)
			{
				var startDateTime = (DateTime)dateTimegroupDateTime(editedRow.StartDate, editedRow.StartTime);
				var endDateTime = dateTimegroupDateTime(editedRow.EndDate, editedRow.EndTime);

				var activityId = (Activities[editedRow.ActivityIndex]).Id;
				var typeShiftId = getID_ActivityId(editedRow);
				var shiftGuidId = getD_ShiftGuidId(editedRow);
				var projectId = getID_ProjectId(editedRow);
				var subModuleId = (int?)null;


				var recordActivity = new RecordActivity(editedRow.GuidId, startDateTime, activityId, typeShiftId, projectId, subModuleId, shiftGuidId, endDateTime, editedRow?.Description);

				var updateRecordAct = _recordProvider.SaveRecord(recordActivity);
				if (updateRecordAct != null)
				{
					var newRecordActivityReport = new RecordActivityReport(updateRecordAct, Activities, Projects, Shifts, TypeShifts);

					if (editedRow != null)
					{
						RecordActivityReportList.Remove(editedRow);
						RecordActivityReportList.Add(newRecordActivityReport);

						setRecordActivityReportList();
						setRecordActivityReportListcollectionView();
						selectRow(newRecordActivityReport);
					}
				}
			}
		}

		private Guid? getD_ShiftGuidId(RecordActivityReport editedRow)
		{
			var result = (Guid?)(editedRow.ShiftIndex == -1 ? null : (Shifts[editedRow.ShiftIndex]).GuidId);
			if (result == null)
				return null;
			else
				return result == Guid.Empty ? null : result;
		}

		private int? getID_ActivityId(RecordActivityReport editedRow)
		{
			return (int?)(editedRow.TypeShiftIndex == -1 ? null : (TypeShifts[editedRow.TypeShiftIndex]).Id);
		}

		private int? getID_ProjectId(RecordActivityReport editedRow)
		{
			var result = (int?)(editedRow.ProjectIndex == -1 ? null : (Projects[editedRow.ProjectIndex]).Id);
			return result == 0 ? null : result;
		}
		private List<RecordActivityReport> getRecordActivityReportList()
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
						return repObj;

					}).ToList();

					return list;
				}
			}

			return new List<RecordActivityReport>();
		}
		private void onCmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			setRecordActivityReportList();
			setRecordActivityReportListcollectionView();
		}

		private void selectRow(RecordActivityReport rowItem)
		{
			var selectRow = RecordActivityReportList.FirstOrDefault(x => x.GuidId == rowItem.GuidId);
			if (selectRow != null)
			{
				dtgRecordActivities.ScrollIntoView(selectRow);
				dtgRecordActivities.SelectedItem = selectRow;
			}
		}

		private void setRecordActivityReportList()
		{
			var getRecordActiviList = getRecordActivityReportList();
			if (getRecordActiviList != null)
			{
				RecordActivityReportList = new ObservableCollection<RecordActivityReport>(getRecordActiviList.OrderBy(x => x.StartDateTime).Select(x => new RecordActivityReport(x, Activities, Projects, Shifts, TypeShifts)));
			}

			lblCount.Content = (RecordActivityReportList?.Count ?? 0).ToString();
		}

		private void setRecordActivityReportListcollectionView()
		{
			dtgRecordActivities.ItemsSource = RecordActivityReportList;
		}
	}
}