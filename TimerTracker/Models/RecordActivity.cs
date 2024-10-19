using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.Models
{
	[Table("Record_activity", Schema = "dbo")]
	public class RecordActivity
	{
		public RecordActivity()
		{
			GuidId = Guid.NewGuid();
			Activity = new();
			Project = new();
		}

		public RecordActivity(DateTime startTime, int activityId, int projectId, string description = "") : this()
		{
			StartTime = startTime;
			ActivityId = activityId;
			ProjectId = projectId;
			Description = description;
		}

		public RecordActivity(Guid guidId, DateTime startTime, int activityId, int projectId, string description = "") : this(startTime, activityId, projectId, description)
		{
			GuidId = guidId;
		}

		[ForeignKey("ActivityId")]
		public Activity Activity { get; set; }

		[Required]
		[Column("Activity_ID")]
		public int ActivityId { get; set; }

		public string? Description { get; set; }

		[Key]
		[Column("Guid_ID")]
		public Guid GuidId { get; set; }
		[ForeignKey("ProjectId")]
		public Project Project { get; set; }

		[Required]
		[Column("Project_ID")]
		public int ProjectId { get; set; }

		[ForeignKey("ShiftGuidId")]
		public Shift? Shift { get; set; }

		[Column("Shift_GuidId")]
		public Guid? ShiftGuidId { get; set; }

		[Required]
		[Column("Start_time")]
		public DateTime StartTime { get; set; }
	}
}
