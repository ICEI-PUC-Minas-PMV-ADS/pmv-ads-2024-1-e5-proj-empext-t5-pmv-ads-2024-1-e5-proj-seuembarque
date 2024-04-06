using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Cliente
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public int Email { get; set; }
        public int Cpf { get; set; }
    }
}
