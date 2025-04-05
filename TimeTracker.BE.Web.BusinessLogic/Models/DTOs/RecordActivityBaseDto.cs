using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class RecordActivityBaseDto : RecordActivityInsertDto, IRecordActivityBase
	{
		/// <inheritdoc />
		public DateTime? EndDateTime { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována.")]
		public Guid GuidId { get; set; }

	}
}
