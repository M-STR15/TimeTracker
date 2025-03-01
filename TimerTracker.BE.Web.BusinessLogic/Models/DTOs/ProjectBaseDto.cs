using System.ComponentModel.DataAnnotations;

namespace TimeTracker.BE.DB.Models
{
	public class ProjectBaseDto
	{
		public virtual string? Description { get; set; }
		public virtual int Id { get; set; }

		[Required]
		public virtual string Name { get; set; } = string.Empty;
	}
}