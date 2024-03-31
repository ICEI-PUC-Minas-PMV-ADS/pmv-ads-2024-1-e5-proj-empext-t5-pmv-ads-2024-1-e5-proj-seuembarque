using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Domain.ObjValor
{
    [Serializable]
    public class ContractorResult : IContractorResult
    {
        public string Message { get; set; }
        public bool AcaoValida { get; set; }
        public object Data { get; set; }
        public object ClaimIdentity { get; set; }
        public object Properties { get; set; }

    }
    public interface IContractorResult
    {
        string Message { get; set; }
        bool AcaoValida { get; set; }
        public object Data { get; set; }
        public dynamic ClaimIdentity { get; set; }
        public object Properties { get; set; }
    }
}
