using Microsoft.EntityFrameworkCore;
using Moq;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.Tests.DB.UnitTests
{
	public abstract class aRepositoryBaseTest<TContext> where TContext : MainDatacontext
	{
		protected readonly Mock<IDbContextFactory<TContext>> _contextFactoryMock;
		protected readonly DbContextOptions<TContext> _dbOptions;

		protected readonly ProjectRepository<TContext> _projectRepository;
		protected readonly ActivityRepository<TContext> _activityRepository;

		public aRepositoryBaseTest()
		{
			_contextFactoryMock = new Mock<IDbContextFactory<TContext>>();
			_dbOptions = new DbContextOptionsBuilder<TContext>()
							.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for each test
							.Options;

			_projectRepository = new ProjectRepository<TContext>(() => (TContext)Activator.CreateInstance(typeof(TContext), _dbOptions)!);
			_activityRepository = new ActivityRepository<TContext>(() => (TContext)Activator.CreateInstance(typeof(TContext), _dbOptions)!);
		}

		protected async Task<TContext> createContextAsync()
		{
			var context = (TContext)Activator.CreateInstance(typeof(TContext), _dbOptions)!;
			await context.Database.EnsureCreatedAsync();
			_contextFactoryMock.Setup(x => x.CreateDbContextAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(context);
			return context;
		}

		/// <summary>
		/// Explicitly delete and recreate the database before each test.
		/// </summary>
		/// <param name="context"></param>
		protected async Task resetDBAsync(TContext context)
		{
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
		}

		protected void compareAllProperties<T>(T expectedObj, T actualObj, HashSet<string>? ignoredProperties = null)
		{
			foreach (var prop in typeof(T).GetProperties())
			{
				try
				{
					if (ignoredProperties != null && ignoredProperties.Contains(prop.Name))
						continue;

					var expected = prop.GetValue(expectedObj);
					var actual = prop.GetValue(actualObj);
					Assert.Equal(expected, actual);
				}
				catch (Exception ex)
				{
					Assert.True(false, $"Test failed due to exception: {ex.Message}" + "; Property name:" + prop.Name);
				}
			}
		}

		protected async Task<IProjectBase?> seedProjectAsync(string name)
		{
			var project = new Project { Name = name, Description = "TestDescr" };
			var result = await _projectRepository.SaveAsync(project);
			return result;
		}

		protected async Task<Activity?> seedActivityAsync(string name)
		{
			await using var context = await createContextAsync();
			var activity = new Activity { Name = name };
			context.Activities.Add(activity);
			return activity;
		}
	}
}
