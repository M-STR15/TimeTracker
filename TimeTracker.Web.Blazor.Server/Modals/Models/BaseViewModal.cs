using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace TimeTracker.Web.Blazor.Server.Modals.Models
{
	public abstract class BaseViewModal : ComponentBase
	{
		[Inject]
		protected HttpClient? _httpClient { get; set; }

		[Inject]
		protected IHttpClientFactory _httpClientFactory { get; set; }

		private bool _visible;
		[Parameter, EditorRequired]
		public virtual bool Visible
		{
			get => _visible;
			set
			{
				if (_visible != value)
				{
					_visible = value;
					if (VisibleChanged.HasDelegate)
						VisibleChanged.InvokeAsync(Visible).Wait();
				}
			}
		}
		[Parameter, EditorRequired]
		public virtual EventCallback<bool> VisibleChanged { get; set; }
		[Parameter, EditorRequired]
		public virtual string Title { get; set; } = string.Empty;

		[Parameter]
		public virtual EventCallback OnModalClosed { get; set; }  // Událost, kterou spustíme při zavření modálu
	}
}
