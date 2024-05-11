using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puc.Diario.Infra;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using Puc.SeuEmbarque.Services.Interface;
using Puc.SeuEmbarque.Services.Services;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        public ActionResult Clientes()
        {
            return View("clientes_lst");
        }

        public async Task<ActionResult> Register(int idCliente = 0, bool flEdit = true)
        {
            try
            {
                var cliente = await _clienteService.GetClientePorId(idCliente);

                ViewBag.flEdit = flEdit;             

                return View("clientes_reg", cliente);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("/Clientes/DeletarCliente")]
        public async Task<IActionResult> DeletarCliente(int idCliente)
        {
            bool isSuccess = await _clienteService.DeletarCliente(idCliente);
            return Ok(new { data = isSuccess });
        }

        [HttpGet("/Clientes/GetListaClientes")]
        public async Task<IActionResult> GetListaClientes()
        {
            List<ClienteDataDto> clientes = await _clienteService.Buscar();
            return Ok(new { data = clientes });
        }

        [HttpPost("/Clientes/InserirCliente")]
        public async Task<IActionResult> InserirCliente(Cliente cliente)
        {           

           var result = await _clienteService.InserirCliente(cliente);

            return Ok(new { content = result != null ? result : new ClienteData() });
        }

        [HttpPost("/Clientes/AtualizarCliente")]
        public async Task<IActionResult> AtualizarCliente(ClienteDataDto cliente)
        {
            var result = await _clienteService.AtualizarCliente(cliente);

            return Ok(new { data = result != null ? result : new ClienteData() });
        }
    }
}
