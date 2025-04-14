using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro projekt.
	/// </summary>
	public interface IProject : IProjectBase
	{
		/// <summary>
		/// Kolekce aktivit spojených s projektem.
		/// </summary>
		ICollection<RecordActivity>? Activities { get; set; }

		/// <summary>
		/// Kolekce podmodulů spojených s projektem.
		/// </summary>
		ICollection<SubModule>? SubModules { get; set; }
	}
}