using Microsoft.EntityFrameworkCore;
using Moq;
using Ninject.Activation;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.DB.UnitTests
{
	public abstract class aRepositoryBaseTest
	{
		protected readonly Mock<IDbContextFactory<MsSqlDbContext>> _contextFactoryMock;
		protected readonly DbContextOptions<MsSqlDbContext> _dbOptions;

		protected readonly ProjectRepository<MsSqlDbContext> _projectRepository;
		protected readonly ActivityRepository<MsSqlDbContext> _activityRepository;
		public aRepositoryBaseTest()
		{
			_contextFactoryMock = new Mock<IDbContextFactory<MsSqlDbContext>>();
			_dbOptions = new DbContextOptionsBuilder<MsSqlDbContext>()
							.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unikátní DB pro každý test
							.Options;

			_projectRepository = new ProjectRepository<MsSqlDbContext>(() => new MsSqlDbContext(_dbOptions));
			_activityRepository = new ActivityRepository<MsSqlDbContext>(() => new MsSqlDbContext(_dbOptions));
		}

		protected async Task<MsSqlDbContext> createContextAsync()
		{
			var context = new MsSqlDbContext(_dbOptions);
			await context.Database.EnsureCreatedAsync();
			_contextFactoryMock.Setup(x => x.CreateDbContextAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(context);
			return context;
		}

		/// <summary>
		/// Před každým testem databázi explicitně odstraňte a znovu vytvořte
		/// </summary>
		/// <param name="context"></param>
		protected async Task resetDBAsync(MsSqlDbContext context)
		{
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
		}
		protected void compareAllProperties<T>(T expectedObj, T actualObj, HashSet<string>? ignoredProperties = null)
		{
			foreach (var prop in typeof(T).GetProperties())
			{
				if (ignoredProperties != null && ignoredProperties.Contains(prop.Name))
					continue;

				var expected = prop.GetValue(expectedObj);
				var actual = prop.GetValue(actualObj);
				Assert.Equal(expected, actual);
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
