using TimeTracker.Basic.Enums;

namespace TimeTracker.Tests.DB.UnitTests
{
	public class ActivityRepositoryTest : aRepositoryBaseTest
	{
		public ActivityRepositoryTest() : base()
		{ }

		[Fact]
		public async Task GetAllAsync_ShouldReturnAllActivities()
		{
			try
			{
				await using var context = await createContextAsync();
				await resetDBAsync(context);

				// Act
				var result = await _activityRepository.GetAllAsync();


				var countEnums = Enum.GetValues(typeof(eActivity)).Length;
				var countItemsInDb = result.Count();
				// Assert
				Assert.NotNull(result);
				Assert.True(countEnums == countItemsInDb);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test mìl uspìt, ale došlo k výjimce: {ex.Message}");
			}
		}
	}
}
