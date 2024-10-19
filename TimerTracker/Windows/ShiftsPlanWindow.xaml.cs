using System.Windows;
using System.Windows.Controls;

namespace TimerTracker.Windows
{
	/// <summary>
	/// Interaction logic for ShiftsPlanWindow.xaml
	/// </summary>
	public partial class ShiftsPlanWindow : Window
	{
		private List<DateTime> _daiesList = new();

		public ShiftsPlanWindow()
		{
			InitializeComponent();

			var monthAndShift = new List<string>();
			var countMountBack = 6;
			var currentDate = DateTime.Now;
			var currentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

			for (int i = 0; i < countMountBack; i++)
			{
				monthAndShift.Add(currentMonth.AddMonths(-i).ToString("MM.yyyy"));
			}

			cmbMontAndYear.ItemsSource = null;
			cmbMontAndYear.ItemsSource = monthAndShift;
		}

		private void cmbMontAndYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			itmcDays.Items.Clear();
			_daiesList.Clear();
			var date = Convert.ToDateTime("01." + cmbMontAndYear.SelectedItem);
			var days = DateTime.DaysInMonth(date.Year, date.Month);
			for (int i = 1; i < days; i++)
			{
				_daiesList.Add(Convert.ToDateTime(i + "." + cmbMontAndYear.SelectedItem));
			}

			foreach (var item in _daiesList)
			{
				var button = new Button()
				{
					Name = "btnDay" + item.ToString("dd"),
					Content = item.ToString("dd"),
					Height = 100,
					Width = 100,
					Margin = new Thickness(5),
				};
				itmcDays.Items.Add(button);
			}
		}
	}
}
