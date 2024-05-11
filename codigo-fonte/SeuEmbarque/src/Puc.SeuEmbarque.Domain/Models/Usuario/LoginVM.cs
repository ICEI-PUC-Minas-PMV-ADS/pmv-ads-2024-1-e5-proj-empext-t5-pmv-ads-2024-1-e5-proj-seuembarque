using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Usuario
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool ManterLogado { get; set; }
    }

    public class Credenciais
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
