using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Models
{
	public abstract class Stamp : IStamp
	{
		private DateTime _stampDateTime;
		/// <inheritdoc />
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