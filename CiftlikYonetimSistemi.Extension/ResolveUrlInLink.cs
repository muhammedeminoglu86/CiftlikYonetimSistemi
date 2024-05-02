using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Extension
{
	public class ResolveUrlInLinkExtension
	{
		public string ResolveUrlInLink(string link, IUrlHelper Url)
		{
			// This pattern looks for 'href=\"~/' and captures the path after '~/'
			var pattern = "href=\\\"~/(.*?)\\\"";
			var replacement = $"href=\"{Url.Content("~/")}$1\"";
			var resolvedLink = Regex.Replace(link, pattern, replacement);

			return resolvedLink;
		}
	}
}
