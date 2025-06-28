using Microsoft.JSInterop;

namespace TimeTracker.FE.Web.Components.Interops
{
	public class LottieLoaderInterop : IAsyncDisposable
	{
		private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

		public LottieLoaderInterop(IJSRuntime jsRuntime)
		{
			_moduleTask = new(() =>
				jsRuntime.InvokeAsync<IJSObjectReference>(
					"import", "./_content/TimeTracker.FE.Web.Components/js/lottieLoader.js"
				).AsTask()
			);
		}

		public async ValueTask Load(string containerId, string animationPath)
		{
			try
			{
				var module = await _moduleTask.Value;
				await module.InvokeVoidAsync("loadLottieAnimation", containerId, animationPath);
			}
			catch (Exception ex)
			{
				var test = ex;
			}
		}

		public async ValueTask DisposeAsync()
		{
			if (_moduleTask.IsValueCreated)
			{
				try
				{
					var module = await _moduleTask.Value;
					await module.DisposeAsync();
				}
				catch (Exception)
				{
					// Ignoruj chybu při dispose
				}
			}
		}
	}
}
