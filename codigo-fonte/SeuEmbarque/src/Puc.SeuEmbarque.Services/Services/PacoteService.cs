using Puc.Diario.Infra;
using Puc.SeuEmbarque.Domain.Models;
using Puc.SeuEmbarque.Domain.Models.Skyscanner;
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
    public class PacoteService : IPacoteService
    {
        private readonly IPacoteRepository _pacoteRepository;
        private readonly IClienteService _clienteService;
        public PacoteService(IPacoteRepository pacoteRepository, IClienteService clienteService)
        {
            _pacoteRepository = pacoteRepository;
            _clienteService = clienteService;
        }
        public async Task<PacoteData> InserirPacote(Pacote pacote)
        {
            try
            {
                if(pacote != null)
                {
                    var result = await _pacoteRepository.InserirPacote(pacote);

                     return result;                    
                }
                else
                {
                    return null;
                }
                    
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<PacoteData> AtualizarPacote(PacoteData pacote)
        {
            try
            {
                if (pacote != null)
                {
                    var updtPacote = new PacoteUpdateDto()
                    {
                        accommodation = pacote.accommodation,
                        client_id = pacote.client_id,
                        adults = pacote.adults,
                        kids = pacote.kids,
                        departure_date = pacote.departure_date.ToString(),
                        destination = pacote.destination,
                        origin = pacote.origin,
                        meals = pacote.meals,
                        package_id = pacote.package_id,
                        price = pacote.price,
                        return_date = pacote.return_date != null ? pacote.return_date.ToString() : null,
                        travel_class = pacote.travel_class
                    };

                    var result = await _pacoteRepository.AtualizarPacote(updtPacote);

                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<PacoteData>> ListarTodos()
        {
            try
            {
                var pacotes = await _pacoteRepository.ListarTodos();
                var pacoteDto = new List<PacoteData>();

                if(pacoteDto != null)
                {
                    return pacoteDto;
                }
                else
                {
                    return new List<PacoteData>();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<PacoteDto>> Buscar()
        {
            try
            {
                var pacotes = await _pacoteRepository.ListarTodos();
                var pacoteDto = new List<PacoteDto>();

                foreach (var pacote in pacotes.data)
                {
                    var nomeCliente =await _clienteService.GetNomeCliente(pacote.client_id);

                    var pacoteData = new PacoteDto()
                    {
                        package_id = pacote.package_id,
                        client_id = pacote.client_id,
                        nome_cliente = nomeCliente.ToString(),
                        origin = pacote.origin,
                        destination = pacote.destination,
                        departure_date = Utils.ToDateTime(pacote.departure_date).ToString("dd/MM/yyyy"),
                        return_date = !String.IsNullOrEmpty(pacote.return_date) ? Utils.ToDateTime(pacote.return_date).ToString("dd/MM/yyyy") : null,
                        price = pacote.price
                    };

                    pacoteDto.Add(pacoteData);
                }
                return pacoteDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PacoteData> GetPacotePorId(int idPacote)
        {
            try
            {
                if (idPacote == 0)
                    return new PacoteData();

                var pacote = await _pacoteRepository.GetPacotePorId(idPacote);
                var nomeCliente = await _clienteService.GetNomeCliente(pacote.client_id);
                var classe = Utils.FormatarClasse(pacote.travel_class);

                pacote.travel_class = classe;
                pacote.nome_cliente = nomeCliente.ToString();

                return pacote;
            
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeletarPacote(int idPacote)
        {
            if (idPacote != null)
            {
                var result = await _pacoteRepository.Deletar(idPacote);

                return result;
            }

            return false;
        }
    }
}
