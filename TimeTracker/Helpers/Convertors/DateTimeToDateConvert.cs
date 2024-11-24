using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.Helpers.Convertors
{
	public class DateTimeToDateConvert : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (value != null)
				{
					var convertString = System.Convert.ToDateTime(value.ToString());
					if (convertString is DateTime date)
					{
						return date.ToString("dd.MM.yyyy");
					}
				}
				return value;
			}
			catch (Exception)
			{
				return value;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
