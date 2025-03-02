using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Models
{
	[Index(nameof(Name), IsUnique = true)]
	[Table("Activities", Schema = "dbo")]
	[Comment("Primární klíč aktivity.")]
	public class Activity : IIdentifiable, IActivity, IActivityBase
	{
		/// <inheritdoc />
		public ICollection<RecordActivity>? Activities { get; set; }

		/// <inheritdoc />
		[Key]
		[Column("Activity_ID")]
		[Comment("Primární klíč aktivity.")]
		public int Id { get; set; }
		/// <inheritdoc />
		[Required]
		[MaxLength(30, ErrorMessage = "Název je příliš dlouhý.")]
		[Comment("Název aktivity.")]
		public string Name { get; set; } = string.Empty;

		public Activity()
		{ }

		public Activity(int id, string name)
		{
			Name = name;
			Id = id;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}