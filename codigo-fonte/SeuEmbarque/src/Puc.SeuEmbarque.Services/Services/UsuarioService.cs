using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Puc.Diario.Infra;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using Puc.SeuEmbarque.Domain.Models.Usuario;
using Puc.SeuEmbarque.Domain.ObjValor;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using Puc.SeuEmbarque.Infra.ApiData.Repository;
using Puc.SeuEmbarque.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUserContractorResult _contractor;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUserContractorResult contractor, IUsuarioRepository usuarioRepository)
        {
            _contractor = contractor;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IUserContractorResult> AutenticarUsuario(LoginVM usuario)
        {
            var result = _contractor;
            result.AcaoValida = true;//Utils.ValidarEmail(usuario.Email);

            var usuarioCred = new Credenciais();
            usuarioCred.email = usuario.Email;
            usuarioCred.password = usuario.Senha;

            var user =  await _usuarioRepository.VerificarUsuarioLogin(usuarioCred);
            var userDB = ListarTodos().Result.FirstOrDefault(x => x.email == usuario.Email);

            if (user.flag)
            {              

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Email),
                    new Claim(ClaimTypes.Name, userDB.name),
                    new Claim(ClaimTypes.Email, userDB.email),
                    new Claim("OutrasProps", "RoleExemplo")
                };
                var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = usuario.ManterLogado

                };

                result.Message = "Logado com sucesso!";
                result.ClaimIdentity = claimsIdentity;
                result.Properties = properties;

                return result;
            }
            else
            {
                result.AcaoValida = false;
            }

            result.Message = "Usuário/Senha Inválidos!";

            return result;
        }

        public async Task<string> MudarContato()
        {
            try
            {
                var response = await _usuarioRepository.ConfigurarContato();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<Usuario> ListarUsuariosAtivos()
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioReg> GetUsuarioPorId(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                    return new UsuarioReg();

                var usuario = await _usuarioRepository.GetUsuarioPorId(idUsuario);
                usuario.cellphone = Utils.FormataTelefone(usuario.cellphone);
                return usuario;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Usuario>> ListarTodos()
        {
            try
            {
                var usuarios = await _usuarioRepository.ListarTodos();

                if (usuarios != null)
                {
                    return usuarios;
                }
                else
                {
                    return new List<Usuario>();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Usuario>> Buscar()
        {
            try
            {
                var usuarios = await _usuarioRepository.ListarTodos();

                if (usuarios != null)
                    return usuarios;
                else
                    return new List<Usuario>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsuarioReg> InserirUsuario(UsuarioReg usuario)
        {
            try
            {
                if (usuario != null)
                {
                    var result = await _usuarioRepository.InserirUsuario(usuario);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsuarioReg> AtualizarUsuario(Usuario usuario)
        {
            try
            {
                if (usuario != null)
                {
                    var result = await _usuarioRepository.AtualizarUsuario(usuario);

                    return result;
                }
                else
                {
                    return new UsuarioReg();
                }

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<bool> DeletarUsuario(int idUsuario)
        {
            if(idUsuario != null)
            {
                var result = await _usuarioRepository.Deletar(idUsuario);

                return result;
            }

            return false;
        }
    }
}
