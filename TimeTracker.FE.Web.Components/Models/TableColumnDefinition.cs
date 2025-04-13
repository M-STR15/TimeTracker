namespace TimeTracker.FE.Web.Components.Models
{
	public enum eTypeColumn
	{
		Text
	}

	public class TableColumnDefinition<TItem>
	{
		public string HeaderCss { get; set; } = string.Empty;
		public string Header { get; set; } = string.Empty;
		public string Key { get; set; } = string.Empty;
		public eTypeColumn eTypeColumn { get; set; } = eTypeColumn.Text;
		public int? Width { get; set; }

		public string ValueCss { get; set; } = string.Empty;

		public string GetWidth()
		{
			if (Width != null)
				return $"width:{Width.ToString()}px;";
			else
				return "width:auto;";
		}
	}
}