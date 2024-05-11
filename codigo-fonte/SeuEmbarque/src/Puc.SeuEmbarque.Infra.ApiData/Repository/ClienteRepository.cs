using Newtonsoft.Json;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly HttpClient _clientesApiHttpClient;
        public ClienteRepository(IHttpClientFactory httpClientFactory)
        {
            _clientesApiHttpClient = httpClientFactory.CreateClient("ClientesApi");
        }

        public async Task<ClienteData> AtualizarCliente(ClienteDataDto pacote)
        {
            try
            {

                var json = JsonConvert.SerializeObject(pacote);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _clientesApiHttpClient.PutAsync(_clientesApiHttpClient.BaseAddress, data);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ClienteRequest>(responseContent);
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
                return new ClienteData();
            }
        }

        public async Task<bool> Deletar(int idCliente)
        {
            try
            {

                var response = await _clientesApiHttpClient.DeleteAsync(_clientesApiHttpClient.BaseAddress + $"/{idCliente}");

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

        public async Task<ClienteData> GetClientePorId(int idCliente)
        {
            try
            {

                var response = await _clientesApiHttpClient.GetAsync(_clientesApiHttpClient.BaseAddress + $"/{idCliente}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ClienteRequest>(responseContent);
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

        public async Task<ClienteData> InserirCliente(Cliente cliente)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                };

                var json = JsonConvert.SerializeObject(cliente);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _clientesApiHttpClient.PostAsync(_clientesApiHttpClient.BaseAddress, data);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ClienteRequest>(responseContent);
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

        public async Task<List<ClienteData>> ListarTodos()
        {
            try
            {
                var response = await _clientesApiHttpClient.GetAsync(_clientesApiHttpClient.BaseAddress + "s");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ClienteLista>(responseContent);
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
    }
}
