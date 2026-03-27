using Microsoft.JSInterop;

namespace TimeTracker.FE.Web.Components.Interops
{
	public class CssLoaderInterop : IAsyncDisposable
	{
		private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

		public CssLoaderInterop(IJSRuntime jsRuntime)
		{
			_moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
				"import", "./_content/TimeTracker.FE.Web.Components/js/cssLoader.js").AsTask());
		}

		public async ValueTask LoadCss(string cssPath)
		{
			var module = await _moduleTask.Value;
			await module.InvokeVoidAsync("loadCss", $"./_content/TimeTracker.FE.Web.Components/{cssPath}");
		}

		/// <summary>
		/// Načte všechny CSS soubory paralelně jedním JS voláním.
		/// </summary>
		public async ValueTask LoadCssBatch(params string[] cssPaths)
		{
			var module = await _moduleTask.Value;
			var fullPaths = cssPaths.Select(p => $"./_content/TimeTracker.FE.Web.Components/{p}").ToArray();
			await module.InvokeVoidAsync("loadCssBatch", (object)fullPaths);
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
				}
			}
		}

	}
}
