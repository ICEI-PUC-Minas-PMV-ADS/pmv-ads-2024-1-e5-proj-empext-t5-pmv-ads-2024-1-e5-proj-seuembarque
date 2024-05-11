using Puc.SeuEmbarque.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Interface
{
    public interface IPacoteService
    {
        Task<PacoteData> InserirPacote(Pacote pacote);
        Task<PacoteData> AtualizarPacote(PacoteData pacote);
        Task<List<PacoteData>> ListarTodos();
        Task<PacoteData> GetPacotePorId(int idPacote);
        Task<List<PacoteDto>> Buscar();
        Task<bool> DeletarPacote(int idPacote);
    }
}
