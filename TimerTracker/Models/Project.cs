﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.Models
{
	[Table("Project", Schema = "dbo")]
	public class Project
	{
		[Key]
		[Column("Project_ID")]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public ICollection<RecordActivity> Activities { get; set; }
	}
}
