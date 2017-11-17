using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IMainServer
    {
        [OperationContract]
        Dictionary<string, DataObj> IntegrityUpdate(Dictionary<string, DataObj> lokalnaBazaServera);
    }
}
