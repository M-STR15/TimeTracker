using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System.Configuration;

namespace TimeTracker.BE.Web.Shared.Services
{
	public partial class EventLogService : IEventLogService
	{
		public EventLogService()
		{
			var outputTempleteFormat = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
			var directory = Path.GetDirectoryName(config.FilePath);
			var logFilePath = Path.Combine(directory, "Loging", "log-.json");

#if DEBUG
			Log.Logger = new LoggerConfiguration()
						.WriteTo.Console(outputTemplate: outputTempleteFormat)
						.WriteTo.File(new JsonFormatter(), logFilePath, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug)
						.CreateLogger();
#else
			Log.Logger = new LoggerConfiguration()
						.WriteTo.Console(outputTemplate: outputTempleteFormat)
						.WriteTo.File(new JsonFormatter(), logFilePath, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)
						.CreateLogger();
#endif
		}


		public void LogFatal(Guid guidId, Exception exception, string? message = null, object? inputObject = null)
		{
			try
			{
				if (inputObject != null)
					exception.Data.Add("InputObject", inputObject);

				var eventLog = getEventLog(guidId, exception, message);
				Log.Fatal("{@EventLog}", eventLog);
			}
			catch (Exception)
			{

			}
		}


		/// <summary>
		/// Loguje chybu
		/// </summary>
		/// <param name="guidId">Unikátní označení, pod kterým bude evidovaná daná událost.</param>
		/// <param name="message">Zpráva k dané události.</param>
		public void LogError(Guid guidId, Exception exception, string? message = null, object? inputObject = null)
		{
			try
			{
				if (inputObject != null)
					exception.Data.Add("InputObject", inputObject);

				var json = getJson(guidId, exception, message);
				Log.Error("EventLog: {Json}", json);
			}
			catch (Exception)
			{

			}
		}

		private string? getJson(Guid guidId, Exception? exception, string? message = null)
		{
			var eventLog = getEventLog(guidId, exception, message);
			return JsonConvert.SerializeObject(eventLog, new JsonSerializerSettings
			{
				MaxDepth = 1,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}

		/// <summary>
		/// Loguje varování
		/// </summary>
		/// <param name="guidId">Unikátní označení, pod kterým bude evidovaná daná událost.</param>
		/// <param name="message">Zpráva k dané události.</param>
		public void LogWarning(Guid guidId, Exception exception, string? message = null, object? inputObject = null)
		{
			try
			{
				if (inputObject != null)
					exception.Data.Add("InputObject", inputObject);

				var json = getJson(guidId, exception, message);
				Log.Warning("EventLog: {Json}", json);
			}
			catch (Exception)
			{

			}
		}

		/// <summary>
		/// Loguje informaci
		/// </summary>
		/// <param name="guidId">Unikátní označení, pod kterým bude evidovaná daná událost.</param>
		/// <param name="message">Zpráva k dané události.</param>
		public void LogInformation(Guid guidId, Exception? exception, string? message = null, object? inputObject = null)
		{
			try
			{
				if (inputObject != null && exception != null)
					exception.Data.Add("InputObject", inputObject);

				var json = getJson(guidId, exception, message);
				Log.Information("EventLog: {Json}", json);
			}
			catch (Exception)
			{

			}
		}

		public void LogDebug(Guid guidId, Exception exception, string? message = null, object? inputObject = null)
		{
			try
			{
				if (inputObject != null)
					exception.Data.Add("InputObject", inputObject);

				var json = getJson(guidId, exception, message);
				Log.Debug("EventLog: {Json}", json);
			}
			catch (Exception)
			{

			}
		}

		private Models.EventLog getEventLog(Guid guidId, Exception? exception, string? message = null)
		{
			var messageData = string.IsNullOrEmpty(message) ? (exception?.Message ?? "") : message;
			var eventData = new Models.EventLog
			{
				GuidId = guidId,
				Message = messageData,
				Exception = exception,
			};

			return eventData;
		}
	}
}
