using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TimerTracker.Windows
{
	/// <summary>
	/// Interaction logic for ShiftsPlanWindow.xaml
	/// </summary>
	public partial class ShiftsPlanWindow : Window
	{
		private List<InfoOfDate> _daiesList = new();

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

			cmbMontAndYear.SelectedIndex = 0;
		}

		private void btnDay_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var infoOfDate = (InfoOfDate)btn.Tag;
			infoOfDate.IsPlanShiftInDay = !infoOfDate.IsPlanShiftInDay;

			btn.Background = new SolidColorBrush(infoOfDate.IsPlanShiftInDay ? Colors.LightBlue : Colors.Gray);
		}

		private void cmbMontAndYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			itmcDays.Items.Clear();
			_daiesList.Clear();

			setHeaderGrid();
			generateDateList();
			generateButtonList();
		}

		private void generateButtonList()
		{
			foreach (var item in _daiesList)
			{
				var button = new Button()
				{
					Name = "btnDay_" + item.Day,
					Content = item.Day,
					Height = 40,
					Width = 40,
					Margin = new Thickness(5),
					Tag = item,
					Background = new SolidColorBrush(Colors.Gray)
				};

				button.Click += btnDay_Click;

				itmcDays.Items.Add(button);
				Grid.SetRow(button, (int)item.WeekInMont + 1);
				Grid.SetColumn(button, InfoOfDate.GetColumn(item.DayOfWeek));
			}
		}

		private void generateDateList()
		{
			var firstDate = Convert.ToDateTime("01." + cmbMontAndYear.SelectedItem);
			var days = DateTime.DaysInMonth(firstDate.Year, firstDate.Month);

			for (int i = 1; i <= days; i++)
			{
				var infoOfDay = new InfoOfDate(Convert.ToDateTime(i + "." + cmbMontAndYear.SelectedItem));
				_daiesList.Add(infoOfDay);
			}
		}
		private void setHeaderGrid()
		{
			var daysList = Enum.GetValues(typeof(DayOfWeek))
										.Cast<DayOfWeek>()
										.ToList();
			foreach (var item in daysList)
			{
				var label = new Label()
				{
					Content = item.ToString(),
				};
				itmcDays.Items.Add(label);
				Grid.SetColumn(label, InfoOfDate.GetColumn(item));
			}
		}
	}

	internal class InfoOfDate
	{
		public InfoOfDate(DateTime date)
		{
			Date = date;
			Day = date.Day;
			DayOfWeek = date.DayOfWeek;
			WeekInMont = getWeekInMonth(date);
		}

		public DateTime Date { get; private set; }
		public int Day { get; private set; }
		public DayOfWeek DayOfWeek { get; private set; }
		public bool IsPlanShiftInDay { get; set; }
		public int WeekInMont { get; private set; }

		private static int getWeekInMonth(DateTime date)
		{
			DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
			int firstDayOfWeek = GetColumn(firstDayOfMonth.DayOfWeek); // 0 = neděle, 1 = pondělí, ...
			int daysSinceStartOfMonth = date.Day;
			int totalDaysBeforeCurrentWeek = daysSinceStartOfMonth + firstDayOfWeek - 1;
			int weekNumber = totalDaysBeforeCurrentWeek / 7 + 1;

			return weekNumber;
		}
		public static int GetColumn(DayOfWeek dayOfWeek) => (dayOfWeek == DayOfWeek.Sunday) ? 6 : (int)dayOfWeek - 1;
	}
}
