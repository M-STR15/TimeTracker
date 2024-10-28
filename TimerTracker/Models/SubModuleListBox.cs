using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using TimerTracker.BE.DB.Models;

namespace TimerTracker.Models
{
    [ObservableObject]
    public partial class SubModuleListBox : ISubModuleWithoutColl, IListBox
    {
        public Guid GuidId { get; private set; }
        private bool _isEditable;
        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;
                OnPropertyChanged();
            }
        }
        private string? _description;
        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private int _projectId;
        public int ProjectId
        {
            get => _projectId;
            set
            {
                _projectId = value;
                OnPropertyChanged();
            }
        }

        public SubModuleListBox()
        {
            GuidId = Guid.NewGuid();
        }

        public SubModuleListBox(int id, string name, string description, int projectId) : this()
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
