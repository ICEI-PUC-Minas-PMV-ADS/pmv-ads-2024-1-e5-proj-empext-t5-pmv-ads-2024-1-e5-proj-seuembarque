using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Interface
{
    public interface IClienteRepository
    {
        Task<ClienteData> InserirCliente(Cliente cliente);
        Task<ClienteData> GetClientePorId(int idCliente);
        Task<List<ClienteData>> ListarTodos();
        Task<ClienteData> AtualizarCliente(ClienteDataDto cliente);
        Task<bool> Deletar(int idCliente);
    }
}
