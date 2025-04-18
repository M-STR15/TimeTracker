using Microsoft.EntityFrameworkCore;
using Moq;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.DB.UnitTests.Models;

namespace TimeTracker.DB.UnitTests
{
	public class ProjectRepositoryTest : aRepositoryBaseTest
	{
		private readonly ProjectRepository<MsSqlDbContext> _projectRepository;
		public ProjectRepositoryTest() : base()
		{
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
				var inputList = new List<ItemTest<Project>>
				{
					new ItemTest<Project>(new Project { Name = "TestProject1", Description = "Test Description 1" },true),
					new ItemTest<Project>(new Project { Name = "TestProject1", Description = "Test Description 1" },false),
					new ItemTest<Project>(new Project { Name = "TestProject2", Description = "Test Description 1" },true),
					new ItemTest<Project>(new Project { Name = "TestProject3"},true)
				};

				foreach (var item in inputList)
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

		[Fact]
		public async Task GetProjectTestAsync()
		{
			await using var context = await createContextAsync();
			await resetDBAsync(context);
			var shouldSucceed = true;
			try
			{
				var inputList = new List<Project>
				{
					new Project { Name = "TestProject1", Description = "Test Description 1" },
					new Project { Name = "TestProject2", Description = "Test Description 1" },
					new Project { Name = "TestProject3"}
				};

				foreach (var item in inputList)
				{
					var result = await _projectRepository.SaveAsync(item);
					Assert.NotNull(result);
				}

				var resultDb = await _projectRepository.GetAllAsync();
				foreach (var expectedItem in inputList)
				{
					Assert.Contains(resultDb, a => a.Name == expectedItem.Name && a.Description == expectedItem.Description);
				}
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
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
	}
}