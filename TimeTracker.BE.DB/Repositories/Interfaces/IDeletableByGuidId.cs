
namespace TimeTracker.BE.DB.Repositories.Interfaces
{
	interface IDeletableByGuidId
	{
		Task<bool> DeleteAsync(Guid guidId);
	}
}
