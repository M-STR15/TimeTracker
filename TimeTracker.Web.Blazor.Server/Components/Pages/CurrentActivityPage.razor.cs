using System.Net;
using TimeTracker.Basic.Enums;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.FE.Web.Components.Models;
using TimeTracker.Web.Blazor.Server.Models;

namespace TimeTracker.Web.Blazor.Server.Components.Pages
{
	public partial class CurrentActivityPage
	{
		private bool _isOpenAddActivityModal;
		private RecordListViewModel? _lastRecordActivity;
		private TotalTimesViewModel? _totalTime;
		private HttpStatusCode _responsevLastRecordActivityStatusCode;
		private bool _isDisableButtonPause => _lastRecordActivity == null || _lastRecordActivity?.ActivityId == (int)eActivity.Stop || _isPosting;
		private bool _isDisableButtonStop => _lastRecordActivity == null || _lastRecordActivity?.ActivityId == (int)eActivity.Stop || _isPosting;

		private bool _isPosting = false;
		protected async override Task OnInitializedAsync()
		{
			await loadDataAsync();
			await base.OnInitializedAsync();
		}

		private void onOpenAddActvityModal() => _isOpenAddActivityModal = true;
		private void onChangeActivityOnPause() => postApi_EndShiftOrPause("api/v1/record-activity/pause");
		private void onChangeActivityOnEndShift() => postApi_EndShiftOrPause("api/v1/record-activity/end-shift");

		private async void readRequestRecordActivityDetailDto(HttpResponseMessage httpResponseMessage)
		{
			var recordActivityBaseDto = await httpResponseMessage.Content.ReadFromJsonAsync<RecordActivityDetailDto>();
			if (recordActivityBaseDto != null)
			{
				changeActivity(recordActivityBaseDto);
				StateHasChanged();
			}
			else
			{
				_toastNotificationService?.AddNotification(eNotificationType.Warning, "Bad Request", "Chyba při načítání požadavku.");
			}
		}


		private async void postApi_EndShiftOrPause(string urlApi)
		{
			_isPosting = true;
			try
			{
				var result = await _httpClient.PostAsync(urlApi, null);

				if (result.StatusCode == HttpStatusCode.OK)
					readRequestRecordActivityDetailDto(result);
				else
					_toastNotificationService?.AddNotification(eNotificationType.Warning, "Bad Request", "Chybný požadavek.");
			}
			catch (Exception ex)
			{
				_eventLogService?.LogError(Guid.Parse("0e50d97b-979e-434d-aa32-76bf0a04c35b"), ex);
				_toastNotificationService?.AddNotification(eNotificationType.Warning, "Bad Request", "Chyba při ukládání dat.");
			}
			finally
			{
				_isPosting = false;
			}
		}

		private void changeActivity(RecordActivityDetailDto recordActivityDetailDto)
		{
			_toastNotificationService?.AddNotification(eNotificationType.Success, "Success", "Data byla upraveny.");
			_lastRecordActivity = _mapper.Map<RecordListViewModel>(recordActivityDetailDto);
			_totalTime?.UpdateActivityId((eActivity)_lastRecordActivity.ActivityId);
		}


		private async Task loadDataAsync()
		{
			try
			{
				if (_httpClient != null)
				{
					var urlApiLastRecordActivity = "/api/v1/last-record-activity";
					var urlApiTotalTime = "/api/v1/reports/total-times";

					var responsevLastRecordActivity = await _httpClient.GetAsync(urlApiLastRecordActivity);
					var responsevApiTotalTime = await _httpClient.GetAsync(urlApiTotalTime);

					if (responsevLastRecordActivity.StatusCode == HttpStatusCode.OK && responsevApiTotalTime.StatusCode == HttpStatusCode.OK)
					{
						// Status 200 OK
						var recordActivityDetailDto = await responsevLastRecordActivity.Content.ReadFromJsonAsync<RecordActivityDetailDto>();
						var totalTimesDto = await responsevApiTotalTime.Content.ReadFromJsonAsync<TotalTimesDto>();
						if (recordActivityDetailDto != null && totalTimesDto != null)
						{
							_totalTime = _mapper.Map<TotalTimesViewModel>(totalTimesDto);
							_lastRecordActivity = _mapper.Map<RecordListViewModel>(recordActivityDetailDto);
						}

						_responsevLastRecordActivityStatusCode = HttpStatusCode.OK;
						_toastNotificationService?.AddNotification(eNotificationType.Success, "Success", "Data byla načtena.");
					}
					else if (responsevLastRecordActivity.StatusCode == HttpStatusCode.NotFound)
					{
						// Status 404 Not Found
						_responsevLastRecordActivityStatusCode = HttpStatusCode.NotFound;
						_toastNotificationService?.AddNotification(eNotificationType.Info, "Not Found", "Data nebyla nalezena.");
					}
					else if (responsevLastRecordActivity.StatusCode == HttpStatusCode.BadRequest)
					{
						_responsevLastRecordActivityStatusCode = HttpStatusCode.BadRequest;
						// Status 400 Bad Request
						_toastNotificationService?.AddNotification(eNotificationType.Warning, "Bad Request", "Chybný požadavek.");
					}
					else
					{
						_responsevLastRecordActivityStatusCode = HttpStatusCode.InternalServerError;
						// Ostatní chyby
						_toastNotificationService?.AddNotification(eNotificationType.Error, "Error", "Chybný požadavek.");
					}
				}

				StateHasChanged();
			}
			catch (Exception ex)
			{
				_eventLogService?.LogError(Guid.Parse("d58ce7fb-9d3e-4947-82a6-346a78d3a4d3"), ex);
			}
		}

		private async void onAfterCloseModalAddActivity_Changed()
		{
			await loadDataAsync();
		}
	}
}
