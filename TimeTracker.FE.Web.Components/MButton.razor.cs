using Microsoft.AspNetCore.Components;

namespace TimeTracker.FE.Web.Components
{
	public partial class MButton
	{
		[Parameter]
		public string Text { get; set; } = string.Empty;

		[Parameter]
		public EventCallback OnClick { get; set; }

		[Parameter]
		public string Src { get; set; } = "/Resources/Icons/SVG/shopping-cart.svg";

		[Parameter]
		public string Style { get; set; } = string.Empty;

		[Parameter]
		public string Class { get; set; } = string.Empty;

		[Parameter]
		public bool IsDisabled { get; set; } = false;

		[Parameter]
		public bool IsVisibleNotification { get; set; } = false;

		[Parameter]
		public int NumberNotification { get; set; } = 0;

		private string _disabledStyle => IsDisabled ? "pointer-events: none; opacity: 0.5;" : "";
		[Parameter] 
		public RenderFragment? ChildContent { get; set; }

		private EventCallback getOnClickCallback()
		{
			return IsDisabled ? EventCallback.Empty : OnClick;
		}
	}
}
