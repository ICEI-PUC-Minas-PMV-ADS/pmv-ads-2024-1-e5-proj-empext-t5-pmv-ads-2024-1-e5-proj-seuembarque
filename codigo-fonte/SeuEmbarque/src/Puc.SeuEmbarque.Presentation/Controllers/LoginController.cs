using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Puc.SeuEmbarque.Domain.Models.Usuario;
using Puc.SeuEmbarque.Services.Interface;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Login()
        {
            bool claimUser = HttpContext.User.Identity.IsAuthenticated;

            if (claimUser)
                return RedirectToAction("Painel", "Painel");

            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            return View("login_auth");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var response = await _usuarioService.AutenticarUsuario(login);
            if (response.AcaoValida)
            {

                var claimsIdentity = new ClaimsIdentity(response.ClaimIdentity.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal, (AuthenticationProperties?)response.Properties);

                return RedirectToAction("Painel", "Painel");
            }

            ViewData["ValidateMessage"] = response.Message;
            return View("login_auth");
        }            
      
    }
}
