using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Dashboard
{
    public class DadosGrafico
    {
        public string Label { get; set; }
        public int Valor { get; set; }
    }

    public class DadosGraficoFaturamento
    {
        public string Mes { get; set; }
        public float Faturamento { get; set; }
        public float MediaPreco { get; set; }
    }
}
