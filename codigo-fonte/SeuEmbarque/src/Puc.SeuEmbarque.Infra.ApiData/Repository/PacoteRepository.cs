using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Skyscanner;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Repository
{
    public class PacoteRepository : IPacoteRepository
    {
        private readonly HttpClient _pacotesApiHttpClient;
        public PacoteRepository(IHttpClientFactory httpClientFactory)
		{
            _pacotesApiHttpClient = httpClientFactory.CreateClient("PacotesApi");
        }

        public async Task<PacoteData> AtualizarPacote(PacoteUpdateDto pacote)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                };

                var json = JsonConvert.SerializeObject(pacote, settings);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _pacotesApiHttpClient.PutAsync(_pacotesApiHttpClient.BaseAddress + "/pacote", data);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PacoteRequest>(responseContent);
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
                return new PacoteData();
            }
        }

        public async Task<PacoteData> GetPacotePorId(int idPacote)
        {
            try
            {

                var response = await _pacotesApiHttpClient.GetAsync(_pacotesApiHttpClient.BaseAddress + "/pacote/" + idPacote);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PacoteRequest>(responseContent);
                    return result.data;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new PacoteData();
            }
        }

        public async Task<PacoteData> InserirPacote(Pacote pacote)
        {
			try
			{
                var settings = new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                };

                var json = JsonConvert.SerializeObject(pacote, settings);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _pacotesApiHttpClient.PostAsync(_pacotesApiHttpClient.BaseAddress + "/pacote", data);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PacoteRequest>(responseContent);
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
				return new PacoteData();
			}
        }

        public async Task<bool> Deletar(int idPacote)
        {
            try
            {

                var response = await _pacotesApiHttpClient.DeleteAsync("/pacote/" + idPacote);

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


        public async Task<ListaPacote> ListarTodos()
        {
            try
            {

                var response = await _pacotesApiHttpClient.GetAsync(_pacotesApiHttpClient.BaseAddress + "/pacotes");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ListaPacote>(responseContent);
                    return result;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ListaPacote();
            }
        }
    }
}
