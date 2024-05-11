using Newtonsoft.Json;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using Puc.SeuEmbarque.Domain.Models.Skyscanner;
using Puc.SeuEmbarque.Domain.Models.Usuario;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HttpClient _usuariosApiHttpClient;

        public UsuarioRepository(IHttpClientFactory httpClientFactory)
        {
            _usuariosApiHttpClient = httpClientFactory.CreateClient("UsuariosApi");
        }

        public async Task<UsuarioReg> AtualizarUsuario(Usuario usuario)
        {
            try
            {

                var json = JsonConvert.SerializeObject(usuario);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _usuariosApiHttpClient.PutAsync(_usuariosApiHttpClient.BaseAddress, data);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UsuarioRequest>(responseContent);
                    var retorno = result.data;
                    return retorno;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new UsuarioReg();
            }
        }

        public async Task<List<Usuario>> BuscarUsuario()
        {
            try
            {
                HttpResponseMessage response = await _usuariosApiHttpClient.GetAsync(_usuariosApiHttpClient.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UsuarioList>(responseContent);
                    return result.data;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ConfigurarContato()
        {
            var backup = "31992017965";
            try
            {
                HttpResponseMessage response = await _usuariosApiHttpClient.GetAsync(_usuariosApiHttpClient.BaseAddress + "/master");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UsuarioList>(responseContent);
                    var number = result.data.Select(x => x.cellphone).FirstOrDefault();
                    return number;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return backup;
            }
        }

        public async Task<bool> Deletar(int idUser)
        {
            try
            {

                var response = await _usuariosApiHttpClient.DeleteAsync(_usuariosApiHttpClient.BaseAddress + $"/{idUser}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<UsuarioReg> GetUsuarioPorId(int idUsuario)
        {
            try
            {
                var response = await _usuariosApiHttpClient.GetAsync(_usuariosApiHttpClient.BaseAddress + $"/{idUsuario}" );

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UsuarioRequest>(responseContent);
                    return result.data;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new UsuarioReg();
            }
        }

        public async Task<UsuarioReg> InserirUsuario(UsuarioReg usuario)
        {
            try
            {
                var json = JsonConvert.SerializeObject(usuario);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _usuariosApiHttpClient.PostAsync(_usuariosApiHttpClient.BaseAddress, data);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UsuarioRequest>(responseContent);
                    return result.data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Usuario>> ListarTodos()
        {
            try
            {
                var response = await _usuariosApiHttpClient.GetAsync(_usuariosApiHttpClient.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UsuarioList>(responseContent);
                    return result.data;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new List<Usuario>();
            }
        }

        public async Task<UsuarioRequestLogin> VerificarUsuarioLogin(Credenciais credenciais)
        {
            try
            {
                var json = JsonConvert.SerializeObject(credenciais);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _usuariosApiHttpClient.PostAsync(_usuariosApiHttpClient.BaseAddress + "/login", data);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UsuarioRequestLogin>(responseContent);
                    return result;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new UsuarioRequestLogin();
            }
        }
    }
}
