﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.BE.D.Models
{
	[Table("Project", Schema = "dbo")]
	public class Project : IProject
	{
		[Key]
		[Column("Project_ID")]
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string? Description { get; set; }

		public ICollection<RecordActivity> Activities { get; set; }
		public ICollection<SubModule> SubModules { get; set; }
		public Project()
		{
			Name = "";
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
