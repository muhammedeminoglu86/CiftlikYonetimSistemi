using Microsoft.AspNetCore.Mvc;

namespace CiftlikYonetimSistemi.Components.LayoutComponents
{
	public class ThemeModeViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("ThemeMode");
		}
	}
}
