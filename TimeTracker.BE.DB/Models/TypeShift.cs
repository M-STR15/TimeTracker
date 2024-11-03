using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
{
    [Table("TypeShifts", Schema = "dbo")]
    [Comment("Tabulka všech možných směn.")]
    public class TypeShift
    {
        [Key]
        [Column("TypeShift_ID")]
        public int Id { get; set; }
        [Required]
        [Column("Name")]
        public string Name { get; set; } = "";
        public string Color { get; set; } = "";
        public bool IsVisibleInMainWindow { get; set; }

        public ICollection<TypeShift> TypeShifts { get; set; }

        public ICollection<RecordActivity> RecordActivity { get; set; }

        public TypeShift()
        { }

        public TypeShift(int id, string name, string color, bool isVisibleInMainWindow = true) : this()
        {
            Id = id;
            Name = name;
            Color = color;
            IsVisibleInMainWindow = isVisibleInMainWindow;
        }
    }
}
