using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Interface
{
    public interface IClienteService
    {
        Task<ClienteData> InserirCliente(Cliente cliente);
        Task<string> GetNomeCliente(int idCliente);
        Task<List<ClienteDto>> ListarTodosClientes();
        Task<ClienteData> GetClientePorId(int idCliente);
        Task<List<ClienteDataDto>> Buscar();
        Task<ClienteData> AtualizarCliente(ClienteDataDto cliente);
        Task<bool> DeletarCliente(int idCliente);
    }
}
