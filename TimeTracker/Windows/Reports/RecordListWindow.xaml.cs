using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
		public List<Activity> Activities { get; set; }
		public List<Project> Projects { get; set; }
		public List<Shift> Shifts { get; set; }
		public List<TypeShift> TypeShifts { get; set; }
		private RecordProvider _recordProvider { get; set; }
		private ActivityProvider _activityProvider { get; set; }
		public List<RecordActivityReport> RecordActivityReportList { get; set; }

		public ICollectionView RecordActivityReportListcollectionView { get; set; }

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
			Projects = convertCollection<Project>(projects).ToList();

			var shiftProvider = new ShiftProvider();
			var shifts = shiftProvider.GetShifts();
			Shifts = convertCollection<Shift>(shifts).ToList();

			var typeShifts = shiftProvider.GetTypeShifts();
			TypeShifts = convertCollection<TypeShift>(typeShifts, false).ToList();

			setRecordActivityReportList();
			RecordActivityReportListcollectionView = CollectionViewSource.GetDefaultView(RecordActivityReportList);
			dtgRecordActivities.ItemsSource = RecordActivityReportListcollectionView;

			DataContext = this;
		}

		private void setRecordActivityReportList()
		{
			RecordActivityReportList = getRecordActivityReportList();
			lblCount.Content = (RecordActivityReportList?.Count ?? 0).ToString();
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
			setRecordActivityReportList();
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
						repObj.ActivityIndex = getIndex(Activities, repObj.ActivityId);
						repObj.ProjectIndex = getIndex(Projects, repObj.ProjectId);
						repObj.ShiftIndex = getIndex(Shifts, repObj.ShiftGuidId);
						repObj.TypeShiftIndex = getIndex(TypeShifts, repObj.TypeShiftId);
						return repObj;
						;
					}).ToList();

					return list;
				}
			}

			return new List<RecordActivityReport>();
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
				var startDateTime = Convert.ToDateTime(convertDataTrimeStringStart + " " + editedRow.StartTime);
				var convertDataTrimeStringEnd = Convert.ToDateTime(editedRow.EndDate).ToString("dd.MM.yyyy");
				var endDateTime = Convert.ToDateTime(convertDataTrimeStringEnd + " " + editedRow.EndTime);

				var activityId = (Activities[editedRow.ActivityIndex]).Id;
				var typeShiftId = (int?)(editedRow.TypeShiftIndex == -1 ? null : (TypeShifts[editedRow.TypeShiftIndex]).Id);
				var shiftGuidId = (Guid?)(editedRow.ShiftIndex == -1 ? null : (Shifts[editedRow.ShiftIndex]).GuidId);
				var projectId = (int?)(editedRow.ProjectIndex == -1 ? null : (Projects[editedRow.ProjectIndex]).Id);
				var subModuleId = 0;


				var recordActivity = new RecordActivity(editedRow.GuidId, startDateTime, activityId, typeShiftId, projectId, null, shiftGuidId, editedRow.EndDateTime, editedRow?.Description);

				var updateRecordAct = _recordProvider.SaveRecord(recordActivity);
				var newRecordActivityReport = new RecordActivityReport(updateRecordAct)
				{
					ActivityIndex = getIndex(Activities, editedRow?.Activity?.Id),
					TypeShiftIndex = getIndex(TypeShifts, editedRow?.TypeShift?.Id),
					ShiftIndex = getIndex(Shifts, editedRow?.Shift?.GuidId),
					ProjectIndex = getIndex(Projects, editedRow?.Project?.Id),
					SubModuleIndex = 0
				};


				var index = RecordActivityReportList.FindIndex(x => x.GuidId == editedRow.GuidId);
				if (index >= 0)
					RecordActivityReportList[index] = newRecordActivityReport;

				//RecordActivityReportListcollectionView.Refresh();
				//RecordActivityReportList.Where(x => x.GuidId == editedRow.GuidId).Select(x => x = newRecordActivityReport);

				// Dokončení úprav aktuálního řádku
				dtgRecordActivities.Dispatcher.BeginInvoke(new Action(() =>
				{
					dtgRecordActivities.CommitEdit(DataGridEditingUnit.Row, true);
					RecordActivityReportListcollectionView.Refresh();
				}), System.Windows.Threading.DispatcherPriority.Background);

				//// Odložení volání Refresh po dokončení úprav
				//Dispatcher.BeginInvoke(new Action(() =>
				//{
				//	if (RecordActivityReportListcollectionView != null)
				//	{
				//		try
				//		{
				//			RecordActivityReportListcollectionView.Refresh();
				//		}
				//		catch (Exception ex)
				//		{
				//			MessageBox.Show($"Chyba při obnově dat: {ex.Message}");
				//		}
				//	}
				//}), System.Windows.Threading.DispatcherPriority.Background);
			}
		}

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
							RecordActivityReportListcollectionView.Refresh();
						}
					}
				}
			}
			catch (Exception)
			{

			}
		}
	}
}