using System.ComponentModel.DataAnnotations;
using TimerTracker.BE.Web.BusinessLogic.Helpers;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	[CopyAttributesFrom(typeof(TypeShift))]
	public class TypeShiftBaseDto : ITypeShiftBase
	{
		/// <inheritdoc />
		public string Color { get; set; } = string.Empty;
		/// <inheritdoc />
		[Key]
		public int Id { get; set; }
		/// <inheritdoc />
		public bool IsVisibleInMainWindow { get; set; }
		/// <inheritdoc />
		public string Name { get; set; } = string.Empty;
	}
}
