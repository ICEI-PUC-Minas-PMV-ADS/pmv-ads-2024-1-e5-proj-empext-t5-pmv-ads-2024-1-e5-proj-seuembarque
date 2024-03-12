using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models
{
    public class Pacote
    {
        [JsonProperty("sobrenome")]
        public int IdPacote { get; set; }
        public int IdCliente { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime DataIda { get; set; }
        public DateTime DataVolta { get; set; }
        public DateTime DataRegistro { get; set; }
        public string ValorOrc { get; set; }
    }
}
