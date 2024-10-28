using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.BE.DB.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("Activities", Schema = "dbo")]
    public class Activity
    {
        [Key]
        [Column("Activity_ID")]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Název je příliš dlouhý.")]
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
