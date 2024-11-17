using System.Globalization;
using System.Windows.Controls;

namespace TimeTracker.Helpers.Validations
{
	public class TimeFormatValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return new ValidationResult(false, "Čas je povinný.");
			}

			if (TimeSpan.TryParseExact(value.ToString(), "hh\\:mm\\:ss", cultureInfo, out _))
			{
				return ValidationResult.ValidResult;
			}

			return new ValidationResult(false, "Čas musí být ve formátu HH:mm:ss.");
		}
	}
}
