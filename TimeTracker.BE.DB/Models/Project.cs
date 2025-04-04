using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Models
{
	[Index(nameof(Name), IsUnique = true)]
	[Table("Project", Schema = "dbo")]
	[Comment("Tabulka projektů.")]
	public class Project : IIdentifiable, IProject, IProjectBase
	{
		/// <inheritdoc />
		[Comment("Aktivity spojené s projektem.")]
		public ICollection<RecordActivity>? Activities { get; set; }
		/// <inheritdoc />
		[Comment("Popis projektu.")]
		public virtual string? Description { get; set; }

		/// <inheritdoc />
		[Key]
		[Column("Project_ID")]
		[Comment("Primární klíč projektu.")]
		public virtual int Id { get; set; }
		/// <inheritdoc />
		[Comment("Název projektu.")]
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		[MaxLength(30, ErrorMessage = "Překročená maximální délka textu.")]
		[MinLength(5, ErrorMessage = "Nebylo dosaženo minimální déky textu.")]
		public virtual string Name { get; set; } = string.Empty;
		/// <inheritdoc />
		[Comment("Podmoduly spojené s projektem.")]
		public ICollection<SubModule>? SubModules { get; set; }

		public Project()
		{ }

		public Project(IProjectBase project) : this()
		{
			Id = project.Id;
			Name = project.Name;
			Description = project.Description;
		}

		public Project(int id, string name, string description = "") : this()
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}