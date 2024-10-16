using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.Models
{
	[Table("Activities", Schema = "dbo")]
	public class Activity
	{
		[Key]
		[Column("Activity_ID")]
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<RecordActivity> Activities { get; set; }

		public Activity()
		{ }

		public Activity(int id, string name) : this()
		{
			Id = id;
			Name = name;
		}
	}
}
