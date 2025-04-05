namespace TimeTracker.Web.Blazor.Server.Models
{
	public class MonthAndYearItem
	{
		public int Month { get; set; }
		public int Year { get; set; }
		public DateTime Date { get; set; }

		public override string ToString()
		{
			return Date.ToString("MM.yyyy");
		}

		public MonthAndYearItem(DateTime date)
		{
			Month = int.Parse(date.ToString("MM"));
			Year = int.Parse(date.ToString("yyyy"));
			Date = date;
		}
	}
}
