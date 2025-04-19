using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DB.UnitTests.Models
{
	public class ItemTest<T>
	{
		public T Model { get; set; }
		public bool ShouldSucceed { get; set; }

		public ItemTest() 
		{ }

		public ItemTest(T model, bool shouldSucceed)
		{
			Model = model;
			ShouldSucceed = shouldSucceed;
		}
	}
}
