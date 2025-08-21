using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.Basic.Enums;
using Activity = TimeTracker.BE.DB.Models.Entities.Activity;
using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.PC.ViewModels
{
	[ObservableObject]
	public partial class RecordActivityReport : IRecordActivity
	{
		/// <summary>
		/// Popis aktivity záznamu. Může být prázdný nebo null.
		/// </summary>
		[ObservableProperty]
		public string? _description;

		private static ICollection<Activity>? _activities;
		private static ICollection<Project>? _projects;

		private static ICollection<Shift>? _shifts;

		private static ICollection<TypeShift>? _typeShifts;

		/// <summary>
		/// Aktivita spojená se záznamem. Může být null, pokud není aktivita přiřazena.
		/// </summary>
		[ObservableProperty]
		private Activity? _activity;

		/// <summary>
		/// Identifikátor aktivity. Určuje typ vykonávané činnosti.
		/// </summary>
		[ObservableProperty]
		private int _activityId;

		/// <summary>
		/// Datum ukončení aktivity ve formátu řetězce (např. "dd.MM.yyyy").
		/// </summary>
		[ObservableProperty]
		private string? _endDate;

		/// <summary>
		/// Datum a čas ukončení aktivity. Může být null, pokud není ukončení zadáno.
		/// </summary>
		[ObservableProperty]
		private DateTime? _endDateTime;

		/// <summary>
		/// Čas ukončení aktivity ve formátu řetězce (např. "HH:mm:ss").
		/// </summary>
		[ObservableProperty]
		private string? _endTime;

		/// <summary>
		/// Jedinečný identifikátor záznamu aktivity (GUID).
		/// </summary>
		[ObservableProperty]
		private Guid _guidId;

		/// <summary>
		/// Projekt, ke kterému je aktivita přiřazena. Může být null.
		/// </summary>
		[ObservableProperty]
		private Project? _project;

		/// <summary>
		/// Identifikátor projektu. Může být null, pokud není projekt přiřazen.
		/// </summary>
		[ObservableProperty]
		private int? _projectId;

		/// <summary>
		/// Index projektu v kolekci projektů. Slouží pro výběr v uživatelském rozhraní.
		/// </summary>
		[ObservableProperty]
		private int _projectIndex;

		/// <summary>
		/// Směna přiřazená k záznamu. Může být null, pokud není směna přiřazena.
		/// </summary>
		[ObservableProperty]
		private Shift? _shift;

		/// <summary>
		/// Jedinečný identifikátor směny (GUID). Může být null.
		/// </summary>
		[ObservableProperty]
		private Guid? _shiftGuidId;

		/// <summary>
		/// Index směny v kolekci směn. Slouží pro výběr v uživatelském rozhraní.
		/// </summary>
		[ObservableProperty]
		private int _shiftIndex;

		/// <summary>
		/// Datum zahájení aktivity ve formátu řetězce (např. "dd.MM.yyyy").
		/// </summary>
		[ObservableProperty]
		private string? _startDate;

		/// <summary>
		/// Datum a čas zahájení aktivity.
		/// </summary>
		[ObservableProperty]
		private DateTime _startDateTime;

		/// <summary>
		/// Čas zahájení aktivity ve formátu řetězce (např. "HH:mm:ss").
		/// </summary>
		[ObservableProperty]
		private string? _startTime;

		/// <summary>
		/// Submodul přiřazený k aktivitě. Může být null.
		/// </summary>
		[ObservableProperty]
		private SubModule? _subModule;

		/// <summary>
		/// Identifikátor submodulu. Může být null, pokud není submodul přiřazen.
		/// </summary>
		[ObservableProperty]
		private int? _subModuleId;

		/// <summary>
		/// Index submodulu v kolekci submodulů. Slouží pro výběr v uživatelském rozhraní.
		/// </summary>
		[ObservableProperty]
		private int _subModuleIndex;

		private ICollection<SubModule>? _subModules;

		/// <summary>
		/// Typ směny přiřazený k záznamu. Může být null, pokud není typ směny přiřazen.
		/// </summary>
		[ObservableProperty]
		private TypeShift? _typeShift;

		/// <summary>
		/// Identifikátor typu směny. Může být null, pokud není typ směny přiřazen.
		/// </summary>
		[ObservableProperty]
		private int? _typeShiftId;

		/// <summary>
		/// Index typu směny v kolekci typů směn. Slouží pro výběr v uživatelském rozhraní.
		/// </summary>
		[ObservableProperty]
		private int _typeShiftIndex;

		/// <summary>
		/// Index aktivity v kolekci aktivit. Slouží pro výběr v uživatelském rozhraní.
		/// </summary>
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