using System.Windows;
using System.Windows.Controls;
using TimeTracker.PC.Models;

namespace TimeTracker.PC.Helpers.Models
{
	public class ProjectTemplateSelector : DataTemplateSelector
	{
		public required DataTemplate EditableTemplate { get; set; }
		public required DataTemplate ReadOnlyTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item is ProjectListBox project)
			{
				return project.IsEditable ? EditableTemplate : ReadOnlyTemplate;
			}
			return base.SelectTemplate(item, container);
		}
	}
}