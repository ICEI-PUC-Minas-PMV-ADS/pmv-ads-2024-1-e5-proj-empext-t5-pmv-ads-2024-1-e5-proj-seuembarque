using Microsoft.AspNetCore.Mvc;
using Puc.SeuEmbarque.Presentation.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Puc.SeuEmbarque.Services.Interface;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Services.Services;
using Puc.SeuEmbarque.Domain.Models.Usuario;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using Puc.Diario.Infra;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    [Authorize]
    public class PainelController : Controller
    {
        private readonly ILogger<PainelController> _logger;
        private readonly IUsuarioService _usuarioService;

        public PainelController(ILogger<PainelController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public IActionResult Painel()
        {
            return View("painel_home");
        }

        public IActionResult Usuarios()
        {
            return View("usuario_lst");
        }

        public async Task<ActionResult> Register(int idUsuario = 0, bool flEdit = true)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioPorId(idUsuario);
                var isNew = (usuario.user_id == 0) ? true : false;
                ViewBag.flEdit = flEdit;
                ViewBag.isNew = isNew;
                ViewBag.MasterUser = new SelectList(Utils.ComboMasterUser(), "Value", "Text");

                return View("usuario_reg", usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("/Painel/DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuario(int idUser)
        {
            bool isSuccess = await _usuarioService.DeletarUsuario(idUser);

            return Ok(new { data = isSuccess });
        }

        [HttpPost("/Painel/InserirUsuario")]
        public async Task<IActionResult> InserirUsuario(UsuarioReg usuario)
        {
            var result = await _usuarioService.InserirUsuario(usuario);

            return Ok(new { content = result != null ? result : new UsuarioReg() });
        }

        [HttpPost("/Painel/AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuario(Usuario usuario)
        {
            var result = await _usuarioService.AtualizarUsuario(usuario);

            return Ok(new { data = result != null ? result : new UsuarioReg() });
        }

        [HttpGet("/Painel/GetListaUsuarios")]
        public async Task<IActionResult> GetListaPacotes()
        {
            List<Usuario> usuarios = await _usuarioService.Buscar();
            return Ok(new { data = usuarios });
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