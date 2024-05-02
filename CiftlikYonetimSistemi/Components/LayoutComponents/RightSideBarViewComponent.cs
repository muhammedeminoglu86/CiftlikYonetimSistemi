using Microsoft.AspNetCore.Mvc;

namespace CiftlikYonetimSistemi.Components.LayoutComponents
{
	public class RightSideBarViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("RightSideBar");
		}
	}
}
