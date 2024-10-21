using System.ComponentModel;

namespace TimerTracker.Models
{
	public interface IListBox
	{
		bool IsEditable { get; set; }

		event PropertyChangedEventHandler PropertyChanged;
	}
}