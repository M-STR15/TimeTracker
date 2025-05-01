using TimeTracker.Basic.Enums;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.Tests.DB.UnitTests
{
	public class TypeShiftRepositoryTest : aRepositoryBaseTest<InMemoryDbContext> 
	{
		private readonly TypeShiftRepository<InMemoryDbContext> _typeShiftRepository;

		public TypeShiftRepositoryTest() : base()
		{		
			_typeShiftRepository = new TypeShiftRepository<InMemoryDbContext>(() => new InMemoryDbContext(_dbOptions));
		}

		[Fact]
		public async Task GetAllAsync_ShouldReturnAllTypeShifts()
		{
			// Arrange
			await using var context = await createContextAsync();
			await resetDBAsync(context);
			// Act
			IEnumerable<TypeShift> result = await _typeShiftRepository.GetAllAsync();
			Array enums = Enum.GetValues(typeof(eTypeShift));
			int countEnums = enums.Length;
			// Assert
			Assert.NotNull(result);
			Assert.Equal(countEnums, result.Count());

			var resultIds = result.Select(r => r.Id).ToHashSet();
			var enumIds = Enum.GetValues(typeof(eTypeShift)).Cast<eTypeShift>().Select(e => (int)e).ToHashSet();

			Assert.True(resultIds.SetEquals(enumIds));
		}

		[Fact]
		public async Task GetTypeShiftsForMainWindowAsync_ShouldReturnVisibleTypeShifts()
		{
			// Arrange
			await using var context = await createContextAsync();
			await resetDBAsync(context);

			// Act
			var result = await _typeShiftRepository.GetTypeShiftsForMainWindowAsync();

			// Assert
			Assert.NotNull(result);


			var resultIds = result.Select(r => r.Id).ToHashSet();
			var enumIds = Enum.GetValues(typeof(eTypeShift)).Cast<eTypeShift>().Where(x => x != eTypeShift.Holiday).Select(e => (int)e).ToHashSet();

			Assert.True(resultIds.SetEquals(enumIds));
		}
	}
}
