using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Puc.Diario.Infra;
using Puc.SeuEmbarque.Domain.Models.Usuario;
using Puc.SeuEmbarque.Domain.ObjValor;
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
        private readonly IContractorResult _contractor;

        public UsuarioService(IContractorResult contractor)
        {
            _contractor = contractor;
        }
        public IContractorResult AutenticarUsuario(LoginVM usuario)
        {
            var result = _contractor;
            result.AcaoValida = Utils.ValidarEmail(usuario.Email);
            //colocar ação valida no if
            if (usuario.Email == "gugaalves@hotmail.com" && usuario.Senha == "123")
            {
                //var usuarios = ListarUsuariosAtivos().FirstOrDefault(x => x.Email == usuario.Email);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Email),
                    new Claim("OutrasProps", "RoleExemplo")
                };
                ClaimsIdentity claimsIdentity= new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = usuario.ManterLogado,
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

        public bool Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> ListarUsuariosAtivos()
        {
            throw new NotImplementedException();
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
