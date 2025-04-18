using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.DB.UnitTests
{
	public class SubModuleRepositoryTest : aRepositoryBaseTest
	{
		private readonly SubModuleRepository<MsSqlDbContext> _subModuleRepository;
		public SubModuleRepositoryTest() : base()
		{
			_subModuleRepository = new SubModuleRepository<MsSqlDbContext>(() => new MsSqlDbContext(_dbOptions));
		}

		/// Testuje, zda metoda GetAllAsync spr�vn� nastav� a z�sk� podmoduly.
		/// </summary>
		[Fact]
		public async Task GetAllAsync_ShouldBeSetAndGetForTheProjectCorrectly()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var project = seedProjectAsync("Test project");
				var project2 = seedProjectAsync("Test project 2");

				ISubModuleBase? subModule = new SubModule
				{
					Name = "Test SubModule",
					ProjectId = project.Id,
					Description = "Test SubModule Description"
				};
				ISubModuleBase? subModule2 = new SubModule
				{
					Name = "Test SubModule 2",
					ProjectId = project.Id
				};
				ISubModuleBase? subModule3 = new SubModule
				{
					Name = "Test SubModule 3",
					ProjectId = project2.Id,
					Description = "Test SubModule Description"
				};

				var subModules = new List<ISubModuleBase?> { subModule, subModule2, subModule3 };

				_ = await _subModuleRepository.SaveAsync(subModule);
				_ = await _subModuleRepository.SaveAsync(subModule2);
				_ = await _subModuleRepository.SaveAsync(subModule3);

				// Act
				var subModuleFromDb = await _subModuleRepository.GetAllAsync();

				// Assert
				Assert.True(
					subModules
					.Select(s => new { s.ProjectId, s.Name, s.Description })
					.ToHashSet()
					.SetEquals(
					subModuleFromDb.Select(s => new { s.ProjectId, s.Name, s.Description })
					),
					"Kolekce shift� nejsou stejn� (bez ohledu na po�ad� a podle hodnot)."
					);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
			}
		}

		/// Testuje, zda metoda GetAsync spr�vn� nastav� a z�sk� podmoduly pro dan� projekt.
		/// </summary>
		[Fact]
		public async Task GetAsync_ShouldBeSetAndGetForTheProjectCorrectly()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var project = seedProjectAsync("Test project");
				var project2 = seedProjectAsync("Test project 2");

				ISubModuleBase? subModule = new SubModule
				{
					Name = "Test SubModule",
					ProjectId = project.Id,
					Description = "Test SubModule Description"
				};
				ISubModuleBase? subModule2 = new SubModule
				{
					Name = "Test SubModule 2",
					ProjectId = project.Id
				};
				ISubModuleBase? subModule3 = new SubModule
				{
					Name = "Test SubModule 3",
					ProjectId = project2.Id,
					Description = "Test SubModule Description"
				};

				var subModules = new List<ISubModuleBase?> { subModule, subModule2, subModule3 };

				_ = await _subModuleRepository.SaveAsync(subModule);
				_ = await _subModuleRepository.SaveAsync(subModule2);
				_ = await _subModuleRepository.SaveAsync(subModule3);

				// Act
				var subModuleFromDb = await _subModuleRepository.GetForTheProjectAsync(subModule.ProjectId);

				// Assert
				Assert.True(
				subModules.Where(x => x.ProjectId == subModule.ProjectId)
				.Select(s => new { s.ProjectId, s.Name, s.Description })
				.ToHashSet()
				.SetEquals(
				subModuleFromDb.Select(s => new { s.ProjectId, s.Name, s.Description })
				),
				"Kolekce shift� nejsou stejn� (bez ohledu na po�ad� a podle hodnot)."
				);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
			}
		}

		/// Testuje, zda metoda SaveAsync spr�vn� ulo�� a na�te podmodul.
		/// </summary>
		[Fact]
		public async Task SaveAsync_ShouldBeSetAndRetrievedCorrectly()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var project = seedProjectAsync("Test project");

				ISubModuleBase? subModule = new SubModule
				{
					Name = "Test SubModule",
					ProjectId = project.Id,
					Description = "Test SubModule Description"
				};

				subModule = await _subModuleRepository.SaveAsync(subModule);

				// Act
				ISubModuleBase? subModuleFromDb = await context.SubModules.FindAsync(subModule?.Id);

				// Assert
				var ignoredProperties = new HashSet<string> { nameof(SubModule.Activities), nameof(SubModule.Id), nameof(SubModule.Activities), nameof(SubModule.Project) };
				compareAllProperties<ISubModuleBase?>(subModule, subModuleFromDb, ignoredProperties);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
			}
		}

		/// Testuje, zda metoda DeleteAsync spr�vn� odstran� podmodul.
		/// </summary>
		[Fact]
		public async Task DeleteAsync_ShouldBeSetAndRetrievedCorrectly()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var project = seedProjectAsync("Test project");

				ISubModuleBase? subModule = new SubModule
				{
					Name = "Test SubModule",
					ProjectId = project.Id,
					Description = "Test SubModule Description"
				};

				subModule = await _subModuleRepository.SaveAsync(subModule);

				// Act
				var result = await _subModuleRepository.DeleteAsync(subModule?.Id ?? 0);

				// Assert
				Assert.True(result);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test m�l usp�t, ale do�lo k v�jimce: {ex.Message}");
			}
		}
	}
}
