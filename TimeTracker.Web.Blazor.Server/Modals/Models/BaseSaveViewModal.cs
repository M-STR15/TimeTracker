using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace TimeTracker.Web.Blazor.Server.Modals.Models
{
	public abstract class BaseSaveViewModal : BaseViewModal
	{
		[Inject]
		protected IMapper? _mapper { get; set; }
		protected abstract void save_Click();

		protected bool isFormValid = false;
		protected EditContext? editContext;
		protected virtual void onFieldChanged(object? sender, FieldChangedEventArgs e)
		{
			if (editContext != null)
			{
				isFormValid = editContext.Validate();
				StateHasChanged(); // přepočítá stav komponenty
			}
		}

		protected virtual async Task closeModalAsync()
		{
			Visible = false;
			await OnModalClosed.InvokeAsync(null);
		}

		protected override void OnParametersSet()
		{
			if (Visible)
			{
				isFormValid = false;
				performActionOnOpen();
			}
			else
			{
				performActionOnClose();
			}
		}


		protected override void OnInitialized()
		{
			base.OnInitialized();

			try
			{
				_httpClient = _httpClientFactory.CreateClient("TimeTrackerAPI");
			}
			catch (Exception)
			{

			}
		}
		protected abstract void performActionOnOpen();
		protected abstract void performActionOnClose();
	}
}
