using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models
{
    public class Pacote
    {
        public int client_id { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string departure_date { get; set; }
        public string? return_date { get; set; }
        public double price { get; set; }
        public string meals { get; set; }
        public bool accommodation { get; set; }
        public int kids { get; set; }
        public int adults { get; set; }
        public string travel_class { get; set; }
    }

    public class PacoteData
    {
        public bool accommodation { get; set; }
        public int adults { get; set; }
        public int client_id { get; set; }
        public string departure_date { get; set; }
        public string? return_date { get; set; }
        public string destination { get; set; }
        public int kids { get; set; }
        public string meals { get; set; }
        public string origin { get; set; }
        public int package_id { get; set; }
        public float price { get; set; }
        public string registration_date { get; set; }
        public string travel_class { get; set; }
        public string? nome_cliente { get; set; }
    }

    public class PacoteRequest
    {
        public PacoteData data { get; set; }
        public string message { get; set; }
    }

    public class ListaPacote
    {
        public List<PacoteData> data { get; set; }
        public string message { get; set; }
    }



    public class PacoteDto
    {
        public int package_id { get; set; }
        public int client_id { get; set; }
        public string? nome_cliente { get; set; }
        public string departure_date { get; set; }
        public string? return_date { get; set; }
        public string destination { get; set; }
        public string origin { get; set; }
        public double price { get; set; }
    }

    public class PacoteUpdateDto
    {
        public int package_id { get; set; }
        public int client_id { get; set; }
        public string departure_date { get; set; }
        public string? return_date { get; set; }
        public string destination { get; set; }
        public string origin { get; set; }
        public float price { get; set; }
        public string travel_class { get; set; }
        public string meals { get; set; }
        public int kids { get; set; }
        public bool accommodation { get; set; }
        public int adults { get; set; }
    }



}
