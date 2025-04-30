using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Models.Entities
{
	[Table("Record_activities", Schema = "Record")]
	public class RecordActivity : aStamp, IRecordActivity, IRecordActivityBase
	{
		private DateTime? _endDateTime;
		private DateTime _startDateTime;
		/// <inheritdoc />
		[ForeignKey("ActivityId")]
		public virtual Activity? Activity { get; set; }
		/// <inheritdoc />
		[Required]
		[Column("Activity_ID")]
		[Comment("Primární klíč aktivity.")]
		public virtual int ActivityId { get; set; }
		/// <inheritdoc />
		[Comment("Popis aktivity.")]
		public virtual string? Description { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public virtual double DurationSec => EndDateTime != null ? ((DateTime)EndDateTime - StartDateTime).TotalSeconds : 0;
		/// <inheritdoc />
		[Column("End_DateTime")]
		[Comment("Datum a čas ukončení aktivity.")]
		public virtual DateTime? EndDateTime
		{
			get => _endDateTime;
			set
			{
				if (_endDateTime != value)
					_endDateTime = value;
			}
		}
		/// <inheritdoc />
		[Key]
		[Column("Guid_ID")]
		public virtual Guid GuidId { get; protected set; }
		/// <inheritdoc />
		[ForeignKey("ProjectId")]
		public virtual Project? Project { get; set; }
		/// <inheritdoc />
		[Column("Project_ID")]
		public virtual int? ProjectId { get; set; }
		/// <inheritdoc />
		[ForeignKey("ShiftGuidId")]
		public virtual Shift? Shift { get; set; }
		/// <inheritdoc />
		[Column("Shift_GuidID")]
		public virtual Guid? ShiftGuidId { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public virtual string StartDate
		{
			get => StartDateTime.ToString("dd.MM.yyyy");
		}
		/// <inheritdoc />
		[Required]
		[Column("Start_DateTime")]
		[Comment("Datum a čas zahájení aktivity.")]
		public virtual DateTime StartDateTime
		{
			get => _startDateTime;
			set
			{
				if (_startDateTime != value)
					_startDateTime = value;
			}
		}
		/// <inheritdoc />
		[ForeignKey("SubModuleId")]
		public virtual SubModule? SubModule { get; set; }
		/// <inheritdoc />
		[Column("SubModule_ID")]
		public virtual int? SubModuleId { get; set; }
		/// <inheritdoc />
		[ForeignKey("TypeShiftId")]
		public virtual TypeShift? TypeShift { get; set; }
		/// <inheritdoc />
		[Column("TypeShift_ID")]
		public virtual int? TypeShiftId { get; set; }

		public RecordActivity() : base()
		{
			GuidId = Guid.Empty;
		}

		public RecordActivity(DateTime startTime, int activityId, string? description = "") : this()
		{
			StartDateTime = modFormatDatetime(startTime);
			ActivityId = activityId;
			Description = description;
		}

		public RecordActivity(DateTime startDateTime, DateTime? endDateTime, int activityId, int? typeShiftId, int? projectId = null, int? subModuleId = null, string? description = "") : this(startDateTime, activityId, description)
		{
			ProjectId = projectId;
			SubModuleId = subModuleId;
			TypeShiftId = typeShiftId;
			EndDateTime = endDateTime;
		}

		public RecordActivity(DateTime startTime, int activityId, int? typeShiftId, int? projectId = null, int? subModuleId = null, string? description = "") : this(startTime, activityId, description)
		{
			ProjectId = projectId;
			SubModuleId = subModuleId;
			TypeShiftId = typeShiftId;
		}

		public RecordActivity(DateTime startTime, int activityId, Guid shiftGuidId, int? typeShiftId, int? projectId = null, int? subModuleId = null, string? description = "") : this(startTime, activityId, typeShiftId, projectId, subModuleId, description)
		{
			ShiftGuidId = shiftGuidId;
		}

		public RecordActivity(Guid guidId, DateTime startTime, int activityId, int? projectId, int? subModuleId, int typeShiftId, string? description = "") : this(startTime, activityId, typeShiftId, projectId, subModuleId, description)
		{
			GuidId = guidId;
		}

		public RecordActivity(Guid guidId, DateTime startTime, int activityId, int? typeShiftId, int? projectId, int? subModuleId, Guid? shiftGuidId, DateTime? endDateTime = null, string? description = "") : this(startTime, endDateTime, activityId, typeShiftId, projectId, subModuleId, description)
		{
			GuidId = guidId;
			ShiftGuidId = shiftGuidId;
		}

		public RecordActivity(Guid guidId, DateTime startDateTime, DateTime? endDateTime, Activity activity, TypeShift typeShift, Shift? shift = null, Project? project = null, SubModule? subModule = null, string? description = "") : this(startDateTime, activity, typeShift, shift, project, subModule, description, endDateTime)
		{
			GuidId = guidId;
		}

		public RecordActivity(DateTime startDateTime, Activity activity, TypeShift? typeShift = null, Shift? shift = null, Project? project = null, SubModule? subModule = null, string? description = "", DateTime? endDateTime = null) : this()
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
			StartDateTime = modFormatDatetime(startDateTime);
			EndDateTime = endDateTime;
			TypeShiftId = typeShift?.Id ?? null;
		}

		private DateTime modFormatDatetime(DateTime st)
		{
			return new DateTime(st.Year, st.Month, st.Day, st.Hour, st.Minute, st.Second);
		}
	}
}