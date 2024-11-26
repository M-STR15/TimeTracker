using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.Helpers.Convertors
{
	public class TotalTimeConvert : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value!=null && value is TimeSpan timeSpanValue)
			{
				return timeSpanValue.ToString(@"hh\:mm\:ss");
			}
			else
			{
				return "";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
