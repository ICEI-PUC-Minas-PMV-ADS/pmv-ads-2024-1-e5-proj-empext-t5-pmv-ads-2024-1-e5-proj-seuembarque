using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Skyscanner
{
    public class Aeroporto
    {
		[JsonProperty("airport_id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("iata_code")]
		public string Iata_code { get; set; }

		[JsonProperty("latitude")]
		public double latitude { get; set; }

		[JsonProperty("longitude")]
		public double longitude { get; set; }

		[JsonProperty("links_count")]
		public float Links_count { get; set; }
	}
}
