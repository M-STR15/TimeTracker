using Microsoft.AspNetCore.Components;

namespace TimeTracker.Web.Blazor.Server.Modals.Models
{
	public abstract class BaseViewModal : ComponentBase
	{
		[Inject]
		protected HttpClient? _httpClient { get; set; }
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

		protected abstract void save_Click();
	}
}
