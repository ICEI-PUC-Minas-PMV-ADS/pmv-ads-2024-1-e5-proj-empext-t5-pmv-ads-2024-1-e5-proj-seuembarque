using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puc.SeuEmbarque.Services.Interface;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    public class FormularioController : Controller
    {
        private readonly IAeroportoService _aeroportoService;
        private readonly IPacoteService _pacoteService;
        private readonly IClienteService _clienteService;
        private readonly IUsuarioService _usuarioService;
        public FormularioController(IAeroportoService aeroportoService, IUsuarioService usuarioService)
        {
            _aeroportoService = aeroportoService;
            _usuarioService = usuarioService;
        }
        // GET: FormularioController
        public async Task<ActionResult> Formulario()
        {
            var config = await _usuarioService.MudarContato();
            ViewBag.ConfigWhatsApp = config;
            return View("formulario_reg");
        }

        [HttpGet("/Aeroporto/PesquisarAeroporto")]
        public async Task<IActionResult> PesquisarAeroporto(string termo)
        {
            var list = await _aeroportoService.GetAeroportos(termo);

            return Ok(new { data = list });
        }      

    }
}
