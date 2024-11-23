using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.Windows.Models
{
	[ObservableObject]
	public partial class RecordActivityReport : IRecordActivity
	{
		private static ICollection<Activity>? _activities;
		private static ICollection<Project>? _projects;
		private static ICollection<Shift>? _shifts;
		private static ICollection<SubModule>? _subModules;
		private static ICollection<TypeShift>? _typeShifts;
		public RecordActivityReport(RecordActivity recordActivity, ICollection<Activity>? activities = null, ICollection<Project>? projects = null, ICollection<Shift>? shifts = null, ICollection<TypeShift>? typeShifts = null, ICollection<SubModule>? subModules = null)
		{
			GuidId = recordActivity.GuidId;
			Activity = recordActivity.Activity;
			Project = recordActivity.Project;
			SubModule = recordActivity.SubModule;
			Shift = recordActivity.Shift;
			TypeShift = recordActivity.TypeShift;

			ActivityId = recordActivity.ActivityId;
			ProjectId = recordActivity.ProjectId;
			SubModuleId = recordActivity.SubModuleId;
			ShiftGuidId = recordActivity.ShiftGuidId;
			Description = recordActivity?.Description;
			StartDateTime = recordActivity.StartDateTime;
			EndDateTime = recordActivity.EndDateTime;
			TypeShiftId = TypeShift?.Id ?? null;

			StartDate = StartDateTime.ToString("dd.MM.yyyy");
			StartTime = StartDateTime.ToString("HH:mm:ss");
			EndDate = EndDateTime?.ToString("dd.MM.yyyy");
			EndTime = EndDateTime?.ToString("HH:mm:ss");
			TotalTime = TimeSpan.FromSeconds(DurationSec);
			_activities = activities;
			_projects = projects;
			_shifts = shifts;
			_subModules = subModules;
			_typeShifts = typeShifts;
		}
		public string? StartDate { get; set; }
		public string? StartTime { get; set; }
		public string? EndDate { get; set; }

		private int _activityIndex { get; set; }
		public int ActivityIndex
		{
			get => _activityIndex;
			set
			{
				if (_activityIndex != value)
				{
					_activityIndex = value;
					OnPropertyChanged();

					if (_activities != null && value != -1)
					{
						Activity = _activities.ElementAtOrDefault(value);
						ActivityId = Activity.Id;
						OnPropertyChanged(nameof(Activity));
					}
				}
			}
		}
		private int _projectIndex;
		public int ProjectIndex
		{
			get => _projectIndex;
			set
			{
				if (_projectIndex != value)
				{
					_projectIndex = value;
					OnPropertyChanged();

					if (_projects != null && value != -1)
					{
						Project = _projects.ElementAtOrDefault(value);
						ProjectId = Project.Id;
						OnPropertyChanged(nameof(Project));

						_subModules = null;
						SubModuleId = null;
						SubModuleIndex = -1;
					}
				}
			}
		}
		private int _shiftIndex;
		public int ShiftIndex
		{
			get => _shiftIndex;
			set
			{
				if (_shiftIndex != value)
				{
					_shiftIndex = value;
					OnPropertyChanged();

					if (_shifts != null && value != -1)
					{
						Shift = _shifts.ElementAtOrDefault(value);
						ShiftGuidId = Shift.GuidId;
						OnPropertyChanged(nameof(Shift));
					}
				}
			}
		}


		private int _subModuleIndex;
		public int SubModuleIndex
		{
			get => _subModuleIndex;
			set
			{
				if (_subModuleIndex != value)
				{
					_subModuleIndex = value;
					OnPropertyChanged();

					if (_subModules != null && value != -1)
					{
						SubModule = _subModules.ElementAtOrDefault(value);
						SubModuleId = SubModule.Id;
						OnPropertyChanged(nameof(SubModule));
					}
				}
			}
		}
		private int _typeShiftIndex;
		public int TypeShiftIndex
		{
			get => _typeShiftIndex;
			set
			{
				if (_typeShiftIndex != value)
				{
					_typeShiftIndex = value;
					OnPropertyChanged();

					if (_typeShifts != null && value != -1)
					{
						TypeShift = _typeShifts.ElementAtOrDefault(value);
						TypeShiftId = TypeShift.Id;
						OnPropertyChanged(nameof(TypeShift));
					}
				}
			}
		}
		public string? EndTime { get; set; }
		public TimeSpan? TotalTime { get; set; }

		[ObservableProperty]
		private Activity _activity;
		[ObservableProperty]
		private int _activityId;
		[ObservableProperty]
		public string? _description;

		public double DurationSec => EndDateTime != null ? ((DateTime)EndDateTime - StartDateTime).TotalSeconds : 0;
		[ObservableProperty]
		private DateTime? _endDateTime;
		[ObservableProperty]
		private Guid _guidId;
		[ObservableProperty]
		private Project? _project;
		[ObservableProperty]
		private int? _projectId;
		[ObservableProperty]
		private Shift? _shift;
		[ObservableProperty]
		private Guid? _shiftGuidId;
		[ObservableProperty]
		private DateTime _startDateTime;
		[ObservableProperty]
		private SubModule? _subModule;
		[ObservableProperty]
		private int? _subModuleId;
		[ObservableProperty]
		private TypeShift _typeShift;
		[ObservableProperty]
		private int? _typeShiftId;
	}
}
