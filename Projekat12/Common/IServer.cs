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
<<<<<<< HEAD
        
=======
        [OperationContract]
        void PrikazInformacija();

        [OperationContract]
        double SrednjaVrednostPotrosnje(string grad);

        [OperationContract]
        bool AzurirajPotrosnju(DataObj potrosac);

        [OperationContract]
        bool DodajEntitet(DataObj noviPotrosac);
>>>>>>> 87f4e0141019fc9143ed4ca255fe6269f27f07b7
    }
}
