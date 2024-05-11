using Puc.SeuEmbarque.Domain.Models.Skyscanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.ApiData.Interface
{
    public interface IAeroportoRepository
    {
        Task<List<Aeroporto>> ListarAeroportos(string termo);
    }
}
