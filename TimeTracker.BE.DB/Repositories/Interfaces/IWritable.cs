using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	/// <summary>
	/// Rozhraní pro zápis (ukládání) entit do databáze.
	/// </summary>
	/// <typeparam name="T">Typ entity, která se ukládá.</typeparam>
	interface IWritable<T>
	{
		/// <summary>
		/// Asynchronně uloží položku do databáze.
		/// Pokud položka již existuje, provede aktualizaci; jinak vytvoří nový záznam.
		/// </summary>
		/// <param name="item">Položka k uložení.</param>
		/// <returns>Uložená položka nebo <c>null</c>, pokud se uložení nezdařilo.</returns>
		Task<T?> SaveAsync(T item);
	}
}
