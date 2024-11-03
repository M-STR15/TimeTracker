using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.Windows.Models
{
    [ObservableObject]
    public partial class TypeShiftRadioButton : TypeShift
    {
        [ObservableProperty]
        private bool _isSelected;

        public TypeShiftRadioButton(TypeShift typeShift) : base(typeShift.Id, typeShift.Name, typeShift.Color, typeShift.IsVisibleInMainWindow)
        { }
    }
}