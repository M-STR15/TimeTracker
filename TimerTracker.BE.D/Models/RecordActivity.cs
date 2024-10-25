using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimerTracker.BE.DB.Models;

namespace TimerTracker.BE.DB.Models
{
    [Table("Record_activity", Schema = "dbo")]
    public class RecordActivity
    {
        public RecordActivity()
        {
            GuidId = Guid.NewGuid();
        }

        public RecordActivity(DateTime startTime, int activityId, string description = "") : this()
        {
            StartTime = startTime;
            ActivityId = activityId;
            Description = description;
        }

        public RecordActivity(DateTime startTime, int activityId, int? projectId = null, string description = "") : this()
        {
            StartTime = startTime;
            ActivityId = activityId;
            ProjectId = projectId;
            Description = description;
        }

        public RecordActivity(DateTime startTime, int activityId, Guid shiftGuidId, int? projectId = null, string description = "") : this()
        {
            StartTime = startTime;
            ActivityId = activityId;
            ProjectId = projectId;
            Description = description;
            ShiftGuidId = shiftGuidId;
        }

        public RecordActivity(Guid guidId, DateTime startTime, int activityId, int projectId, string description = "") : this(startTime, activityId, projectId, description)
        {
            GuidId = guidId;
        }

        public RecordActivity(Guid guidId, DateTime startTime, int activityId, int projectId, Guid shiftGuidId, string description = "") : this(startTime, activityId, projectId, description)
        {
            GuidId = guidId;
            ShiftGuidId = shiftGuidId;
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
        public Project? Project { get; set; }

        [Column("Project_ID")]
        public int? ProjectId { get; set; }

        [ForeignKey("ShiftGuidId")]
        public Shift? Shift { get; set; }

        [Column("Shift_GuidId")]
        public Guid? ShiftGuidId { get; set; }

        [Required]
        [Column("Start_time")]
        public DateTime StartTime { get; set; }

        [ForeignKey("SubModuleID")]
        public SubModule? SubModule { get; set; }

        [Column("SubModule_ID")]
        public int? SubModuleID { get; set; }
    }
}
