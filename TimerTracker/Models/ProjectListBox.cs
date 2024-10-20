using System.ComponentModel;
using TimerTracker.BE.D.Models;

namespace TimerTracker.Models
{
	public class ProjectListBox : IProject, INotifyPropertyChanged
	{
		private string? _description;
		public string? Description
		{
			get => _description;
			set
			{
				if (_description != value)
				{
					_description = value;
					OnPropertyChanged(nameof(Description));
				}
			}
		}
		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				if (_id != value)
				{
					_id = value;
					OnPropertyChanged(nameof(Id));
				}
			}
		}
		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				if (_name != value)
				{
					_name = value;
					OnPropertyChanged(nameof(Name));
				}
			}
		}


		public override string ToString()
		{
			return Name.ToString();
		}
		private bool _isEditable;
		public bool IsEditable 
		{ 
			get=> _isEditable;
			set
			{
				_isEditable = value;
				OnPropertyChanged(nameof(IsEditable));
			}
		}
		public ProjectListBox()
		{ }
		public ProjectListBox(Project project)
		{
			Id = project.Id;
			Name = project.Name;
			Description = project.Description;

			IsEditable = Name == "Project 1";
		}

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
