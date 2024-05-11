using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using Puc.SeuEmbarque.Domain.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Interface
{
    public interface IUsuarioRepository
    {
        Task<string> ConfigurarContato();
        Task<List<Usuario>> BuscarUsuario();
        Task<UsuarioReg> GetUsuarioPorId(int idUsuario);
        Task<List<Usuario>> ListarTodos();
        Task<UsuarioReg> InserirUsuario(UsuarioReg usuario);
        Task<UsuarioReg> AtualizarUsuario(Usuario usuario);
        Task<UsuarioRequestLogin> VerificarUsuarioLogin(Credenciais credenciais);
        Task<bool> Deletar(int idUser);
    }
}
