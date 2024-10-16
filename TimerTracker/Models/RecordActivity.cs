﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.Models
{
	[Table("Record_activity", Schema = "dbo")]
	public class RecordActivity
	{
		[Key]
		[Column("Guid_ID")]
		public Guid GuidId { get; set; }
		[Column("Start_time")]
		public DateTime StartTime { get; set; }
		[Column("Activity_ID")]
		public int ActivityId { get; set; }
		[Column("Project_ID")]
		public int ProjectId { get; set; }

		[ForeignKey("ActivityId")]
		public Activity Activity { get; set; }
		[ForeignKey("ProjectId")]
		public Project Project { get; set; }
		public string Description { get; set; }

		public RecordActivity()
		{
			GuidId = Guid.NewGuid();
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
	}
}
