using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IAuditServer
    {
        [OperationContract]
        string[] DajKljuc(); // rukovanje

        [OperationContract]
        void PosaljiKljucSesije(string m);

        [OperationContract]
        void PrijaviNeprijavljene(byte[] neprijavljeni);
    }
}
