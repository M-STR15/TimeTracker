namespace TimeTracker.PC.Stories
{
    public class MainStory
    {
        public ContainerStore ContainerStore { get; private set; }

        public MainStory()
        {
            ContainerStore = new ContainerStore();
        }
    }
}