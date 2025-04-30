using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Models.Entities
{
	[Index(nameof(ProjectId), nameof(Name), IsUnique = true)]
	[Table("SubModule", Schema = "Project")]
	[Comment("Tabulka podmodulů.")]
	public class SubModule : IIdentifiable, ISubModule, ISubModuleBase
	{
		/// <inheritdoc />
		[Comment("Aktivity spojené s podmodulem.")]
		public virtual ICollection<RecordActivity>? Activities { get; set; }
		/// <inheritdoc />
		[Comment("Popis podmodulu.")]
		public virtual string? Description { get; set; }
		/// <inheritdoc />
		[Key]
		[Column("SubModule_ID")]
		[Comment("Primární klíč podmodulu.")]
		public virtual int Id { get; protected set; }
		/// <inheritdoc />
		[Required]
		[MaxLength(30, ErrorMessage = "Název je příliš dlouhý.")]
		[Comment("Název podmodulu.")]
		public virtual string Name { get; set; } = string.Empty;
		/// <inheritdoc />
		[ForeignKey("ProjectId")]
		[Comment("Projekt, ke kterému podmodul patří.")]
		public virtual Project? Project { get; set; }
		/// <inheritdoc />
		[Column("Project_ID")]
		[Comment("ID projektu, ke kterému podmodul patří.")]
		[Required]
		public virtual int ProjectId { get; set; }

		public SubModule()
		{}

		public SubModule(int id, string name, int projectId, string? description = null)
		{
			Id = id;
			Name = name;
			Description = description;
			ProjectId = projectId;
		}

		public SubModule(ISubModuleBase subModule) : this()
		{
			Id = subModule.Id;
			Name = subModule.Name;
			Description = subModule.Description;
			ProjectId = subModule.ProjectId;
		}
	}
}