using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.DB.UnitTests.Models;

namespace TimeTracker.DB.UnitTests
{
	public class ProjectRepositoryTest : aRepositoryBaseTest
	{
		public ProjectRepositoryTest() : base()
		{
		}

		/// <summary>
		/// Otestování methody pro vkládání projektu do DB.
		/// </summary>
		[Fact]
		public async Task SaveAsync_ShouldAddProject()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var project = new Project { Name = "Test Product", Description = "Test Description" };

				var result = await _projectRepository.SaveAsync(project);
				checkSavedData(project, result);

				var resultDb = await _projectRepository.GetAsync(result.Id);

				Assert.NotNull(result);
				Assert.NotNull(resultDb);

				//otesování zda se vrácený objekt co vystupuje z methody se shoduje s tím co je v DB
				var ignoredProperties = new HashSet<string> { nameof(Project.SubModules), nameof(Project.Activities) };
				compareAllProperties<IProjectBase>(result, resultDb, ignoredProperties);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Theory]
		[InlineData("TestProject", "Test Description", true)]
		[InlineData("TestProject", null, true)]
		[InlineData(null, "Test Description", false)]
		public async Task SaveAsync_ShouldInsertProject(string name, string? description, bool shouldSucceed)
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
					Assert.True(false, "Test mìl selhat, ale operace probìhla bez výjimky.");
				}

				Assert.NotNull(result);

			}
			catch (Exception ex)
			{
				if (shouldSucceed)
				{
					Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
				}
			}
		}
		[Theory]
		[InlineData("TestProject", true)]
		[InlineData("", false)]
		public async Task DeleteAsync_ShouldRemoveProject(string name, bool shouldSucceed)
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
					Assert.False(resultDelete, "Test mìl selhat, ale operace probìhla bez výjimky.");
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
					Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
				}
			}
		}
		/// <summary>
		/// testuje multi vkládání projektù do DB i to , když se opakuje název projektu, tak to správnì vyhodnotí
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task SaveAsync_ShouldMultiInsertProjects()
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
						Assert.True(false, "Test mìl selhat, ale operace probìhla bez výjimky.");
					}

					Assert.NotNull(result);
				}
			}
			catch (Exception ex)
			{
				if (shouldSucceed)
				{
					Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
				}
			}
		}

		[Fact]
		public async Task GetAllAsync_ShouldReturnAllProjects()
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
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
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