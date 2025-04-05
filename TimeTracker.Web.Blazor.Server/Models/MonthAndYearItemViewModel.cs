namespace TimeTracker.Web.Blazor.Server.Models
{
	public class MonthAndYearItemViewModel
	{
		public int Month { get; set; }
		public int Year { get; set; }
		public DateTime Date { get; set; }

		public override string ToString()
		{
			try
			{
				return this?.Date.ToString("MM.yyyy") ?? "";
			}
			catch (Exception ex)
			{
				return "";
			}
		}

		public MonthAndYearItemViewModel(DateTime date)
		{
			Month = int.Parse(date.ToString("MM"));
			Year = int.Parse(date.ToString("yyyy"));
			Date = date;
		}
	}
}
