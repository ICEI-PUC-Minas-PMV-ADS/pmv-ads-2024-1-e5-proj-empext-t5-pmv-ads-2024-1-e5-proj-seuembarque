using Microsoft.AspNetCore.Mvc;
using Puc.SeuEmbarque.Presentation.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    [Authorize]
    public class PainelController : Controller
    {
        private readonly ILogger<PainelController> _logger;

        public PainelController(ILogger<PainelController> logger)
        {
            _logger = logger;
        }

        public IActionResult Painel()
        {
            return View("painel_home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}