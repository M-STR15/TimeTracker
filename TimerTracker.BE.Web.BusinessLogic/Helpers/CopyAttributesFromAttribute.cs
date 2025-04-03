namespace TimerTracker.BE.Web.BusinessLogic.Helpers
{
	/// <summary>
	/// Helper třída napomáhající přidání atributů z jedné třídy na druhou děděnou.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	internal class CopyAttributesFromAttribute : Attribute
	{
		public Type SourceType { get; }

		public CopyAttributesFromAttribute(Type sourceType)
		{
			SourceType = sourceType;
		}
	}
}
