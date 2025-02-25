using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.PC.Models
{
	[ObservableObject]
	public partial class ProjectListBox : IProjectWithoutColl, IListBox
	{
		public Guid GuidId { get; private set; }

		[ObservableProperty]
		private string? _description;

		[ObservableProperty]
		private int _id;

		[ObservableProperty]
		private string _name = string.Empty;

		public override string ToString()
		{
			return Name.ToString();
		}

		[ObservableProperty]
		private bool _isEditable;

		public ProjectListBox()
		{
			GuidId = Guid.NewGuid();
		}

		public ProjectListBox(string name = "", string description = "", bool isEditable = false) : this()
		{
			Name = name;
			Description = description;
			IsEditable = isEditable;
		}

		public ProjectListBox(Project project) : this(project.Name, project?.Description ?? "")
		{
			Id = project?.Id ?? 0;
			IsEditable = false;
		}
	}
}