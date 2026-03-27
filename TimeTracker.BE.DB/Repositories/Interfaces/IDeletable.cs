namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	/// <summary>
	/// Rozhraní pro asynchronní mazání entity daného typu.
	/// </summary>
	/// <typeparam name="T">Typ entity, která má být smazána.</typeparam>
	interface IDeletable<T>
	{
		/// <summary>
		/// Asynchronně smaže zadanou entitu.
		/// </summary>
		/// <param name="item">Entita, která má být smazána.</param>
		/// <returns>Vrací <c>true</c>, pokud bylo smazání úspěšné; jinak <c>false</c>.</returns>
		Task<bool> DeleteAsync(T item);
	}
}
