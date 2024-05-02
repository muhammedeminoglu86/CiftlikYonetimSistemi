using Microsoft.AspNetCore.Mvc;

namespace CiftlikYonetimSistemi.Components.LayoutComponents.KT_Components
{
	public class KT_ShoppingViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("KT_Shopping");
		}
	}
}
