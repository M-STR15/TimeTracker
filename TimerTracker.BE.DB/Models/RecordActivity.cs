using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.BE.DB.Models
{
    [Table("Record_activities", Schema = "dbo")]
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

        public RecordActivity(DateTime startTime, int activityId, int? projectId = null, int? subModuleId = null, string description = "") : this(startTime, activityId, description)
        {
            ProjectId = projectId;
            SubModuleId = subModuleId;
        }

        public RecordActivity(DateTime startTime, int activityId, Guid shiftGuidId, int? projectId = null, int? subModuleId = null, string description = "") : this(startTime, activityId, projectId, subModuleId, description)
        {
            ShiftGuidId = shiftGuidId;
        }

        public RecordActivity(Guid guidId, DateTime startTime, int activityId, int? projectId, int? subModuleId, string description = "") : this(startTime, activityId, projectId, subModuleId, description)
        {
            GuidId = guidId;
        }

        public RecordActivity(Guid guidId, DateTime startTime, int activityId, int? projectId, int? subModuleId, Guid shiftGuidId, string description = "") : this(startTime, activityId, projectId, subModuleId, description)
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

        [Column("Shift_GuidID")]
        public Guid? ShiftGuidId { get; set; }

        [Required]
        [Column("Start_time")]
        public DateTime StartTime { get; set; }

        [ForeignKey("SubModuleId")]
        public SubModule? SubModule { get; set; }

        [Column("SubModule_ID")]
        public int? SubModuleId { get; set; }
    }
}