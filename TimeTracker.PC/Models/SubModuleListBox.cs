using CommunityToolkit.Mvvm.ComponentModel;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.PC.Models
{
	/// <summary>
	/// Třída slouží pro manipulaci v Listboxu, je to mezi vrstva mezi DB a editování dat.
	/// </summary>
	[ObservableObject]
	public partial class SubModuleListBox : ISubModuleBase, IListBox
	{
		/// <summary>
		/// Jedinečný identifikátor instance (GUID).
		/// </summary>
		public Guid GuidId { get; private set; }

		/// <summary>
		/// Určuje, zda je položka editovatelná.
		/// </summary>
		[ObservableProperty]
		private bool _isEditable;

		/// <summary>
		/// Popis submodulu.
		/// </summary>
		[ObservableProperty]
		private string? _description;

		/// <summary>
		/// Identifikátor submodulu.
		/// </summary>
		[ObservableProperty]
		private int _id;

		/// <summary>
		/// Název submodulu.
		/// </summary>
		[ObservableProperty]
		private string _name = string.Empty;

		/// <summary>
		/// Identifikátor projektu, ke kterému submodul patří.
		/// </summary>
		[ObservableProperty]
		private int _projectId;

		/// <summary>
		/// Výchozí konstruktor, generuje nový GUID.
		/// </summary>
		public SubModuleListBox()
		{
			GuidId = Guid.NewGuid();
		}

		/// <summary>
		/// Konstruktor s parametry pro inicializaci vlastností.
		/// </summary>
		/// <param name="id">Identifikátor submodulu.</param>
		/// <param name="name">Název submodulu.</param>
		/// <param name="description">Popis submodulu.</param>
		/// <param name="projectId">Identifikátor projektu.</param>
		public SubModuleListBox(int id, string name, string? description, int projectId) : this()
		{
			Id = id;
			Name = name;
			Description = description;
			ProjectId = projectId;
		}

		/// <summary>
		/// Konstruktor pro vytvoření instance z objektu implementujícího ISubModuleBase.
		/// </summary>
		/// <param name="subModule">Objekt submodulu.</param>
		public SubModuleListBox(ISubModuleBase subModule) : this(subModule.Id, subModule.Name, subModule.Description, subModule.ProjectId)
		{ }
	}
}