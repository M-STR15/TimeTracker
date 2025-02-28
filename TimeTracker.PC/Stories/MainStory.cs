namespace TimeTracker.PC.Stories
{
	public class MainStory
	{
		public DIContainerStore ContainerStore { get; private set; }

		public MainStory()
		{
			ContainerStore = new DIContainerStore();
		}
	}
}