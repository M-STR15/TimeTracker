namespace TimeTracker.PC.Stories
{
	/// <summary>
	/// Hlavní třída příběhu aplikace.
	/// Uchovává instanci DIContainerStore pro správu závislostí.
	/// </summary>
	public class MainStory
	{
		/// <summary>
		/// Uchovává instanci DIContainerStore.
		/// </summary>
		public DIContainerStore DIContainerStore { get; private set; }

		/// <summary>
		/// Konstruktor třídy MainStory.
		/// Inicializuje instanci DIContainerStore.
		/// </summary>
		/// <param name="_diContainerStore">Instance DIContainerStore pro správu závislostí.</param>
		public MainStory(DIContainerStore _diContainerStore)
		{
			DIContainerStore = _diContainerStore;
		}
	}
}