using Microsoft.EntityFrameworkCore;
using Moq;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.Tests.DB.UnitTests
{
	public abstract class aRepositoryBaseTest
	{
		protected readonly Mock<IDbContextFactory<InMemoryDbContext>> _contextFactoryMock;
		protected readonly DbContextOptions<InMemoryDbContext> _dbOptions;

		protected readonly ProjectRepository<InMemoryDbContext> _projectRepository;
		protected readonly ActivityRepository<InMemoryDbContext> _activityRepository;
		public aRepositoryBaseTest()
		{
			_contextFactoryMock = new Mock<IDbContextFactory<InMemoryDbContext>>();
			_dbOptions = new DbContextOptionsBuilder<InMemoryDbContext>()
							.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unikátní DB pro každý test
							.Options;

			_projectRepository = new ProjectRepository<InMemoryDbContext>(() => new InMemoryDbContext(_dbOptions));
			_activityRepository = new ActivityRepository<InMemoryDbContext>(() => new InMemoryDbContext(_dbOptions));
		}

		protected async Task<InMemoryDbContext> createContextAsync()
		{
			var context = new InMemoryDbContext(_dbOptions);
			await context.Database.EnsureCreatedAsync();
			_contextFactoryMock.Setup(x => x.CreateDbContextAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(context);
			return context;
		}

		/// <summary>
		/// Před každým testem databázi explicitně odstraňte a znovu vytvořte
		/// </summary>
		/// <param name="context"></param>
		protected async Task resetDBAsync(InMemoryDbContext context)
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
					Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}" + "; Název property:" + prop.Name);
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
