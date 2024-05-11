using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Puc.SeuEmbarque.Domain.Models.Skyscanner;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Repository
{
    public class AeroportoRepository : IAeroportoRepository
    {
        private readonly HttpClient _aeroportoApiHttpClient;
        //private readonly string _skyScannerApiKey;

        public AeroportoRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
			_aeroportoApiHttpClient = httpClientFactory.CreateClient("AeroportoApi");
            // _skyScannerApiKey = configuration.GetSection("SkyScannerConfig")["ApiKey"];

            //_skyScannerHttpClient.DefaultRequestHeaders.Add("x-api-key", _skyScannerApiKey);
        }
        public async Task<List<Aeroporto>> ListarAeroportos(string termo)
        {
            try
            {
                string url = _aeroportoApiHttpClient.BaseAddress + termo;

                HttpResponseMessage response = await _aeroportoApiHttpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<AeroportoRequest>(responseContent);

                    return result.Data;
                }
                else
                {
                    throw new Exception("A solicitação à API falhou com o status: " + response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return new List<Aeroporto>();
            }
        }
    }
}
