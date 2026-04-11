namespace TimeTracker.BE.Web.Shared.Services
{
	/// <summary>
	/// Rozhraní pro službu protokolování událostí.
	/// </summary>
	public interface IEventLogService
	{
		/// <summary>
		/// Zaloguje chybovou událost.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="message">Volitelná zpráva s popisem chyby.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		void LogError(Guid guidId, Exception exception, string? message = null, object? inputObject = null);

		/// <summary>
		/// Asynchronně zaloguje chybovou událost.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		Task LogErrorAsync(Guid guidId, Exception exception, object? inputObject = null);

		/// <summary>
		/// Asynchronně zaloguje chybovou událost s volitelnou zprávou.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="message">Zpráva s popisem chyby.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		Task LogErrorAsync(Guid guidId, Exception exception, string? message, object? inputObject = null);

		/// <summary>
		/// Zaloguje informační událost.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="message">Volitelná zpráva s popisem informace.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		void LogInformation(Guid guidId, Exception exception, string? message = null, object? inputObject = null);

		/// <summary>
		/// Asynchronně zaloguje informační událost.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="message">Volitelná zpráva s popisem informace.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		Task LogInformationAsync(Guid guidId, string? message = null, object? inputObject = null);

		/// <summary>
		/// Asynchronně zaloguje informační událost s výjimkou.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		Task LogInformationAsync(Guid guidId, Exception exception, object? inputObject = null);

		/// <summary>
		/// Asynchronně zaloguje informační událost s výjimkou a volitelnou zprávou.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="message">Zpráva s popisem informace.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		Task LogInformationAsync(Guid guidId, Exception exception, string? message, object? inputObject = null);

		/// <summary>
		/// Zaloguje varovnou událost.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="message">Volitelná zpráva s popisem varování.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		void LogWarning(Guid guidId, Exception exception, string? message = null, object? inputObject = null);

		/// <summary>
		/// Asynchronně zaloguje varovnou událost.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		Task LogWarningAsync(Guid guidId, Exception exception, object? inputObject = null);

		/// <summary>
		/// Asynchronně zaloguje varovnou událost s volitelnou zprávou.
		/// </summary>
		/// <param name="guidId">Jedinečný identifikátor události.</param>
		/// <param name="exception">Výjimka, která nastala.</param>
		/// <param name="message">Zpráva s popisem varování.</param>
		/// <param name="inputObject">Volitelný vstupní objekt pro další kontext.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		Task LogWarningAsync(Guid guidId, Exception exception, string? message, object? inputObject = null);
	}
}