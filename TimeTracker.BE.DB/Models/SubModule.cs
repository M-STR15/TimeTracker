﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
{
	[Index(nameof(ProjectId), nameof(Name), IsUnique = true)]
	[Table("SubModule", Schema = "dbo")]
	public class SubModule : ISubModuleWithoutColl, IIdentifiable
	{
		[Key]
		[Column("SubModule_ID")]
		public virtual int Id { get; set; }

		[Required]
		[MaxLength(30, ErrorMessage = "Název je příliš dlouhý.")]
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

		public SubModule(int id, string name, int projectId, string? description = null)
		{
			Id = id;
			Name = name;
			Description = description;
			ProjectId = projectId;
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