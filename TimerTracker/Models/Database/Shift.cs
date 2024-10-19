using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.Models.Database
{
    [Index(nameof(StartDate), IsUnique = true)]
    [Table("Shifts", Schema = "dbo")]
    [Comment("Tabulka slouží k naplánování směny.")]
    public class Shift
    {
        [Key]
        [Column("Guid_ID")]
        public Guid GuidId { get; set; }
        [Required]
        [Column("Start_date")]
        public DateTime StartDate { get; set; }

        public string? Description { get; set; }

        public ICollection<Shift> Shifts { get; set; }

        public Shift()
        {

        }

        public Shift(Guid guidId, DateTime startDate, string? description = null)
        {
            GuidId = guidId;
            StartDate = startDate;
            Description = description;
        }
    }
}
