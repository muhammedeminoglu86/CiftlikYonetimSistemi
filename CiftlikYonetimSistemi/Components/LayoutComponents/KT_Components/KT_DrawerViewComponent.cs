using Microsoft.AspNetCore.Mvc;

namespace CiftlikYonetimSistemi.Components.LayoutComponents.KT_Components
{
	public class KT_DrawerViewComponent :ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("KT_Drawer");
		}
	}
}
