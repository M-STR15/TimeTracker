using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.BE.DB.Models
{
	public abstract class Stamp
	{
		private DateTime _stampDateTime;

		[Required]
		[Column("Stamp_DateTime")]
		public virtual DateTime StampDateTime
		{
			get => _stampDateTime;
			set
			{
				if (_stampDateTime != value)
					_stampDateTime = value;
			}
		}

		public Stamp()
		{
			StampDateTime = DateTime.Now;
		}
	}
}