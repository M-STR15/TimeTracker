using TimeTracker.BE.Web.Shared.Services;

namespace TimeTracker.Web.Blazor.Server.Helpers
{
	public class ApplicationLifecycleLogger : IHostedService
	{
		private readonly IEventLogService _logger;

		public ApplicationLifecycleLogger(IEventLogService logger)
		{
			_logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _logger.LogInformationAsync(Guid.Parse("60121569-c9ef-41bf-8ce4-c83d84ff65f4"), "🚀 Aplikace byla spuštěna.");
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await _logger.LogInformationAsync(Guid.Parse("30af2ba3-1d10-47e7-b1ce-0876b577c572"), "🛑 Aplikace se ukončuje.");
		}
	}
}
