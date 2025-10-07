using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.PC.ViewModels
{
	/// <summary>
	/// ViewModel pro radiobutton typu směny, rozšiřuje entitu TypeShift o vlastnost výběru.
	/// </summary>
	[ObservableObject]
	public partial class TypeShiftRadioButton(TypeShift typeShift) : TypeShift(typeShift.Id, typeShift.Name, typeShift.Color, typeShift.IsVisibleInMainWindow)
	{
		/// <summary>
		/// Určuje, zda je radiobutton vybrán.
		/// </summary>
		[ObservableProperty]
		private bool _isSelected;
	}
}