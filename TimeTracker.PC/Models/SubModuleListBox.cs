using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.PC.Models
{
	/// <summary>
	/// Třída slouží pro manipulaci v Listboxu, je to mezi vrstva mezi DB a editování dat.
	/// </summary>
	[ObservableObject]
	public partial class SubModuleListBox : ISubModuleWithoutColl, IListBox
	{
		public Guid GuidId { get; private set; }

		[ObservableProperty]
		private bool _isEditable;

		[ObservableProperty]
		private string? _description;

		[ObservableProperty]
		private int _id;

		[ObservableProperty]
		private string _name = string.Empty;

		[ObservableProperty]
		private int _projectId;

		public SubModuleListBox()
		{
			GuidId = Guid.NewGuid();
		}

		public SubModuleListBox(int id, string name, string? description, int projectId) : this()
		{
			Id = id;
			Name = name;
			Description = description;
			ProjectId = projectId;
		}

		public SubModuleListBox(ISubModuleWithoutColl subModule) : this(subModule.Id, subModule.Name, subModule.Description, subModule.ProjectId)
		{ }
	}
}