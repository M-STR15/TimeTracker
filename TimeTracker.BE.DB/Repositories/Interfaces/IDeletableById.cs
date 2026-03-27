namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	/// <summary>
	/// Rozhraní pro mazání entity podle jejího identifikátoru.
	/// </summary>
	interface IDeletableById
	{
		/// <summary>
		/// Asynchronně smaže entitu podle zadaného identifikátoru.
		/// </summary>
		/// <param name="id">Identifikátor entity, která má být smazána.</param>
		/// <returns>Vrací <c>true</c>, pokud byla entita úspěšně smazána; jinak <c>false</c>.</returns>
		Task<bool> DeleteAsync(int id);
	}
}
