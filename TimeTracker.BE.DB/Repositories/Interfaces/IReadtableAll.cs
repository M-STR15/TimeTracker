namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	interface IReadtableAll<T>
	{
		Task<IEnumerable<T>> GetAllAsync();

	}
}
