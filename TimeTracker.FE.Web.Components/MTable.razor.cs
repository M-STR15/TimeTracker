using Microsoft.AspNetCore.Components;
using TimeTracker.FE.Web.Components.Models;

namespace TimeTracker.FE.Web.Components
{
	public partial class MTable<TRow> : ComponentBase
	{
		/// <summary>
		///  Název zobrazení nebo kontext, který slouží k filtrování sloupců.
		/// </summary>
		[Parameter, EditorRequired]
		public IList<TableColumnDefinition<TRow>>? Columns { get; set; }

		/// <summary>
		/// Filtr pro zobrazení jenom konkrétních sloupců.
		/// </summary>
		[Parameter]
		public string DisplayView { get; set; } = "";

		//[Parameter]
		//public EventCallback<IList<TRow>> OnRowsUpdated { get; set; }
		/// <summary>
		/// Data zobrazovaná v jednotlivých řádcích tabulky.
		/// </summary>
		[Parameter, EditorRequired]
		public IList<TRow>? Rows { get; set; }
		/// <summary>
		/// Aktuálně vybraný řádek.
		/// </summary>
		[Parameter]
		public TRow? Selected { get; set; }
		/// <summary>
		/// Callback, který se vyvolá při změně vybraného řádku.
		/// </summary>
		[Parameter]
		public EventCallback<TRow?> SelectedChanged { get; set; }
		/// <summary>
		/// Callback, který se vyvolá při výběru řádku.
		/// </summary>
		[Parameter]
		public EventCallback<TRow?> OnRowSelected { get; set; }

		protected override Task OnInitializedAsync()
		{
			return base.OnInitializedAsync();
		}

		private void OnRowClicked(TRow row)
		{
			Selected = row;
			OnRowSelected.InvokeAsync(row);
			SelectedChanged.InvokeAsync(row);
		}

		private string GetRowClass(TRow row)
		{
			return row.Equals(Selected) ? "selected-row" : string.Empty;
		}
	}
}