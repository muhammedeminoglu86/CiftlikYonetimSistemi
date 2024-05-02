using Microsoft.AspNetCore.Mvc;

namespace CiftlikYonetimSistemi.Components.LayoutComponents
{
	public class SideBarViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("SideBar");
		}
	}
}
