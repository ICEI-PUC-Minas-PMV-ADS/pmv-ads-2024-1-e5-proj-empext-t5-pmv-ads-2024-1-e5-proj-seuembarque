using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Puc.Diario.Infra;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Services.Interface;
using Puc.SeuEmbarque.Services.Services;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    public class PacotesController : Controller
    {
        private readonly IPacoteService _pacoteService;
        private readonly IClienteService _clienteService;
        public PacotesController(IPacoteService pacoteService, IClienteService clienteService)
        {
            _pacoteService = pacoteService;
            _clienteService = clienteService;
        }
        // CRIAR UM DROPDOWN DE CLIENTES NA HORA DE INSERIR MANUALMENTE UM CLIENTE PARA VINCULAR O ID E AI SIM INSERIR UM PACOTE
        // GET: PacotesController
        public ActionResult Pacotes()
        {
            return View("pacotes_lst");
        }

        public async Task<ActionResult> Register(int idPacote = 0, bool flEdit = true)
        {
            try
            {
                var pacote = await _pacoteService.GetPacotePorId(idPacote);
                var clientes = await _clienteService.ListarTodosClientes();

                ViewBag.flEdit = flEdit;
                ViewBag.Classe = new SelectList(Utils.ComboClasse(), "Value", "Text");
                ViewBag.Hospedagem = new SelectList(Utils.ComboHospedagem(), "Value", "Text");
                ViewBag.Opcionais = new SelectList(Utils.ComboOpcionais(), "Value", "Text");
                ViewBag.Clientes = clientes;

                return View("pacotes_reg", pacote);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("/Pacotes/DeletarPacote")]
        public async Task<IActionResult> DeletarPacote(int idPacote)
        {
            bool isSuccess = await _pacoteService.DeletarPacote(idPacote);
            return Ok(new { data = isSuccess });
        }

        [HttpGet("/Pacotes/GetListaPacotes")]
        public async Task<IActionResult> GetListaPacotes()
        {
            List<PacoteDto> pacotes = await _pacoteService.Buscar();
            return Ok(new { data = pacotes });
        }

        [HttpPost("/Pacotes/InserirPacote")]
        public async Task<IActionResult> InserirPacote(Pacote pacote)
        {
            var result = await _pacoteService.InserirPacote(pacote);

            return Ok(new { data = result != null ? result : new PacoteData() });
        }

        [HttpPost("/Pacotes/AtualizarPacote")]
        public async Task<IActionResult> AtualizarPacote(PacoteData pacote)
        {
            var result = await _pacoteService.AtualizarPacote(pacote);

            return Ok(new { data = result != null ? result : new PacoteData() });
        }
    }
}
