using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Entiteti;

namespace Common.Contracts
{
    [ServiceContract]
    public interface IUpdate
    {
        [OperationContract]
        Dictionary<string, DataObj> IntegrityUpdate();

        [OperationContract]
        void VratiKonzistentnuBazu(Dictionary<string, DataObj> glavna);
    }
}
