using CiftlikYonetimSistemi.Business.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class JSViewComponent : ViewComponent
{
	private readonly IJavascriptService _javascriptService;
	private readonly ResolveUrlInLinkExtension _resolveUrlInLinkExtension;

	public JSViewComponent(IJavascriptService javascriptService, ResolveUrlInLinkExtension resolveUrlInLinkExtension)
	{
		_javascriptService = javascriptService;
		_resolveUrlInLinkExtension = resolveUrlInLinkExtension;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var controllerName = ViewContext.RouteData.Values["controller"]?.ToString();
		var actionName = ViewContext.RouteData.Values["action"]?.ToString();

		// Assuming 'Value' is a string that directly represents JavaScript code or reference
		var javascripts = await _javascriptService.GetAllAsync(
			"SELECT * FROM Javascript WHERE controllername = @controllername AND actionname = @actionname AND isactive = 1 order by ordernumber",
			new { controllerName, actionName });

		//var scriptValues = javascripts.Select(js => js.Value).ToList();
		var scriptValues = javascripts.Select(link => _resolveUrlInLinkExtension.ResolveUrlInLink(link.Value, this.Url)).ToList();

		// You could further process these 'Value' fields here if needed

		ViewBag.ScriptValues = scriptValues;

		return View("JS", scriptValues); // Assuming you have a view named 'Javascript' that expects a list of script values
	}

	private string ResolveUrlInLink(string link, IUrlHelper Url)
	{
		// This pattern looks for 'href=\"~/' and captures the path after '~/'
		var pattern = "href=\\\"~/(.*?)\\\"";
		var replacement = $"href=\"{Url.Content("~/")}$1\"";
		var resolvedLink = Regex.Replace(link, pattern, replacement);

		return resolvedLink;
	}
}
