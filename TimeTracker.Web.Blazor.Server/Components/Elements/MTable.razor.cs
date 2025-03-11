using Microsoft.AspNetCore.Components;
using TimeTracker.Web.Blazor.Server.Models;

namespace TimeTracker.Web.Blazor.Server.Components.Elements
{
	public partial class MTable<TRow> : ComponentBase
	{
		[Parameter, EditorRequired]
		public IList<TableColumnDefinition<TRow>>? Columns { get; set; }

		[Parameter]
		public string DisplayView { get; set; } = "";

		[Parameter]
		public EventCallback<IList<TRow>> OnRowsUpdated { get; set; }

		[Parameter]
		public EventCallback<TRow> OnRowUpdated { get; set; }

		[Parameter, EditorRequired]
		public IList<TRow>? Rows { get; set; }

		protected override Task OnInitializedAsync()
		{
			return base.OnInitializedAsync();
		}

		private byte[]? getArrayByteValue(object? value)
		{
			return value is byte[] convertVal ? convertVal : null;
		}

		private int getIntValue(object? value)
		{
			return value is int intValue ? intValue : 0;
		}

		private void setIntValue(ref object value, ref string propertyName, ref int newValue, int rowIndex)
		{
			if (Rows != null && Rows.Count > 0)
			{
				var item = Rows[rowIndex]; // Získáte konkrétní objekt na řádku
				var property = item.GetType().GetProperty(propertyName); // Najdete vlastnost podle názvu

				if (property != null && property.CanWrite) // Zkontrolujte, zda vlastnost existuje a lze ji zapisovat
				{
					property.SetValue(item, newValue); // Nastavíte novou hodnotu
				}

				OnRowsUpdated.InvokeAsync(Rows);
				OnRowUpdated.InvokeAsync(item);

				StateHasChanged();
			}
		}
	}
}