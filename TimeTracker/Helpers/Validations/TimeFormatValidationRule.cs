using System.Globalization;
using System.Windows.Controls;

namespace TimeTracker.PC.Helpers.Validations
{
	public class TimeFormatValidationRule : ValidationRule
	{
		/// <summary>
		/// Ověřuje, zda je zadaná hodnota platným časovým formátem HH:mm:ss.
		/// </summary>
		/// <param name="value">Hodnota k ověření.</param>
		/// <param name="cultureInfo">Kulturní informace pro formátování.</param>
		/// <returns>Výsledek ověření, který indikuje, zda je hodnota platná.</returns>
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
