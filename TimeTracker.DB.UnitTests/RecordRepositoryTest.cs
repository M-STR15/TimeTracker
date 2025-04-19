using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.Tests.DB.UnitTests
{
	/// <summary>
	/// Testovací třída pro testování metod třídy RecordRepository.
	/// </summary>
	public class RecordRepositoryTest : aRepositoryBaseTest
	{
		private readonly RecordRepository<MsSqlDbContext> _recordRepository;

		/// <summary>
		/// Konstruktor třídy RecordRepositoryTest.
		/// Inicializuje instanci RecordRepository.
		/// </summary>
		public RecordRepositoryTest() : base()
		{
			_recordRepository = new RecordRepository<MsSqlDbContext>(() => new MsSqlDbContext(_dbOptions));
		}

		/// <summary>
		/// Testuje metodu SaveAsync, která by měla přidat nový záznam do databáze.
		/// </summary>
		[Fact]
		public async Task SaveAsync_ShouldAddNewRecord()
		{
			await using var context = await createContextAsync();
			await resetDBAsync(context);
			var project = await seedProjectAsync("Test");

			try
			{
				// Arrange
				var record = new RecordActivity
				{
					StartDateTime = DateTime.Now,
					ActivityId = project.Id,
					Description = "Test Activity"
				};

				// Act
				var resultDb = await _recordRepository.SaveAsync(record);

				// Assert
				Assert.NotNull(resultDb);

				var ignoredProperties = new HashSet<string> { nameof(RecordActivity.GuidId), nameof(RecordActivity.Activity), nameof(RecordActivity.Project), nameof(RecordActivity.SubModule), nameof(RecordActivity.TypeShift) };
				compareAllProperties(record, resultDb, ignoredProperties);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}

		/// <summary>
		/// Testuje metodu DeleteAsync, která by měla odstranit záznam z databáze.
		/// </summary>
		[Fact]
		public async Task DeleteAsync_ShouldRemoveRecord()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);
				var project = await seedProjectAsync("Test");

				// Arrange
				var record = new RecordActivity
				{
					StartDateTime = DateTime.Now,
					ActivityId = project.Id,
					Description = "Test Activity"
				};

				var resultDb = await _recordRepository.SaveAsync(record);

				// Act
				var result = await _recordRepository.DeleteAsync(resultDb.GuidId);

				// Assert
				Assert.True(result);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}

		/// <summary>
		/// Testuje metodu GetAsync, která by měla vrátit záznam podle zadaného Guid.
		/// </summary>
		[Fact]
		public async Task GetAsync_ShouldReturnRecordByGuid()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);
				var project = await seedProjectAsync("Test");

				// Arrange
				var record = new RecordActivity
				{
					StartDateTime = DateTime.Now,
					ActivityId = project.Id,
					Description = "Test Activity"
				};

				var resultDb = await _recordRepository.SaveAsync(record);

				// Act
				var result = await _recordRepository.GetAsync(resultDb.GuidId);

				// Assert
				Assert.NotNull(result);

				var ignoredProperties = new HashSet<string> { nameof(RecordActivity.GuidId), nameof(RecordActivity.Activity), nameof(RecordActivity.Project), nameof(RecordActivity.SubModule), nameof(RecordActivity.TypeShift) };
				compareAllProperties(record, resultDb, ignoredProperties);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}

		/// <summary>
		/// Testuje metodu GetAllAsync, která by měla vrátit všechny záznamy z databáze.
		/// </summary>
		[Fact]
		public async Task GetAllAsync_ShouldReturnAllRecords()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);
				var project = await seedProjectAsync("Test");
				var project2 = await seedProjectAsync("Test2");

				// Arrange
				var record1 = new RecordActivity
				{
					StartDateTime = DateTime.Now,
					ActivityId = project.Id,
					Description = "Test Activity 1"
				};

				var record2 = new RecordActivity
				{
					StartDateTime = DateTime.Now.AddMinutes(1),
					ActivityId = project2.Id,
					Description = "Test Activity 2"
				};

				await _recordRepository.SaveAsync(record1);
				await _recordRepository.SaveAsync(record2);

				// Act
				var resultDb = await _recordRepository.GetAllAsync();

				// Assert
				Assert.NotNull(resultDb);
				Assert.Equal(2, resultDb.Count());
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}
	}
}
