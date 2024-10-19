using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.Models.Database
{
	[Table("SubModule", Schema = "dbo")]
	public class SubModule
	{
		[Key]
		[Column("SubModule_ID")]
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }

		[Column("Project_ID")]
		public int ProjectId { get; set; }

		[ForeignKey("ProjectId")]
		public Project Project { get; set; }
		public ICollection<RecordActivity> Activities { get; set; }

		public SubModule()
		{
			Name = "";
		}
	}
}
