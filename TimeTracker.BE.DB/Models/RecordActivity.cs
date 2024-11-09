using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
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

        public RecordActivity(Guid guidId, DateTime startTime, Activity activity, TypeShift typeShift, Project? project = null, SubModule? subModule = null, Shift? shift = null, string description = "") : this(startTime, activity, typeShift, project, subModule, shift, description)
        {
            GuidId = guidId;
        }

        public RecordActivity(DateTime startTime, Activity activity, TypeShift typeShift, Project? project = null, SubModule? subModule = null, Shift? shift = null, string description = "") : this()
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
            StartTime = startTime;
            TypeShiftId = typeShift.Id;
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

        [Column("End_time")]
        public DateTime? EndTime { get; set; }

        [ForeignKey("SubModuleId")]
        public SubModule? SubModule { get; set; }

        [Column("SubModule_ID")]
        public int? SubModuleId { get; set; }

        [ForeignKey("TypeShiftId")]
        public TypeShift TypeShift { get; set; }

        [Column("TypeShift_ID")]
        public int TypeShiftId { get; set; }

        public double DurationSec { get => EndTime != null ? ((DateTime)EndTime - StartTime).TotalSeconds : 0; }
    }
}