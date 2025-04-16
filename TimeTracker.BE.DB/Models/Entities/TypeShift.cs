using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Models.Entities
{
	[Table("TypeShifts", Schema = "dbo")]
	[Comment("Tabulka všech možných směn.")]
	public class TypeShift : IIdentifiable, ITypeShift, ITypeShiftBase
	{
		/// <inheritdoc />
		[Column("Color")]
		[Comment("Barva směny.")]
		public string Color { get; set; } = string.Empty;
		/// <inheritdoc />
		[Key]
		[Column("TypeShift_ID")]
		[Comment("Primární klíč typu směny.")]
		public int Id { get; protected set; }
		/// <inheritdoc />
		[Comment("Viditelnost směny v hlavním okně.")]
		public bool IsVisibleInMainWindow { get; set; }
		/// <inheritdoc />
		[Required]
		[Column("Name")]
		[Comment("Název typu směny.")]
		[StringLength(30)]
		public string Name { get; set; } = string.Empty;
		/// <inheritdoc />
		[Comment("Kolekce aktivit záznamů.")]
		public ICollection<RecordActivity>? RecordActivity { get; set; }
		/// <inheritdoc />
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