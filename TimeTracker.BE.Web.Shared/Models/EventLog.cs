using TimeTracker.BE.Web.Shared.Helpers;

namespace TimeTracker.BE.Web.Shared.Models
{
	public class EventLog
	{
		private static string _appVersion = VersionSWHelper.GetVersionSW();
		public string ClassName { get; private set; } = string.Empty;
		public string MethodName { get; private set; } = string.Empty;
		public string Version { get; private set; } = _appVersion;
		public Guid GuidId { get; set; }
		public string Message { get; set; } = string.Empty;
		public DateTime? BuildDate { get; private set; } = new BuildInfo().BuildDate;
		private Exception? _exception;
		public Exception? Exception
		{
			get => _exception;
			set
			{
				if (_exception != value)
				{
					_exception = value;
					ClassName = _exception?.TargetSite?.DeclaringType?.Name ?? string.Empty;
					MethodName = _exception?.TargetSite?.Name ?? string.Empty;
				}
			}
		}
	}
}