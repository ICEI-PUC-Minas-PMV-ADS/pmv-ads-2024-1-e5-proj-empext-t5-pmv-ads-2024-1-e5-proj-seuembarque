using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puc.SeuEmbarque.Services.Interface;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    public class FormularioController : Controller
    {
        private readonly IAeroportoService _aeroportoService;
        public FormularioController(IAeroportoService aeroportoService)
        {
            _aeroportoService = aeroportoService;
        }
        // GET: FormularioController
        public ActionResult Formulario()
        {
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
