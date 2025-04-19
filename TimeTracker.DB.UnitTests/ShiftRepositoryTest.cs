using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.Tests.DB.UnitTests
{
	public class ShiftRepositoryTest : aRepositoryBaseTest
	{
		private readonly ShiftRepository<MsSqlDbContext> _shiftRepository;

		public ShiftRepositoryTest() : base()
		{
			_shiftRepository = new ShiftRepository<MsSqlDbContext>(() => new MsSqlDbContext(_dbOptions));
		}

		[Fact]
		public async Task GetAllAsync_ShouldReturnAllShifts()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var shift1 = new Shift { StartDate = DateTime.Now, TypeShiftId = 1, Description = "Shift 1" };
				var shift2 = new Shift { StartDate = DateTime.Now.AddDays(1), TypeShiftId = 2, Description = "Shift 2" };

				context.Shifts.AddRange(shift1, shift2);
				await context.SaveChangesAsync();

				// Act
				var result = await _shiftRepository.GetAllAsync();

				// Assert
				Assert.NotNull(result);
				Assert.Equal(2, result.Count());
				Assert.Contains(result, s => s.Description == "Shift 1");
				Assert.Contains(result, s => s.Description == "Shift 2");
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Fact]
		public async Task GetAsync_ShouldReturnShiftsWithinDateRange()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var shift1 = new Shift { StartDate = DateTime.Now, TypeShiftId = 1, Description = "Shift 1" };
				var shift2 = new Shift { StartDate = DateTime.Now.AddDays(1), TypeShiftId = 2, Description = "Shift 2" };

				context.Shifts.AddRange(shift1, shift2);
				await context.SaveChangesAsync();

				var dateFrom = DateTime.Now.AddDays(-1);
				var dateTo = DateTime.Now.AddDays(2);

				// Act
				var result = await _shiftRepository.GetAsync(dateFrom, dateTo);

				// Assert
				Assert.NotNull(result);
				Assert.Equal(2, result.Count());
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Fact]
		public async Task Save_ShouldAddUpdateAndDeleteShifts()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var existingShift = new Shift { StartDate = new DateTime(2025, 4, 1), TypeShiftId = 1, Description = "Existing Shift" };
				context.Shifts.Add(existingShift);
				context.SaveChanges();

				var newShift = new Shift { StartDate = new DateTime(2025, 4, 2), TypeShiftId = 2, Description = "New Shift" };
				var updatedShift = new Shift(existingShift.GuidId, existingShift.StartDate, 3, "Updated Shift");

				var shifts = new List<Shift> { newShift, updatedShift };

				// Act
				var result = _shiftRepository.Save(shifts);
				var countShifts = context.Shifts.Count();

				var shiftFromDB = (await _shiftRepository.GetAllAsync()).ToList();
				// Assert
				Assert.True(result);
				Assert.Equal(2, countShifts);
				Assert.True(
					shifts
						.Select(s => new { s.GuidId, s.StartDate, s.TypeShiftId })
						.ToHashSet()
						.SetEquals(
							shiftFromDB.Select(s => new { s.GuidId, s.StartDate, s.TypeShiftId })
						),
					"Kolekce shiftù nejsou stejné (bez ohledu na poøadí a podle hodnot)."
				);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}
	}
}
