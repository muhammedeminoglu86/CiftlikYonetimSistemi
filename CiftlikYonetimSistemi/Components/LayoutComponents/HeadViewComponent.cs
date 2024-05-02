using CiftlikYonetimSistemi.Business.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using CiftlikYonetimSistemi.Extension;

public class HeadViewComponent : ViewComponent
{
	private readonly IHeadService _headService;
	private readonly ResolveUrlInLinkExtension _resolveUrlInLinkExtension;

	public HeadViewComponent(IHeadService headService, ResolveUrlInLinkExtension resolveUrlInLinkExtension)
	{
		_headService = headService;
		_resolveUrlInLinkExtension = resolveUrlInLinkExtension;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var controllerName = ViewContext.RouteData.Values["controller"]?.ToString();
		var actionName = ViewContext.RouteData.Values["action"]?.ToString();

		var head = await _headService.GetOne("select * from Head where controllername = @controllername and actionname = @actionname and isactive = 1", new { controllerName, actionName });

		// Assuming head.HeadValues is a collection of strings containing your HTML link elements
		var resolvedHeadValues = head.HeadValues.Select(link => _resolveUrlInLinkExtension.ResolveUrlInLink(link.Value, this.Url)).ToList();

		// Add resolvedHeadValues to the view's model or ViewBag/ViewData as needed
		ViewBag.ResolvedHeadValues = resolvedHeadValues;

		return View("Head", head);
	}

}
