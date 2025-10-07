using System.ComponentModel;

namespace TimeTracker.Basic.Enums
{
	/// <summary>
	/// Typ směny
	/// </summary>
	public enum eTypeShift
	{
		/// <summary>
		/// V kanceláři
		/// </summary>
		[Description("V kanceláři")]
		Office = 1,
		/// <summary>
		/// Práce z domova
		/// </summary>
		[Description("Práce z domova")]
		HomeOffice = 2,
		/// <summary>
		/// Práce na nespecifickém místě
		/// </summary>
		[Description("Práce na nespecifickém místě")]
		Others = 3,
		/// <summary>
		/// Dovolená
		/// </summary>
		[Description("Dovolená")]
		Holiday = 4
	}
}