using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
{
	[Index(nameof(StartDate), IsUnique = true)]
	[Table("Shifts", Schema = "dbo")]
	[Comment("Tabulka slouží k naplánování směny.")]
	public class Shift : Stamp, IIdentifiableGuid
	{
		public string? Description { get; set; }

		[Key]
		[Column("Guid_ID")]
		public Guid GuidId { get; set; }

		public ICollection<Shift>? Shifts { get; set; }

		[Required]
		[Column("Start_date")]
		public DateTime StartDate { get; set; }

		[NotMapped]
		public string StartDateLongStr { get => (GuidId == Guid.Empty ? "" : StartDate.Date.ToString("dd.MM.yyyy")); }

		[ForeignKey("TypeShiftId")]
		public TypeShift? TypeShift { get; set; }

		[Required]
		[Column("TypeShift_ID")]
		public int TypeShiftId { get; set; }

		public Shift() : base()
		{
		}

		public Shift(Guid guidId, DateTime startDate, int typeShiftId, string? description = null) : this()
		{
			GuidId = guidId;
			StartDate = startDate;
			Description = description;
			TypeShiftId = typeShiftId;
		}

		public override string ToString()
		{
			return StartDate.ToString();
		}
	}
}