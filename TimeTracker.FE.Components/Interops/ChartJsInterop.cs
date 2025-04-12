using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TimeTracker.FE.Components.Interops
{
	public class ChartJsInterop : IAsyncDisposable
	{
		private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

		public ChartJsInterop(IJSRuntime jsRuntime)
		{
			_moduleTask = new(() =>
				jsRuntime.InvokeAsync<IJSObjectReference>(
					"import", "./_content/TimeTracker.FE.Components/js/mchart.js"
				).AsTask()
			);
			//_moduleTask = new(() =>
			//	jsRuntime.InvokeAsync<IJSObjectReference>(
			//		"import", "https://cdn.jsdelivr.net/npm/chart.js"
			//	).AsTask()
			//);
		}

		public async ValueTask SetupChart(string id, object config)
		{
			var module = await _moduleTask.Value;
			await module.InvokeVoidAsync("setup", id, config);
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