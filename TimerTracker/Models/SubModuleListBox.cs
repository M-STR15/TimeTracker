using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using TimerTracker.BE.DB.Models;

namespace TimerTracker.Models
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
        private string _name;
        [ObservableProperty]
        private int _projectId;
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
