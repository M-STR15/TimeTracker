using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.BE.DB.Models
{
    [Index(nameof(ProjectId),nameof(Name), IsUnique = true)]
    [Table("SubModule", Schema = "dbo")]
	public class SubModule : ISubModuleWithoutColl
	{
		[Key]
		[Column("SubModule_ID")]
		public virtual int Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
		public virtual string? Description { get; set; }

		[Column("Project_ID")]
		public virtual int ProjectId { get; set; }

		[ForeignKey("ProjectId")]
		public virtual Project Project { get; set; }
		public virtual ICollection<RecordActivity> Activities { get; set; }

		public SubModule()
		{
			Name = "";
		}

		public SubModule(ISubModuleWithoutColl subModule) : this()
		{
			Id = subModule.Id;
			Name = subModule.Name;
			Description = subModule.Description;
			ProjectId = subModule.ProjectId;
		}
	}
}
