using Puc.SeuEmbarque.Domain.Models;
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
        Task<IUserContractorResult> AutenticarUsuario(LoginVM usuario);
        IEnumerable<Usuario> ListarUsuariosAtivos();
        Task<string> MudarContato();
        Task<UsuarioReg> GetUsuarioPorId(int idUsuario);
        Task<List<Usuario>> ListarTodos();
        Task<List<Usuario>> Buscar();
        Task<UsuarioReg> InserirUsuario(UsuarioReg usuario);
        Task<UsuarioReg> AtualizarUsuario(Usuario usuario);
        Task<bool> DeletarUsuario(int idUsuario);
    }
}
