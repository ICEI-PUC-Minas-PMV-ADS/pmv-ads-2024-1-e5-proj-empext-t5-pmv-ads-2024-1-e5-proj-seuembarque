using Puc.Diario.Infra;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Cliente;
using Puc.SeuEmbarque.Domain.Models.Usuario;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using Puc.SeuEmbarque.Infra.ApiData.Repository;
using Puc.SeuEmbarque.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteData> GetClientePorId(int idCliente)
        {
            try
            {
                if (idCliente == 0)
                    return new ClienteData();

                var cliente = await _clienteRepository.GetClientePorId(idCliente);

                cliente.cpf = cliente.cpf == "preencher" ? null : Utils.FormataCPF(cliente.cpf);
                cliente.cellphone = cliente.cellphone == "preencher" ? null : Utils.FormataTelefone(cliente.cellphone);

                return cliente;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetNomeCliente(int idCliente)
        {
            try
            {
                string nomeCliente = "-";
                var cliente = await _clienteRepository.GetClientePorId(idCliente);

                if(cliente != null)                
                    nomeCliente = cliente.name;                              
                
                return nomeCliente;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ClienteData> InserirCliente(Cliente cliente)
        {//logica para nao inserir 2 emails diferentes, verifica se o email ja existe se existir retorna o mesmo cara 
            try
            {
                if (cliente != null)
                {
                    var clientes = await _clienteRepository.ListarTodos();
                    var result = clientes.FirstOrDefault(x => x.email == cliente.email) ?? new ClienteData();

                    if (result.client_id == 0)
                    {
                        result = await _clienteRepository.InserirCliente(cliente);
                    }
                       

                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ClienteDto>> ListarTodosClientes()
        {
            try
            {
                var clientes = await _clienteRepository.ListarTodos();
                List<ClienteDto> list = new List<ClienteDto>();               

                foreach (var cliente in clientes)
                {
                    var clienteDto = new ClienteDto();

                    clienteDto.client_id = cliente.client_id;
                    clienteDto.name = cliente.name;

                    list.Add(clienteDto);
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<ClienteDto>();
            }
        }

          public async Task<List<ClienteDataDto>> Buscar()
        {
            try
            {
                var clientes = await _clienteRepository.ListarTodos();
                var clienteDto = new List<ClienteDataDto>();

                foreach (var cliente in clientes)
                {

                    var clienteData = new ClienteDataDto()
                    {
                        client_id = cliente.client_id,
                        name = cliente.name,
                        email = cliente.email,
                        cpf = cliente.cpf != "preencher" && cliente.cpf != null ? Utils.FormataCPF(cliente.cpf) : cliente.cpf,
                        cellphone = cliente.cellphone != "preencher" ? Utils.FormataTelefone(cliente.cellphone) : cliente.cellphone
                    };

                    clienteDto.Add(clienteData);
                }
                return clienteDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClienteData> AtualizarCliente(ClienteDataDto cliente)
        {
            try
            {
                if (cliente != null)
                {
                    var result = await _clienteRepository.AtualizarCliente(cliente);

                    return result;
                }
                else
                {
                    return new ClienteData();
                }

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<bool> DeletarCliente(int idCliente)
        {
            if (idCliente != null)
            {
                var result = await _clienteRepository.Deletar(idCliente);

                return result;
            }

            return false;
        }
    }
}
