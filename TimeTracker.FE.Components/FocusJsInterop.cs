using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TimeTracker.FE.Components;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class FocusJsInterop : IAsyncDisposable
{
	private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

	public FocusJsInterop(IJSRuntime jsRuntime)
	{
		_moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/TimeTracker.FE.Components/js/focusUtils.js").AsTask());
	}

	public async ValueTask DelayBlur(ElementReference element) 
	{
		var module = await _moduleTask.Value;
		await module.InvokeVoidAsync("removeFocus", element);
	}

	public async ValueTask RemoveFocus(ElementReference element)
	{
		var module = await _moduleTask.Value;
		await module.InvokeVoidAsync("removeFocus", element);
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
