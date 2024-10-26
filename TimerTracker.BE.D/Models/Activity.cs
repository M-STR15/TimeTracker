using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.BE.DB.Models
{
    [Table("Activities", Schema = "dbo")]
    [Index(nameof(Name))]
    public class Activity
    {
        [Key]
        [Column("Activity_ID")]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RecordActivity> Activities { get; set; }

        public Activity()
        {
            Name = "";
        }

        public Activity(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
