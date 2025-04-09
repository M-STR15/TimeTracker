namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	interface IDeletableById
	{
		Task<bool> DeleteAsync(int id);
	}
}
