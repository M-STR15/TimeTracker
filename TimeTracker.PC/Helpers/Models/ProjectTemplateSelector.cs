using System.Windows;
using System.Windows.Controls;
using TimeTracker.PC.Models;

namespace TimeTracker.PC.Helpers.Models
{
	/// <summary>
	/// Selektor šablon pro projekty v seznamu.
	/// Vybere šablonu podle toho, zda je projekt editovatelný.
	/// </summary>
	internal class ProjectTemplateSelector : DataTemplateSelector
	{
		/// <summary>
		/// Šablona pro editovatelný projekt.
		/// </summary>
		public required DataTemplate EditableTemplate { get; set; }

		/// <summary>
		/// Šablona pro pouze pro čtení projekt.
		/// </summary>
		public required DataTemplate ReadOnlyTemplate { get; set; }

		/// <summary>
		/// Vybere šablonu na základě vlastnosti IsEditable objektu ProjectListBox.
		/// </summary>
		/// <param name="item">Položka seznamu (očekává se ProjectListBox).</param>
		/// <param name="container">Kontejner šablony.</param>
		/// <returns>Vrací odpovídající šablonu.</returns>
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