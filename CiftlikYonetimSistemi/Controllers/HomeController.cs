using CiftlikYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;

namespace CiftlikYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IDistributedCache _cache;

		public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
			_cache = cache;
        }

		public async Task<IActionResult> Index()
		{
			// Cache'den bir deðer okuma
			string value = await _cache.GetStringAsync("myKey");
			if (value == null)
			{
				value = "This was fetched from the database and then stored in Redis.";

				// Cache'e bir deðer yazma
				await _cache.SetStringAsync("myKey", value, new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // süre sonu
				});
			}

			return View(model: value);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
