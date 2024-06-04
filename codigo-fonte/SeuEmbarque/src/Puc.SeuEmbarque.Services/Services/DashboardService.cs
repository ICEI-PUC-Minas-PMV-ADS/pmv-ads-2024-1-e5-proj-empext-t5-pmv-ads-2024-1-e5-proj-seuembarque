using Puc.SeuEmbarque.Domain.Models.Dashboard;
using Puc.SeuEmbarque.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IPacoteService _pacoteService;
        private readonly IClienteService _clienteService;
        public DashboardService(IPacoteService pacoteService, IClienteService clienteService)
        {
            _pacoteService = pacoteService;
            _clienteService = clienteService;
        }
        public async Task<List<DadosGrafico>> GraficoClientes()
        {
            try
            {
                var listaPacotes = await _pacoteService.ListarTodos();
                var listaClientes = await _clienteService.ListarTodosClientes();

                var dadosClientes = listaPacotes
                 .GroupBy(p => p.client_id)
                 .Select(g => new DadosGrafico
                 {
                     Label = listaClientes.FirstOrDefault(c => c.client_id == g.Key)?.name ?? "",
                     Valor = g.Count()
                 })
                 .OrderBy(d => d.Valor)
                 .TakeLast(5)
                 .ToList();

                return dadosClientes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DadosGraficoFaturamento>> GraficoFaturamento()
        {
            try
            {
                var lista = await _pacoteService.ListarTodos();
                var listaAnoCorrente = lista.Where(p => DateTime.ParseExact(p.registration_date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Year == DateTime.Now.Year);

                Dictionary<int, (float total, int count)> faturamentoPorMes = new Dictionary<int, (float, int)>();

                for (int i = 1; i <= 12; i++)
                {
                    faturamentoPorMes[i] = (0, 0);
                }

                // calcular faturamento de cada mes
                foreach (var pacote in listaAnoCorrente)
                {
                    var date = DateTime.ParseExact(pacote.registration_date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    int mes = date.Month;

                    // adicionar o preço do pacote ao total do mês correspondente
                    faturamentoPorMes[mes] = (faturamentoPorMes[mes].total + pacote.price, faturamentoPorMes[mes].count + 1);
                }

                // dicionario em lista
                var dados = faturamentoPorMes.Select(x =>
                {
                    float media = x.Value.count == 0 ? 0 : x.Value.total / x.Value.count;
                    return new DadosGraficoFaturamento
                    {
                        Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key),
                        Faturamento = x.Value.total,
                        MediaPreco = media
                    };
                }).ToList();

                return dados;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<DadosGrafico>> GraficoDestinos()
        {
            try
            {
                var lista = await _pacoteService.ListarTodos();
                var destinos = lista
                    .GroupBy(d => d.destination)
                    .Select(x => new DadosGrafico { Label = x.Key, Valor = x.Count() })
                    .OrderByDescending(v => v.Valor)
                    .Take(5)
                    .ToList();

                return destinos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
