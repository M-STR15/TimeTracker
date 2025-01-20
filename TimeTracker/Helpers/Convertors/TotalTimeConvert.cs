using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.Helpers.Convertors
{
	public class TotalTimeConvert : IValueConverter
	{
		/// <summary>
		/// Převádí hodnotu TimeSpan na formátovaný řetězec.
		/// Pokud je počet dnů 0, vrátí pouze hodiny, minuty a sekundy.
		/// Jinak vrátí počet dnů a čas ve formátu hh:mm:ss.
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null && value is TimeSpan timeSpanValue)
			{
				string result = "";
				if (timeSpanValue.Days == 0)
				{
					result = $"{timeSpanValue:hh\\:mm\\:ss}";
				}
				else
				{
					result = $"{timeSpanValue.Days} day{(timeSpanValue.Days != 1 ? "s" : "")} {timeSpanValue:hh\\:mm\\:ss}";
				}
				return result;
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
