using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimerTracker.BE.DB.Models
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

        [Required]
        [Column("TypeShift_ID")]
        public int TypeShiftId { get; set; }

        [ForeignKey("TypeShiftId")]
        public TypeShift TypeShift { get; set; }

        public Shift()
        {
        }

        public Shift(Guid guidId, DateTime startDate, int typeShiftId, string? description = null) : this()
        {
            GuidId = guidId;
            StartDate = startDate;
            Description = description;
            TypeShiftId = typeShiftId;
        }
    }
}