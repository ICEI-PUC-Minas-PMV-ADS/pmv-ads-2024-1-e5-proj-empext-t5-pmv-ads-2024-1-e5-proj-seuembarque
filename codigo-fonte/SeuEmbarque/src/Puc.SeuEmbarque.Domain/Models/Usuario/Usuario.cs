using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Usuario
{
    public class Usuario
    {
        public string cellphone { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public int user_id { get; set; }
        public bool? master_user { get; set; }
    }
    public class UsuarioReg
    {
        public int user_id { get; set; }
        public string cellphone { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string? password { get; set; }
        public bool? master_user { get; set; }
    }


    public class UsuarioList
    {
        public List<Usuario> data { get; set; }
        public string message { get; set; }
    }

    public class UsuarioRequest
    {
        public UsuarioReg data { get; set; }
        public string message { get; set; }
    }

    public class UsuarioRequestLogin
    {
        public Usuario data { get; set; }
        public string message { get; set; }
        public bool flag { get; set; }
    }
}
