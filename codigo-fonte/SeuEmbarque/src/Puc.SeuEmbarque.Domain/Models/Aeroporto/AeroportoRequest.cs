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
		[JsonProperty("data")]
		public List<Aeroporto> Data { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

	}
}
