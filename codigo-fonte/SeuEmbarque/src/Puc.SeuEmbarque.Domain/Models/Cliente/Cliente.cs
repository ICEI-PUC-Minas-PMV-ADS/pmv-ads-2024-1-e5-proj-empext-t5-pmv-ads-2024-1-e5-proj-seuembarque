using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.Models.Cliente
{
    public class Cliente
    {
        public string name { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string cellphone { get; set; }
    }

    public class ClienteData
    {
        public int client_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string cellphone { get; set; }
        public string registration_date { get; set; }
    }

    public class ClienteDataDto
    {
        public int client_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string cellphone { get; set; }
    }

    public class ClienteRequest
    {
        public ClienteData data { get; set; }
        public string message { get; set; }
    }

    public class ClienteLista
    {
        public List<ClienteData> data { get; set; }
        public string message { get; set; }
    }

}
