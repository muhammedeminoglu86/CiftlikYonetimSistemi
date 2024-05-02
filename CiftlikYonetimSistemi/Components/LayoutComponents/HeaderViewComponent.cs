using CiftlikYonetimSistemi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CiftlikYonetimSistemi.Components.LayoutComponents
{
	public class HeaderViewComponent : ViewComponent
	{

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("Header");
		}
	}
}
