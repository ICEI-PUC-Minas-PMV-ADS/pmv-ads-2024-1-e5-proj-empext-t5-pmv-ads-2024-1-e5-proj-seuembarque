using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Services.Interface;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("Painel")]
        public IActionResult Dashboard()
        {
            return View("dashboard_home");
        }


        [HttpGet("/Dashboard/GetDadosDestinos")]
        public async Task<IActionResult> GetDadosDestinos()
        {
            var destinos = await _dashboardService.GraficoDestinos();
            return Ok(new { data = destinos });
        }

        [HttpGet("/Dashboard/GetDadosClientes")]
        public async Task<IActionResult> GetDadosClientes()
        {
            var clientes = await _dashboardService.GraficoClientes();
            return Ok(new { data = clientes });
        }

        [HttpGet("/Dashboard/GetDadosFaturamento")]
        public async Task<IActionResult> GetDadosFaturamento()
        {
            try
            {
                var faturamento = await _dashboardService.GraficoFaturamento();
                return Ok(new { data = faturamento });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
            
        }

    }
}
