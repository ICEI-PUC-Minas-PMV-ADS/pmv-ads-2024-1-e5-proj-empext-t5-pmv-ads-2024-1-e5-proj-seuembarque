using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.ObjValor
{
    [Serializable]
    public class DataContractorResult<TEntity> : IDataContractorResult<TEntity>
    {       
        public string Message { get; set; }
        public bool AcaoValida { get; set; } = false;
        public TEntity Data { get; set; }

    }
    public interface IDataContractorResult<TEntity>
    {
        string Message { get; set; }
        bool AcaoValida { get; set; }
        public TEntity Data { get; set; }
    }
}
