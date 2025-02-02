﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("Project", Schema = "dbo")]
    public class Project : IProjectWithoutColl, IIdentifiable
	{
        [Key]
        [Column("Project_ID")]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual string? Description { get; set; }

        public ICollection<RecordActivity> Activities { get; set; }
        public ICollection<SubModule> SubModules { get; set; }

        public Project()
        {
            Name = "";
        }

        public Project(IProjectWithoutColl project) : this()
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