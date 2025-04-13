using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TimeTracker.FE.Web.Components.Interops
{
	public class ChartJsInterop : IAsyncDisposable
	{
		private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

		public ChartJsInterop(IJSRuntime jsRuntime)
		{
			_moduleTask = new(() =>
				jsRuntime.InvokeAsync<IJSObjectReference>(
					"import", "./_content/TimeTracker.FE.Web.Components/js/mchart.js"
				).AsTask()
			);
		}

		public async ValueTask SetupChartAsync(ElementReference canvasRef, object config)
		{
			var module = await _moduleTask.Value;
			await module.InvokeVoidAsync("setup", canvasRef, config);
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