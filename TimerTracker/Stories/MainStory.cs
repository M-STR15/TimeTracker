using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTracker.Stories
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
