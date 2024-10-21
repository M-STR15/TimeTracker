using System.ComponentModel;
using TimerTracker.BE.D.Models;

namespace TimerTracker.Models
{
	public class SubModuleListBox : ISubModuleWithoutColl, INotifyPropertyChanged, IListBox
	{
		private bool _isEditable;
		public bool IsEditable
		{
			get => _isEditable;
			set
			{
				_isEditable = value;
				OnPropertyChanged(nameof(IsEditable));
			}
		}
		private string? _description;
		public string? Description
		{
			get => _description;
			set
			{
				_description = value;
				OnPropertyChanged(nameof(Description));
			}
		}
		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}
		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		private int _projectId;
		public int ProjectId
		{
			get => _projectId;
			set
			{
				_projectId = value;
				OnPropertyChanged(nameof(ProjectId));
			}
		}

		public SubModuleListBox()
		{ }

		public SubModuleListBox(int id, string name, string description, int projectId)
		{
			Id = id;
			Name = name;
			Description = description;
			ProjectId = projectId;
		}

		public SubModuleListBox(ISubModuleWithoutColl subModule) : this(subModule.Id, subModule.Name, subModule.Description, subModule.ProjectId)
		{ }

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
