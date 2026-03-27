namespace TimeTracker.Basic.Interfaces
{
	/// <summary>
	/// Rozhraní reprezentující základní položku sledování času.
	/// </summary>
	public interface ITItem
	{
		/// <summary>
		/// Získá jedinečný identifikátor položky.
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// Získá název položky.
		/// </summary>
		public string Name { get; }
	}
}
