using Puc.SeuEmbarque.Domain.Models.Usuario;
using Puc.SeuEmbarque.Domain.ObjValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Interface
{
    public interface IUsuarioService
    {
        IContractorResult AutenticarUsuario(LoginVM usuario);
        bool RegistrarUsuario(Usuario usuario);
        bool Deletar(int id);
        IEnumerable<Usuario> ListarUsuariosAtivos();
    }
}
