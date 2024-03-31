using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Skyscanner
{
    public class Aeroporto
    {
        [JsonProperty("entityId")]
        public string entityId { get; set; }

        [JsonProperty("iataCode")]
        public string iataCode { get; set; }

        [JsonProperty("parentId")]
        public string parentId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("countryId")]
        public string countryId { get; set; }

        [JsonProperty("countryName")]
        public string countryName { get; set; }

        [JsonProperty("cityName")]
        public string cityName { get; set; }

        [JsonProperty("location")]
        public string location { get; set; }

        [JsonProperty("hierarchy")]
        public string hierarchy { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("highlighting")]
        public List<List<int>> highlighting { get; set; }
    }

    public class ListaAeroportos
    {
        [JsonProperty("places")]
        public List<Aeroporto> Aeroportos { get; set; }
    }
}
