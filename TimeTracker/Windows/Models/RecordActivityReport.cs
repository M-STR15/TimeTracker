using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.Windows.Models
{
	[ObservableObject]
	public partial class RecordActivityReport : IRecordActivity
	{
		[ObservableProperty]
		public string? _description;

		private static ICollection<Activity>? _activities;
		[ObservableProperty]
		private Activity _activity;

		[ObservableProperty]
		private int _activityId;

		[ObservableProperty]
		private string? _endDate;

		[ObservableProperty]
		private DateTime? _endDateTime;

		[ObservableProperty]
		private string? _endTime;

		[ObservableProperty]
		private Guid _guidId;

		[ObservableProperty]
		private Project? _project;

		[ObservableProperty]
		private int? _projectId;

		private int _projectIndex;

		private static ICollection<Project>? _projects;
		[ObservableProperty]
		private Shift? _shift;

		[ObservableProperty]
		private Guid? _shiftGuidId;

		private int _shiftIndex;


		private static ICollection<Shift>? _shifts;
		[ObservableProperty]
		private string? _startDate;

		[ObservableProperty]
		private DateTime _startDateTime;

		[ObservableProperty]
		private string? _startTime;

		[ObservableProperty]
		private SubModule? _subModule;

		[ObservableProperty]
		private int? _subModuleId;

		private int _subModuleIndex;

		[ObservableProperty]
		private TypeShift? _typeShift;

		[ObservableProperty]
		private int? _typeShiftId;

		private int _typeShiftIndex;
		private static ICollection<TypeShift>? _typeShifts;

		private ICollection<SubModule> _subModules;

		public static void SetIndexCollection(ICollection<Activity>? activities = null, ICollection<Project>? projects = null, ICollection<Shift>? shifts = null, ICollection<TypeShift>? typeShifts = null)
		{
			_activities = activities;
			_shifts = shifts;
			_typeShifts = typeShifts;
			_projects = projects;
		}
		public RecordActivityReport(IRecordActivity recordActivityReport)
		{
			if (recordActivityReport != null)
			{
				GuidId = recordActivityReport.GuidId;
				Activity = recordActivityReport.Activity;
				Project = recordActivityReport.Project;
				SubModule = recordActivityReport.SubModule;
				Shift = recordActivityReport.Shift;
				TypeShift = recordActivityReport.TypeShift;

				ActivityId = recordActivityReport.ActivityId;
				ProjectId = recordActivityReport.ProjectId;
				SubModuleId = recordActivityReport.SubModuleId;
				ShiftGuidId = recordActivityReport.ShiftGuidId;
				Description = recordActivityReport?.Description;
				StartDateTime = recordActivityReport?.StartDateTime ?? DateTime.Now;
				if (ActivityId != (int)eActivity.Stop)
					EndDateTime = recordActivityReport?.EndDateTime ?? null;

				TypeShiftId = TypeShift?.Id ?? null;

				StartDate = StartDateTime.ToString("dd.MM.yyyy");
				StartTime = StartDateTime.ToString("HH:mm:ss");
				EndDate = EndDateTime?.ToString("dd.MM.yyyy");
				EndTime = EndDateTime?.ToString("HH:mm:ss");

				//OnPropertyChanged(nameof(TotalTime));
				if (_activities != null)
					ActivityIndex = getIndex(_activities, ActivityId);
				if (_projects != null)
				{
					ProjectIndex = getIndex(_projects, ProjectId);
					createSumModuleList();
				}
				if (_shifts != null)
					ShiftIndex = getIndex(_shifts, ShiftGuidId);
				if (_typeShifts != null)
					TypeShiftIndex = getIndex(_typeShifts, TypeShiftId);
				if (_subModules != null)
					SubModuleIndex = getIndex(_subModules, SubModuleId);
			}
		}

		private void createSumModuleList()
		{
			if (ProjectId != null)
			{
				_subModules = new List<SubModule>();
				_subModules.Add(new SubModule());
				foreach (var item in Project.SubModules)
				{
					_subModules.Add(item);
				}
			}
		}

		//public RecordActivityReport(RecordActivity recordActivity)
		//{
		//	if (recordActivity != null)
		//	{
		//		GuidId = recordActivity.GuidId;
		//		Activity = recordActivity.Activity;
		//		Project = recordActivity.Project;
		//		SubModule = recordActivity.SubModule;
		//		Shift = recordActivity.Shift;
		//		TypeShift = recordActivity.TypeShift;

		//		ActivityId = recordActivity.ActivityId;
		//		ProjectId = recordActivity.ProjectId;
		//		SubModuleId = recordActivity.SubModuleId;
		//		ShiftGuidId = recordActivity.ShiftGuidId;
		//		Description = recordActivity?.Description;
		//		StartDateTime = recordActivity?.StartDateTime ?? DateTime.Now;
		//		if (ActivityId != (int)eActivity.Stop)
		//			EndDateTime = recordActivity?.EndDateTime ?? null;

		//		TypeShiftId = TypeShift?.Id ?? null;

		//		StartDate = StartDateTime.ToString("dd.MM.yyyy");
		//		StartTime = StartDateTime.ToString("HH:mm:ss");
		//		EndDate = EndDateTime?.ToString("dd.MM.yyyy");
		//		EndTime = EndDateTime?.ToString("HH:mm:ss");

		//		if (_activities != null)
		//			ActivityIndex = getIndex(_activities, ActivityId);
		//		if (_projects != null)
		//		{ 
		//			ProjectIndex = getIndex(_projects, ProjectId);
		//			createSumModuleList();
		//		}
		//		if (_shifts != null)
		//			ShiftIndex = getIndex(_shifts, ShiftGuidId);
		//		if (_typeShifts != null)
		//			TypeShiftIndex = getIndex(_typeShifts, TypeShiftId);
		//		if (_subModules != null)
		//			SubModuleIndex = getIndex(_subModules, SubModuleId);
		//		//OnPropertyChanged(nameof(TotalTime));
		//	}
		//}



		public int ActivityIndex
		{
			get => _activityIndex;
			set
			{
				if (_activityIndex != value)
				{
					_activityIndex = value;
					OnPropertyChanged();

					//if (_activities != null && value != -1)
					//{
					//	Activity = _activities.ElementAtOrDefault(value);
					//	ActivityId = Activity.Id;
					//	OnPropertyChanged(nameof(Activity));
					//	OnPropertyChanged(nameof(ActivityId));
					//}
				}
			}
		}

		public double DurationSec => EndDateTime != null ? ((DateTime)EndDateTime - (DateTime)StartDateTime).TotalSeconds : 0;

		public int ProjectIndex
		{
			get => _projectIndex;
			set
			{
				if (_projectIndex != value)
				{
					_projectIndex = value;
					OnPropertyChanged();

					//if (_projects != null && value != -1)
					//{
					//	Project = _projects.ElementAtOrDefault(value);
					//	ProjectId = Project.Id;
					//	OnPropertyChanged(nameof(Project));
					//	OnPropertyChanged(nameof(ProjectId));

					//	_subModules = null;
					//	SubModuleId = null;
					//	SubModuleIndex = -1;
					//}
				}
			}
		}

		public int ShiftIndex
		{
			get => _shiftIndex;
			set
			{
				if (_shiftIndex != value)
				{
					_shiftIndex = value;
					OnPropertyChanged();

					//if (_shifts != null && value != -1)
					//{
					//	Shift = _shifts.ElementAtOrDefault(value);
					//	ShiftGuidId = Shift.GuidId;
					//	OnPropertyChanged(nameof(Shift));
					//	OnPropertyChanged(nameof(ShiftGuidId));
					//}
				}
			}
		}

		public int SubModuleIndex
		{
			get => _subModuleIndex;
			set
			{
				if (_subModuleIndex != value)
				{
					_subModuleIndex = value;
					OnPropertyChanged();

					//if (_subModules != null && value != -1)
					//{
					//	SubModule = _subModules.ElementAtOrDefault(value);
					//	SubModuleId = SubModule.Id;
					//	OnPropertyChanged(nameof(SubModule));
					//	OnPropertyChanged(nameof(SubModuleId));
					//}
				}
			}
		}

		public TimeSpan? TotalTime
		{
			get
			{
				var duration = 0.00;
				if (ActivityId != (int)eActivity.Stop)
				{
					duration = EndDateTime != null ? ((DateTime)EndDateTime - (DateTime)StartDateTime).TotalSeconds : 0;
					return TimeSpan.FromSeconds(duration);
				}
				else
				{
					return null;
				}
			}
		}
		public int TypeShiftIndex
		{
			get => _typeShiftIndex;
			set
			{
				if (_typeShiftIndex != value)
				{
					_typeShiftIndex = value;
					OnPropertyChanged();

					//if (_typeShifts != null && value != -1)
					//{
					//	TypeShift = _typeShifts.ElementAtOrDefault(value);
					//	TypeShiftId = TypeShift.Id;
					//	OnPropertyChanged(nameof(TypeShift));
					//	OnPropertyChanged(nameof(TypeShiftId));
					//}
				}
			}
		}

		private int _activityIndex { get; set; }

		private int getIndex<T>(IEnumerable<T> collection, int? objectId)
						where T : IIdentifiable
		{
			var newColection = collection.Select((row, index) => new { row, index }).ToList();
			var result = newColection.FirstOrDefault(x => x.row.Id == objectId)?.index ?? -1;
			return result;
		}

		private int getIndex<T>(IEnumerable<T> collection, Guid? objectId)
			where T : IIdentifiableGuid
		{
			var newColection = collection.Select((row, index) => new { row, index }).ToList();
			var result = newColection.FirstOrDefault(x => x.row.GuidId == objectId)?.index ?? -1;
			return result;
		}
	}
}
