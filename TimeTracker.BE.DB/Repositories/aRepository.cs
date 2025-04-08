using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.BE.DB.Repositories
{
	public abstract class aRepository<T> where T : MainDatacontext
	{
		protected readonly Func<T> _contextFactory;

		public aRepository(Func<T> contextFactory)
		{
			_contextFactory = contextFactory;
		}
	}
}
