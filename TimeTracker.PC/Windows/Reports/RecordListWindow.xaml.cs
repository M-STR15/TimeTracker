using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.Enums;
using TimeTracker.PC.Services;
using TimeTracker.PC.Stories;
using TimeTracker.PC.ViewModels;
using TimeTracker.PC.Windows.Reports.Services;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.PC.Windows.Reports
{
	public partial class RecordListWindow : Window
	{
		private readonly EventLogService _eventLogService;
		private readonly MainStory _mainStoru;
		private readonly Func<SqliteDbContext> _context;
		public RecordListWindow(MainStory mainStore, Func<SqliteDbContext> context)
		{
			_eventLogService = new EventLogService();
			_mainStoru = mainStore;
			_context = context;
			inicialization();
		}

		private async void inicialization()
		{
			try
			{
				InitializeComponent();
				_recordProvider = _mainStoru.DIContainerStore.GetRecordRepository();
				_activityProvider = _mainStoru.DIContainerStore.GetActivityRepository();

				Activities = (await _activityProvider.GetAllAsync()).ToList();
				new ObservableCollection<string>(Enum.GetNames<eActivity>());

				var projectRepository = new ProjectRepository<SqliteDbContext>(_context);
				var projects = await projectRepository.GetAllAsync();
				Projects = convertCollection<Project>(projects).ToList();

				var shiftRepository = new ShiftRepository<SqliteDbContext>(_context);
				var shifts = await shiftRepository.GetAllAsync();
				Shifts = convertCollection<Shift>(shifts).ToList();

				var typeShiftRepository = new TypeShiftRepository<SqliteDbContext>(_context);
				var typeShifts = await typeShiftRepository.GetAllAsync();
				TypeShifts = convertCollection<TypeShift>(typeShifts, false).ToList();

				cmbMonth.ItemsSource = new ReportParameterService().Monts;
				cmbMonth.SelectedIndex = 0;

				RecordActivityReport.SetIndexCollection(Activities, Projects, Shifts, TypeShifts);

				setRecordActivityReportList();
				setRecordActivityReportListcollectionView();

				DataContext = this;
			}
			catch (Exception)
			{
				_eventLogService.WriteError(Guid.Parse("1acd3061-db78-4f29-befa-ea1f72d2a036"), null, "Problém se spouštěním record list window.");
			}
		}

		public List<Activity> Activities { get; set; }
		public List<Project> Projects { get; set; }
		public List<SubModule>? SubModules { get; set; }
		public ObservableCollection<RecordActivityReport> RecordActivityReportList { get; set; }
		public List<Shift> Shifts { get; set; }
		public List<TypeShift> TypeShifts { get; set; }
		private ActivityRepository<SqliteDbContext> _activityProvider { get; set; }
		private RecordRepository<SqliteDbContext> _recordProvider { get; set; }

		private async void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (sender is Button btn)
				{
					var guidDeleteRow = Guid.Parse(btn.Tag?.ToString());
					var result = await _recordProvider.DeleteAsync(guidDeleteRow);

					var findRow = RecordActivityReportList.FirstOrDefault(x => x.GuidId == guidDeleteRow);
					if (findRow != null)
						RecordActivityReportList.Remove(findRow);
				}
			}
			catch (Exception)
			{
				_eventLogService.WriteError(Guid.Parse("7a322e9b-26cb-4491-a98f-af54887459a7"), null, "Problém při mazáná záznamu.");
			}
		}

		private ICollection<T> convertCollection<T>(IEnumerable<T> collection, bool emptyValue = true)
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

		private async void dtgRecordActivities_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			var editedRow = e.Row.Item as RecordActivityReport;
			if (editedRow != null)
			{
				var startDateTime = (DateTime?)dateTimegroupDateTime(editedRow.StartDate, editedRow.StartTime);
				var endDateTime = dateTimegroupDateTime(editedRow.EndDate, editedRow.EndTime);

				//var column = e.Column; // Sloupec, který se edituje

				var projectId = getID_ProjectId(editedRow);
				var subModuleId = getID_SubModuleId(editedRow);
				var shiftGuidId = getD_ShiftGuidId(editedRow);
				var typeShiftId = getID_ActivityId(editedRow);
				var activityId = (Activities[editedRow.ActivityIndex]).Id;

				if (startDateTime != null)
				{
					var startDateTimeConv = (DateTime)startDateTime;
					var recordActivity = new RecordActivity(editedRow.GuidId, startDateTimeConv, activityId, typeShiftId, projectId, subModuleId, shiftGuidId, endDateTime, editedRow?.Description);

					var updateRecordAct = await _recordProvider.SaveAsync(recordActivity);
					if (updateRecordAct != null)
					{
						var newRecordActivityReport = new RecordActivityReport(updateRecordAct);

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
		}

		private Guid? getD_ShiftGuidId(RecordActivityReport editedRow)
		{
			var result = (Guid?)(editedRow.ShiftIndex == -1 ? null : (Shifts[editedRow.ShiftIndex]).GuidId);
			if (result == null)
				return null;
			else
				return result == Guid.Empty ? editedRow.ShiftGuidId : result;
		}

		private int? getID_ActivityId(RecordActivityReport editedRow)
		{
			return (int?)(editedRow.TypeShiftIndex == -1 ? editedRow.ActivityId : (TypeShifts[editedRow.TypeShiftIndex]).Id);
		}

		private int? getID_ProjectId(RecordActivityReport editedRow)
		{
			var result = (int?)(editedRow.ProjectIndex == -1 ? null : (Projects[editedRow.ProjectIndex]).Id);
			return result == 0 ? editedRow.ProjectId : result;
		}

		private int? getID_SubModuleId(RecordActivityReport editedRow)
		{
			if (SubModules != null && editedRow.SubModuleIndex != null && editedRow.SubModuleIndex != -1)
			{
				var result = (int?)(editedRow.SubModuleIndex == -1 ? null : (SubModules[editedRow.SubModuleIndex]).Id);
				return result == 0 ? editedRow.SubModuleId : result;
			}
			else
			{
				return editedRow.SubModuleId;
			}
		}

		private async Task<List<RecordActivityReport>> getRecordActivityReportListAsync()
		{
			if (cmbMonth.Text != "")
			{
				var startTime = Convert.ToDateTime("1." + cmbMonth.SelectedItem);
				var endTime = startTime.AddMonths(1);
				var origList = await _recordProvider.GetAsync(startTime, endTime);
				if (origList != null)
				{
					var list = origList.Select((record, index) =>
					{
						return new RecordActivityReport(record);
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

		private async void setRecordActivityReportList()
		{
			var getRecordActiviList = await getRecordActivityReportListAsync();
			if (getRecordActiviList != null)
			{
				RecordActivityReportList = new ObservableCollection<RecordActivityReport>(getRecordActiviList.OrderBy(x => x.StartDateTime).Select(x => new RecordActivityReport(x)));
			}

			lblCount.Content = (RecordActivityReportList?.Count ?? 0).ToString();
		}

		private void setRecordActivityReportListcollectionView() => dtgRecordActivities.ItemsSource = RecordActivityReportList;

		private void dtgRecordActivities_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			var column = e.Column; // Sloupec, který se edituje

			if (column.Header.ToString() == "Sub module")
			{
				var row = e.Row;       // Řádek, který se edituje
				var item = (RecordActivityReport)row.Item;
				if (item.ProjectId != null)
				{
					var subModules = new List<SubModule>();
					subModules.Add(new SubModule());
					var project = Projects.FirstOrDefault(x => x.Id == item.ProjectId);
					if (project != null && project.SubModules != null)
					{
						foreach (var itemList in project.SubModules.ToList())
						{
							subModules.Add(itemList);
						}
						SubModules = subModules;
					}
				}
			}
		}

		private async void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			var datefilter = Convert.ToDateTime("1." + cmbMonth.SelectedItem);
			var countDayInMont = DateTime.DaysInMonth(datefilter.Year, datefilter.Month);
			var date = new DateTime(datefilter.Year, datefilter.Month, countDayInMont, 23, 59, 59);
			var newRecord = new RecordActivity(date, (int)eActivity.Start);
			await _recordProvider.SaveAsync(newRecord);

			setRecordActivityReportList();
			setRecordActivityReportListcollectionView();
		}
	}
}