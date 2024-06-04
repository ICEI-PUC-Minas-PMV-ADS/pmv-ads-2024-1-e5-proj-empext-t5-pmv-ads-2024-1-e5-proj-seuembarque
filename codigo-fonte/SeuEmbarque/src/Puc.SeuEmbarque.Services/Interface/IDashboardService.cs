using Puc.SeuEmbarque.Domain.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Interface
{
    public interface IDashboardService
    {
        //Task<List<DadosGrafico>> ObterDadosGraficoAsync();
        Task<List<DadosGrafico>> GraficoDestinos();
        Task<List<DadosGrafico>> GraficoClientes();
        Task<List<DadosGraficoFaturamento>> GraficoFaturamento();
    }
}
