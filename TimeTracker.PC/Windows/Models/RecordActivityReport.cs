using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using Activity = TimeTracker.BE.DB.Models.Activity;

namespace TimeTracker.PC.Windows.Models
{
	[ObservableObject]
	public partial class RecordActivityReport : IRecordActivity
	{
		[ObservableProperty]
		public string? _description;

		private static ICollection<Activity>? _activities;
		private static ICollection<Project>? _projects;

		private static ICollection<Shift>? _shifts;

		private static ICollection<TypeShift>? _typeShifts;

		[ObservableProperty]
		private Activity? _activity;

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

		[ObservableProperty]

		private int _projectIndex;
		[ObservableProperty]
		private Shift? _shift;

		[ObservableProperty]
		private Guid? _shiftGuidId;

		[ObservableProperty]
		private int _shiftIndex;
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

		[ObservableProperty]
		private int _subModuleIndex;

		private ICollection<SubModule> _subModules;

		[ObservableProperty]
		private TypeShift? _typeShift;

		[ObservableProperty]
		private int? _typeShiftId;

		[ObservableProperty]
		private int _typeShiftIndex;
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

				OnPropertyChanged(nameof(TotalTime));
			}
		}

		public int ActivityIndex
		{
			get => _activityIndex;
			set
			{
				if (_activityIndex != value)
				{
					_activityIndex = value;
					OnPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Vypočítá dobu trvání v sekundách mezi počátečním a koncovým časem.
		/// Pokud není koncový čas nastaven, vrátí 0.
		/// </summary>
		public double DurationSec => EndDateTime != null ? ((DateTime)EndDateTime - (DateTime)StartDateTime).TotalSeconds : 0;

		/// <summary>
		/// Vypočítá celkový čas trvání aktivity.
		/// Pokud je aktivita zastavena, vrátí null.
		/// </summary>
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

		private int _activityIndex { get; set; }

		/// <summary>
		/// Nastaví kolekce indexů pro aktivity, projekty, směny a typy směn.
		/// </summary>
		public static void SetIndexCollection(ICollection<Activity>? activities = null, ICollection<Project>? projects = null, ICollection<Shift>? shifts = null, ICollection<TypeShift>? typeShifts = null)
		{
			_activities = activities;
			_shifts = shifts;
			_typeShifts = typeShifts;
			_projects = projects;
		}
		private void createSumModuleList()
		{
			if (ProjectId != null)
			{
				_subModules = new List<SubModule>();
				_subModules.Add(new SubModule());
				if (Project?.SubModules != null)
				{
					foreach (var item in Project.SubModules)
					{
						_subModules.Add(item);
					}
				}
			}
		}
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
