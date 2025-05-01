using TimeTracker.Basic.Enums;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.Tests.DB.UnitTests
{
	public class ReportRepositoryTest: aRepositoryBaseTest<InMemoryDbContext> 
	{
		private readonly ReportRepository<InMemoryDbContext> _reportRepository;
		private readonly RecordRepository<InMemoryDbContext> _recordRepository;

		public ReportRepositoryTest() : base()
		{
			_reportRepository = new ReportRepository<InMemoryDbContext>(() => new InMemoryDbContext(_dbOptions));
			_recordRepository = new RecordRepository<InMemoryDbContext>(() => new InMemoryDbContext(_dbOptions));
		}

		[Fact]
		public async Task GetActivityOverDaysAsync_ShouldReturnCorrectData()
		{
			try
			{
				// Arrange
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var record1 = new RecordActivity
				{
					StartDateTime = DateTime.Now.AddHours(-3),
					ActivityId = (int)eActivity.Start,
					TypeShiftId = (int)eTypeShift.Office,
				};

				var record2 = new RecordActivity
				{
					StartDateTime = DateTime.Now.AddHours(-1.5),
					ActivityId = (int)eActivity.Pause,
					TypeShiftId = (int)eTypeShift.Office,
				};

				var record3 = new RecordActivity
				{
					StartDateTime = DateTime.Now.AddHours(-1),
					ActivityId = (int)eActivity.Start,
					TypeShiftId = (int)eTypeShift.Office,
				};
				var item1 = await _recordRepository.SaveAsync(record1);
				var item2 = await _recordRepository.SaveAsync(record2);
				var item3 = await _recordRepository.SaveAsync(record3);
				//var allItems = _recordRepository.GetAllAsync();
				var start = DateTime.Now.AddHours(-4);
				var end = DateTime.Now;

				// Act
				var result = await _reportRepository.GetActivityOverDaysAsync(start, end);

				// Assert
				Assert.NotNull(result);
				var dayData = result.FirstOrDefault(x => x.Date == record1.StartDateTime.Date && x.TypeShift == eTypeShift.Office);
				Assert.NotNull(dayData);
				Assert.Equal(1.5, dayData.WorkHours);
				Assert.Equal(0.5, dayData.Pause);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}
		//TODO dodìlat, když v pøípadì, že se testuje pøes pùlnoc, tak to mùže haprovat
		[Fact]
		public async Task GetActualSumaryHours_ShouldReturnCorrectSummary()
		{
			try
			{
				// Arrange
				await using var context = await createContextAsync();
				await resetDBAsync(context);
				var setDate = DateTime.Now;
				var record1 = new RecordActivity
				{
					StartDateTime = setDate.AddHours(-6),
					TypeShiftId = (int)eTypeShift.Office,
					ActivityId = (int)eActivity.Start,
				};
				var record2 = new RecordActivity
				{
					StartDateTime = setDate.AddHours(-5),
					TypeShiftId = (int)eTypeShift.Office,
					ActivityId = (int)eActivity.Start,
				};
				var record3 = new RecordActivity
				{
					StartDateTime = setDate.AddHours(-4),
					ActivityId = (int)eActivity.Pause,
				};
				var record4 = new RecordActivity
				{
					StartDateTime = setDate.AddHours(-3),
					ActivityId = (int)eActivity.Stop,
				};

				var item1 = await _recordRepository.SaveAsync(record1);
				var item2 = await _recordRepository.SaveAsync(record2);
				var item3 = await _recordRepository.SaveAsync(record3);
				var item4 = await _recordRepository.SaveAsync(record4);

				// Act
				var result = _reportRepository.GetSumaryHoursInDay(setDate.Year, setDate.Month, setDate.Day);

				// Assert
				Assert.NotNull(result);
				Assert.Equal(2, result.WorkHours);
				Assert.Equal(1, result.PauseHours);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Fact]
		public async Task GetWorkplaceHours_ShouldReturnCorrectWorkplaceData()
		{
			try
			{
				// Arrange
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				var record1 = new RecordActivity
				{
					StartDateTime = new DateTime(2025, 4, 1),
					ActivityId = (int)eActivity.Start,
					TypeShiftId = (int)eTypeShift.Office,
				};

				var record2 = new RecordActivity
				{
					StartDateTime = new DateTime(2025, 4, 2),
					ActivityId = (int)eActivity.Start,
					TypeShiftId = (int)eTypeShift.HomeOffice,
				};

				await _recordRepository.SaveAsync(record1);
				await _recordRepository.SaveAsync(record2);

				// Act
				var result = _reportRepository.GetWorkplaceHours(2025, 4);

				// Assert
				Assert.NotNull(result);
				//Assert.Single(result.OfficeWorkHourslist);
				//Assert.Single(result.HomeOfficeWorkHourslist);
				//Assert.Equal(1, result.OfficeWorkHourslist.First().WorkHours);
				//Assert.Equal(2, result.HomeOfficeWorkHourslist.First().WorkHours);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}
	}
}
