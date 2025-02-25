using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
{
	[Table("TypeShifts", Schema = "dbo")]
	[Comment("Tabulka všech možných směn.")]
	public class TypeShift : IIdentifiable
	{
		[Column("Color")]
		[Comment("Barva směny.")]
		public string Color { get; set; } = "";

		[Key]
		[Column("TypeShift_ID")]
		[Comment("Primární klíč typu směny.")]
		public int Id { get; set; }

		[Comment("Viditelnost směny v hlavním okně.")]
		public bool IsVisibleInMainWindow { get; set; }

		[Required]
		[Column("Name")]
		[Comment("Název typu směny.")]
		public string Name { get; set; } = "";

		[Comment("Kolekce aktivit záznamů.")]
		public ICollection<RecordActivity>? RecordActivity { get; set; }

		[Comment("Kolekce typů směn.")]
		public ICollection<TypeShift>? TypeShifts { get; set; }

		public TypeShift()
		{ }

		public TypeShift(int id, string name, string color, bool isVisibleInMainWindow = true) : this()
		{
			Id = id;
			Name = name;
			Color = color;
			IsVisibleInMainWindow = isVisibleInMainWindow;
		}

		public override string ToString()
		{
			return Name.ToString();
		}
	}
}