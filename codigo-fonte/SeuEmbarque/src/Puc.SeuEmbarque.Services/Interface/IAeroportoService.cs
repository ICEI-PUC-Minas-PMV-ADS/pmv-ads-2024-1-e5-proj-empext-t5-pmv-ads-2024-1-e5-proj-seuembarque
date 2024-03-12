using Puc.SeuEmbarque.Domain.Models.Skyscanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Interface
{
    public interface IAeroportoService
    {
        Task<List<AeroportoDto>> GetAeroportos(string termo);
    }
}
