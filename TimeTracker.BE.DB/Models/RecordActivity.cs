using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
{
	[Table("Record_activities", Schema = "dbo")]
	public class RecordActivity : IRecordActivity
	{
		private string _endDate;

		private DateTime? _endDateTime;

		private string _endTime;

		private string _startDate;

		private DateTime _startDateTime;

		private string _startTime;

		public RecordActivity()
		{
			GuidId = Guid.Empty;
		}

		public RecordActivity(DateTime startTime, int activityId, string description = "") : this()
		{
			StartDateTime = startTime;
			ActivityId = activityId;
			Description = description;
		}

		public RecordActivity(DateTime startDateTime, DateTime? endDateTime, int activityId, int typeShiftId, int? projectId = null, int? subModuleId = null, string description = "") : this(startDateTime, activityId, description)
		{
			ProjectId = projectId;
			SubModuleId = subModuleId;
			TypeShiftId = typeShiftId;
			EndDateTime = endDateTime;
		}

		public RecordActivity(DateTime startTime, int activityId, int typeShiftId, int? projectId = null, int? subModuleId = null, string description = "") : this(startTime, activityId, description)
		{
			ProjectId = projectId;
			SubModuleId = subModuleId;
			TypeShiftId = typeShiftId;
		}

		public RecordActivity(DateTime startTime, int activityId, Guid shiftGuidId, int typeShiftId, int? projectId = null, int? subModuleId = null, string description = "") : this(startTime, activityId, typeShiftId, projectId, subModuleId, description)
		{
			ShiftGuidId = shiftGuidId;
		}

		public RecordActivity(Guid guidId, DateTime startTime, int activityId, int? projectId, int? subModuleId, int typeShiftId, string description = "") : this(startTime, activityId, typeShiftId, projectId, subModuleId, description)
		{
			GuidId = guidId;
		}

		public RecordActivity(Guid guidId, DateTime startTime, int activityId, int typeShiftId, int? projectId, int? subModuleId, Guid shiftGuidId, string description = "") : this(startTime, activityId, typeShiftId, projectId, subModuleId, description)
		{
			GuidId = guidId;
			ShiftGuidId = shiftGuidId;
		}

		public RecordActivity(Guid guidId, DateTime startDateTime, DateTime? endDateTime, Activity activity, TypeShift typeShift, Shift? shift = null, Project? project = null, SubModule? subModule = null, string description = "") : this(startDateTime, endDateTime, activity, typeShift, shift, project, subModule, description)
		{
			GuidId = guidId;
		}

		public RecordActivity(DateTime startDateTime, DateTime? endDateTime, Activity activity, TypeShift typeShift, Shift? shift = null, Project? project = null, SubModule? subModule = null, string description = "") : this()
		{
			Activity = activity;
			Project = project;
			SubModule = subModule;
			Shift = shift;
			TypeShift = typeShift;

			ActivityId = activity.Id;
			ProjectId = project?.Id ?? null;
			SubModuleId = subModule?.Id ?? null;
			ShiftGuidId = shift?.GuidId ?? null;
			Description = description;
			StartDateTime = startDateTime;
			EndDateTime = endDateTime;
			TypeShiftId = typeShift?.Id ?? null;
		}

		[ForeignKey("ActivityId")]
		public virtual Activity Activity { get; set; }

		[Required]
		[Column("Activity_ID")]
		public virtual int ActivityId { get; set; }

		public virtual string? Description { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public virtual double DurationSec => EndDateTime != null ? ((DateTime)EndDateTime - StartDateTime).TotalSeconds : 0;

		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		//public virtual string? EndDate
		//{
		//	get => EndDateTime?.ToString("dd.MM.yyyy");
		//}

		[Column("End_DateTime")]
		public virtual DateTime? EndDateTime
		{
			get => _endDateTime;
			set
			{
				if (_endDateTime != value)
					_endDateTime = value;
			}
		}

		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		//public virtual string? EndTime
		//{
		//	get => EndDateTime?.ToString("HH:mm:ss");
		//}

		[Key]
		[Column("Guid_ID")]
		public virtual Guid GuidId { get; set; }

		[ForeignKey("ProjectId")]
		public virtual Project? Project { get; set; }

		[Column("Project_ID")]
		public virtual int? ProjectId { get; set; }

		[ForeignKey("ShiftGuidId")]
		public virtual Shift? Shift { get; set; }

		[Column("Shift_GuidID")]
		public virtual Guid? ShiftGuidId { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public virtual string StartDate
		{
			get => StartDateTime.ToString("dd.MM.yyyy");
		}

		[Required]
		[Column("Start_DateTime")]
		public virtual DateTime StartDateTime
		{
			get => _startDateTime;
			set
			{
				if (_startDateTime != value)
					_startDateTime = value;
			}
		}
		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		//public virtual string StartTime
		//{
		//	get => StartDateTime.ToString("HH:mm:ss");
		//}
		[ForeignKey("SubModuleId")]
		public virtual SubModule? SubModule { get; set; }

		[Column("SubModule_ID")]
		public virtual int? SubModuleId { get; set; }

		[ForeignKey("TypeShiftId")]
		public virtual TypeShift TypeShift { get; set; }

		[Column("TypeShift_ID")]
		public virtual int? TypeShiftId { get; set; }
	}
}