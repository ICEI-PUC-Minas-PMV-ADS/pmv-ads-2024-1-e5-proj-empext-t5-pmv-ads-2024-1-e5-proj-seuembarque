using Puc.SeuEmbarque.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Interface
{
    public interface IPacoteRepository
    {
        Task<PacoteData> InserirPacote(Pacote pacote);
        Task<PacoteData> AtualizarPacote(PacoteUpdateDto pacote);
        Task<ListaPacote> ListarTodos();
        Task<PacoteData> GetPacotePorId(int idPacote);
        Task<bool> Deletar(int idPacote);
    }
}
