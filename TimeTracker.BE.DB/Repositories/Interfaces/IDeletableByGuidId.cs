namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	/// <summary>
	/// Rozhraní pro mazání entit podle jejich GUID identifikátoru.
	/// </summary>
	interface IDeletableByGuidId
	{
		/// <summary>
		/// Asynchronně smaže entitu podle zadaného GUID identifikátoru.
		/// </summary>
		/// <param name="guidId">GUID identifikátor entity ke smazání.</param>
		/// <returns>Vrací <c>true</c>, pokud byla entita úspěšně smazána; jinak <c>false</c>.</returns>
		Task<bool> DeleteAsync(Guid guidId);
	}
}
