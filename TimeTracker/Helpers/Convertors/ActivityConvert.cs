﻿using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.Helpers.Convertors
{
	public class ActivityConvert : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
