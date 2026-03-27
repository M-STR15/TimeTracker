namespace TimeTracker.Basic.Interfaces
{
	/// <summary>
	/// Rozhraní reprezentující položku s barvou.
	/// Rozšiřuje <see cref="ITItem"/> o vlastnost barvy.
	/// </summary>
	public interface ITItemWithColor : ITItem
	{
		/// <summary>
		/// Získá barvu položky (např. hexadecimální kód barvy).
		/// </summary>
		string Color { get; }
	}
}
