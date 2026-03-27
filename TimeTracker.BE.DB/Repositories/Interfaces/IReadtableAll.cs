namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	/// <summary>
	/// Rozhraní pro čtení všech záznamů daného typu z databáze.
	/// </summary>
	/// <typeparam name="T">Typ entity, která se načítá.</typeparam>
	interface IReadtableAll<T>
	{
		/// <summary>
		/// Asynchronně získá všechny záznamy daného typu.
		/// </summary>
		/// <returns>Kolekce všech záznamů typu <typeparamref name="T"/>.</returns>
		Task<IEnumerable<T>> GetAllAsync();

	}
}
