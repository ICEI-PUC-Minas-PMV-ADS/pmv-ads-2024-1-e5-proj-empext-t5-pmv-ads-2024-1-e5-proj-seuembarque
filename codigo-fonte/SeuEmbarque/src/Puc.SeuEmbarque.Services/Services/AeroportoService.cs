using Puc.SeuEmbarque.Domain.Models.Skyscanner;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using Puc.SeuEmbarque.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Services.Services
{
    public class AeroportoService : IAeroportoService
    {
        private readonly IAeroportoRepository _skyScannerRepository;
        public AeroportoService(IAeroportoRepository skyScannerRepository)
        {
            _skyScannerRepository = skyScannerRepository;
        }
        public async Task<List<AeroportoDto>> GetAeroportos(string termo)
        {
            var dtos = new List<AeroportoDto>();
            var result = await _skyScannerRepository.ListarAeroportos(termo);

            foreach (var aeroporto in result)
            {
                var dto = new AeroportoDto();
                dto.AeroportoId = aeroporto.Id;
                dto.Name = $"{aeroporto.Name} ({aeroporto.Iata_code ?? "qualquer" })";

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
