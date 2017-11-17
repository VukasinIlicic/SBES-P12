using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IServer
    {
        [OperationContract]
        Dictionary<string,DataObj> PrikazInformacija();

        [OperationContract]
        double SrednjaVrednostPotrosnje(string grad, int year);

        [OperationContract]
        bool AzurirajPotrosnju(string id, string month, double consumption);

        [OperationContract]
        bool DodajEntitet(DataObj noviPotrosac);

        [OperationContract]
        bool ObrisiEntitet(string id);
    }
}
