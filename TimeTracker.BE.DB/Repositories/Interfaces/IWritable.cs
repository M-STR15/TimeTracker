using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	interface IWritable<T>
	{
		Task<T?> SaveAsync(T item);
	}
}
