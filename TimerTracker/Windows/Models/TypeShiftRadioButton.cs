using CommunityToolkit.Mvvm.ComponentModel;
using TimerTracker.BE.DB.Models;

namespace TimerTracker.Windows.Models
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
