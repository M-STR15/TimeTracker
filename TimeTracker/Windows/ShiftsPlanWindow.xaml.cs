using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;
using TimeTracker.Services;
using TimeTracker.Stories;
using TimeTracker.Windows.Models;

namespace TimeTracker.Windows
{
    public partial class ShiftsPlanWindow : Window
    {
        private List<InfoOfDate> _dailyList = new();
        private List<TypeShiftRadioButton> _typeShifts = new();
        private MainStory _mainStory;
        private ShiftProvider _shiftProvider;
        private EventLogService _eventLogService;

        public ShiftsPlanWindow(MainStory mainStory)
        {
            InitializeComponent();

            _eventLogService = new EventLogService();
            _mainStory = mainStory;
            _shiftProvider = mainStory.ContainerStore.GetShiftProvider();

            _typeShifts = _shiftProvider.GetTypeShifts().Select(x => new TypeShiftRadioButton(x)).ToList();
            if (_typeShifts.Count > 0)
                _typeShifts.First().IsSelected = true;

            lvTypeShifts.ItemsSource = _typeShifts;

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

        private void generateButtonList()
        {
            foreach (var item in _dailyList)
            {
                var button = new Button()
                {
                    Name = "btnDay_" + item.Day,
                    Content = item.Day,
                    Height = 40,
                    Width = 40,
                    Margin = new Thickness(5),
                    Tag = item,
                    Background = getColorButtom(item)
                };

                button.Click += onClickOnBtnDay_Click;

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

                _dailyList.Add(infoOfDate);
            }
        }

        private SolidColorBrush getColorButtom(InfoOfDate infoOfDate)
        {
            if (infoOfDate.IsPlanShiftInDay)
            {
                var typeSchift = _typeShifts.FirstOrDefault(x => x.Id == (int)infoOfDate.eTypeShift);
                if (typeSchift != null)
                {
                    var colorString = typeSchift.Color;
                    var color = (Color)ColorConverter.ConvertFromString(colorString);
                    return new SolidColorBrush(color);
                }
                else
                {
                    return new SolidColorBrush(Colors.Gray);
                }
            }
            else
            {
                return new SolidColorBrush(Colors.Gray);
            }
        }

        private void onClickOnBtnDay_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoOfDate = (InfoOfDate)btn.Tag;
            infoOfDate.IsPlanShiftInDay = (!infoOfDate.IsPlanShiftInDay);
            var selTypeShift = _typeShifts.FirstOrDefault(x => x.IsSelected);
            infoOfDate.eTypeShift = (eTypeShift)Enum.ToObject(typeof(eTypeShift), selTypeShift?.Id ?? 0);

            btn.Background = getColorButtom(infoOfDate);
        }

        private void onChangeItemMontAndYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                itmcDays.Items.Clear();
                _dailyList.Clear();

                setHeaderGrid();
                generateDateList();
                generateButtonList();
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("ee6b3d6c-5dd4-4db9-a083-d7144229bf80"), ex.Message, "Problém s přepnutí měsíců.");
            }
        }

        private void onSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var shifts = new List<Shift>();

                foreach (var item in _dailyList.Where(x => x.IsPlanShiftInDay).ToList())
                {
                    var shift = new Shift(item.GuidId, item.Date, (int)item.eTypeShift);
                    shifts.Add(shift);
                }
                var result = _shiftProvider.SaveShifts(shifts);

                this.Close();
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("f40f8f19-9bb9-40e5-a293-1e95acc63384"), ex.Message, "Problém s uložením směn.");
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
}