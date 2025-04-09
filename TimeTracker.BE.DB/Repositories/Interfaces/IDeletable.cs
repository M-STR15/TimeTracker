namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	interface IDeletable<T>
	{
		Task<bool> DeleteAsync(T item);
	}
}
