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
    public class SkyscannerRepository : ISkyscannerRepository
    {
        private readonly HttpClient _skyScannerHttpClient;
        private readonly string _skyScannerApiKey;

        public SkyscannerRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _skyScannerHttpClient = httpClientFactory.CreateClient("SkyScannerApi");
            _skyScannerApiKey = configuration.GetSection("SkyScannerConfig")["ApiKey"];

            _skyScannerHttpClient.DefaultRequestHeaders.Add("x-api-key", _skyScannerApiKey);
        }
        public async Task<List<Aeroporto>> ListarAeroportos(string termo)
        {
            try
            {
                var obj = new AeroportoRequestObj
                {
                    AeroportoRequest = new AeroportoRequest
                    {
                        searchTerm = termo
                    }
                };

                var requestAero = JsonConvert.SerializeObject(obj);

                var content = new StringContent(requestAero, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _skyScannerHttpClient.PostAsync(_skyScannerHttpClient.BaseAddress, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ListaAeroportos>(responseContent);

                    return result.Aeroportos;
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
