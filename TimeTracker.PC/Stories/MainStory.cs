namespace TimeTracker.PC.Stories
{
	public class MainStory
	{
		public DIContainerStore DIContainerStore { get; private set; }

		public MainStory(DIContainerStore _diContainerStore)
		{
			DIContainerStore = _diContainerStore;
		}
	}
}