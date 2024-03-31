using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Skyscanner
{
    public class AeroportoRequest
    {
        [JsonProperty("market")]
        public string market { get; set; } = "BR";

        [JsonProperty("locale")]
        public string locale { get; set; } = "pt-BR";

        [JsonProperty("searchTerm")]
        public string searchTerm { get; set; }
    }

    public class AeroportoRequestObj
    {
        [JsonProperty("query")]
        public AeroportoRequest AeroportoRequest { get; set; }
    }
}
