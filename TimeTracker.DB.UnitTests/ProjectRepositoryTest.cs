using Microsoft.EntityFrameworkCore;
using Moq;
using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.DB.UnitTests.Models;

namespace TimeTracker.DB.UnitTests
{
	public class ProjectRepositoryTest
	{
		private readonly Mock<IDbContextFactory<MsSqlDbContext>> _contextFactoryMock;
		private readonly DbContextOptions<MsSqlDbContext> _dbOptions;
		private readonly ProjectRepository<MsSqlDbContext> _projectRepository;
		public ProjectRepositoryTest()
		{
			_contextFactoryMock = new Mock<IDbContextFactory<MsSqlDbContext>>();
			_dbOptions = new DbContextOptionsBuilder<MsSqlDbContext>()
							.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unik�tn� DB pro ka�d� test
							.Options;
			_projectRepository = new ProjectRepository<MsSqlDbContext>(() => new MsSqlDbContext(_dbOptions));
		}

		/// <summary>
		/// Otestov�n� methody pro vkl�d�n� produktu do DB.
		/// </summary>
		[Fact]
		public async Task AddProjectTestAsync()
		{
			await using var context = await createContextAsync();
			await resetDBAsync(context);

			var project = new Project { Name = "Test Product", Description = "Test Description" };

			var result = await _projectRepository.SaveAsync(project);
			checkSavedData(project, result);

			var resultDb = await _projectRepository.GetAsync(result.Id);

			Assert.NotNull(result);
			Assert.NotNull(resultDb);

			//otesov�n� zda se vr�cen� objekt co vystupuje z methody se shoduje s t�m co je v DB
			var ignoredProperties2 = new HashSet<string> { nameof(Project.SubModules), nameof(Project.Activities) };
			compareAllProperties<IProjectBase>(result, resultDb, ignoredProperties2);
		}

		[Theory]
		[InlineData("TestProject", "Test Description", true)]
		[InlineData("TestProject", null, true)]
		[InlineData(null, "Test Description", false)]
		public async Task InsertProjectTestAsync(string name, string? description, bool shouldSucceed)
		{
			await using var context = await createContextAsync();
			await resetDBAsync(context);

			try
			{

				var project = new Project { Name = name, Description = description };
				var result = await _projectRepository.SaveAsync(project);
				checkSavedData(project, result);

				if (!shouldSucceed)
				{
					Assert.True(false, "Test m�l selhat, ale operace prob�hla bez v�jimky.");
				}

				Assert.NotNull(result);

			}
			catch (Exception ex)
			{
				if (shouldSucceed)
				{
					Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
				}
			}
		}
		[Theory]
		[InlineData("TestProject", true)]
		[InlineData("", false)]
		public async Task DeleteProjectTestAsync(string name, bool shouldSucceed)
		{
			await using var context = await createContextAsync();
			resetDBAsync(context).Wait();

			try
			{
				var project = new Project { Name = name };
				var result = await _projectRepository.SaveAsync(project);

				var resultDelete = await _projectRepository.DeleteAsync(result?.Id ?? 0);
				if (!shouldSucceed)
				{
					Assert.False(resultDelete, "Test m�l selhat, ale operace prob�hla bez v�jimky.");
				}
				else
				{
					Assert.True(resultDelete);
				}
			}
			catch (Exception ex)
			{
				if (shouldSucceed)
				{
					Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
				}
			}
		}
		/// <summary>
		/// testuje multi vkl�d�n� projekt� do DB i to , kdy� se opakuje n�zev projektu, tak to spr�vn� vyhodnot�
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task MultiInsertProjectTestAsync()
		{
			await using var context = await createContextAsync();
			await resetDBAsync(context);
			var shouldSucceed = true;
			try
			{
				var list = new List<ItemTest<Project>>
				{
					new ItemTest<Project>(new Project { Name = "TestProject1", Description = "Test Description 1" },true),
					new ItemTest<Project>(new Project { Name = "TestProject1", Description = "Test Description 1" },false),
					new ItemTest<Project>(new Project { Name = "TestProject2", Description = "Test Description 1" },true),
					new ItemTest<Project>(new Project { Name = "TestProject3"},true)
				};

				foreach (var item in list)
				{
					shouldSucceed = item.ShouldSucceed;
					var result = await _projectRepository.SaveAsync(item.Model);
					checkSavedData(item.Model, result);

					if (!shouldSucceed)
					{
						Assert.True(false, "Test m�l selhat, ale operace prob�hla bez v�jimky.");
					}

					Assert.NotNull(result);
				}
			}
			catch (Exception ex)
			{
				if (shouldSucceed)
				{
					Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
				}
			}
		}

		private void checkSavedData(Project inputProject, IProjectBase outputProejct)
		{
			Assert.NotNull(outputProejct);

			var ignoredProperties = new HashSet<string>
					{
						nameof(Project.SubModules),
						nameof(Project.Activities),
						nameof(Project.Id)
					};

			compareAllProperties<IProjectBase>(inputProject, outputProejct, ignoredProperties);
		}

		private async Task<MsSqlDbContext> createContextAsync()
		{
			var context = new MsSqlDbContext(_dbOptions);
			await context.Database.EnsureCreatedAsync();
			_contextFactoryMock.Setup(x => x.CreateDbContextAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(context);
			return context;
		}

		/// <summary>
		/// P�ed ka�d�m testem datab�zi explicitn� odstra�te a znovu vytvo�te
		/// </summary>
		/// <param name="context"></param>
		private async Task resetDBAsync(MsSqlDbContext context)
		{
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
		}
		private void compareAllProperties<T>(T expectedObj, T actualObj, HashSet<string>? ignoredProperties = null)
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
	}
}