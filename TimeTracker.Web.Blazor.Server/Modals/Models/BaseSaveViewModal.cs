using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;

namespace TimeTracker.Web.Blazor.Server.Modals.Models
{
	public abstract class BaseSaveViewModal : BaseViewModal
	{
		[Inject]
		protected IMapper? _mapper { get; set; }
		/// <summary>
		/// Událost, která se spustí při kliknutí na tlačítko Uložit.
		/// </summary>
		protected abstract void save_Click();
		/// <summary>
		/// Určuje, zda je formulář platný (všechny validace prošly).
		/// </summary>
		protected bool isFormValid = false;
		/// <summary>
		/// EditContext pro sledování změn ve formuláři a validaci.
		/// </summary>
		protected EditContext? editContext;

		protected virtual void onFieldChanged(object? sender, FieldChangedEventArgs e)
		{
			if (editContext != null)
			{
				isFormValid = editContext.Validate();

				StateHasChanged(); // přepočítá stav komponenty
			}
		}

		/// <summary>
		/// Zavře modální okno a spustí událost OnModalClosed.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task closeModalAsync()
		{
			Visible = false;
			await OnModalClosed.InvokeAsync(null);
		}
		/// <summary>
		/// Metoda, která se volá při změně parametrů komponenty.
		/// </summary>
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
		/// <summary>
		/// Akce, která se provede při otevření modálního okna.
		/// </summary>
		protected abstract void performActionOnOpen();
		/// <summary>
		/// Akce, která se provede při zavření modálního okna.
		/// </summary>
		protected abstract void performActionOnClose();
	}
}
