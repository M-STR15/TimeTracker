using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimerTracker.Models.Database;
using TimerTracker.Providers;
using TimerTracker.Stories;

namespace TimerTracker.Windows
{
    public partial class ShiftsPlanWindow : Window
	{
		private List<InfoOfDate> _daiesList = new();
		private MainStory _mainStory;
		private ShiftProvider _shiftProvider;
		public ShiftsPlanWindow(MainStory mainStory)
		{
			InitializeComponent();

			_mainStory = mainStory;
			_shiftProvider = mainStory.ContainerStore.GetShiftProvider();

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
			var setIsPlanShiftInDay = !infoOfDate.IsPlanShiftInDay;
			infoOfDate.IsPlanShiftInDay = setIsPlanShiftInDay;

			btn.Background = new SolidColorBrush(setIsPlanShiftInDay ? Colors.LightBlue : Colors.Gray);
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			var shifts = new List<Shift>();

			foreach (var item in _daiesList.Where(x => x.IsPlanShiftInDay).ToList())
			{
				var shift = new Shift(item.GuidId, item.Date);
				shifts.Add(shift);
			}
			_shiftProvider.SaveShifts(shifts);

			this.Close();
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
					Background = getColorButtom(item.IsPlanShiftInDay)
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
			var lastDate = Convert.ToDateTime(days + "." + cmbMontAndYear.SelectedItem);

			var planShiftsInDB = _shiftProvider.GetShifts(firstDate, lastDate);
			for (int i = 1; i <= days; i++)
			{
				var date = Convert.ToDateTime(i + "." + cmbMontAndYear.SelectedItem);
				var anyShiftInDb = planShiftsInDB?.Any(x => x.StartDate == date) ?? false;
				var shift = new Shift();
				if (anyShiftInDb)
					shift = planShiftsInDB.First(x => x.StartDate == date);

				var guidId = anyShiftInDb ? shift.GuidId : Guid.Empty;
				var infoOfDate = new InfoOfDate(date, guidId, anyShiftInDb);

				_daiesList.Add(infoOfDate);
			}
		}

		private SolidColorBrush getColorButtom(bool isPlanShiftInDay) => new SolidColorBrush(isPlanShiftInDay ? Colors.LightBlue : Colors.Gray);
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
		private string? _description;

		private bool _isPlanShiftInDay;

		public InfoOfDate(DateTime date, Guid guidID, bool isPlanShiftInDay, string? description = null)
		{
			GuidId = guidID;
			Date = date;
			Day = date.Day;
			DayOfWeek = date.DayOfWeek;
			WeekInMont = getWeekInMonth(date);
			Description = description;
			IsPlanShiftInDay = isPlanShiftInDay;

			IsEdited = false;
		}

		public DateTime Date { get; private set; }
		public int Day { get; private set; }
		public DayOfWeek DayOfWeek { get; private set; }
		public string? Description
		{
			get => _description;
			set
			{
				_description = value;
				IsEdited = true;
			}
		}
		public Guid GuidId { get; private set; }
		public bool IsEdited { get; private set; }

		public bool IsPlanShiftInDay
		{
			get => _isPlanShiftInDay;
			set
			{
				_isPlanShiftInDay = value;
				IsEdited = true;
			}
		}
		public int WeekInMont { get; private set; }
		public static int GetColumn(DayOfWeek dayOfWeek) => (dayOfWeek == DayOfWeek.Sunday) ? 6 : (int)dayOfWeek - 1;

		private static int getWeekInMonth(DateTime date)
		{
			DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
			int firstDayOfWeek = GetColumn(firstDayOfMonth.DayOfWeek); // 0 = neděle, 1 = pondělí, ...
			int daysSinceStartOfMonth = date.Day;
			int totalDaysBeforeCurrentWeek = daysSinceStartOfMonth + firstDayOfWeek - 1;
			int weekNumber = totalDaysBeforeCurrentWeek / 7 + 1;

			return weekNumber;
		}
	}
}
