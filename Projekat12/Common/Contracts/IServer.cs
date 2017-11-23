using System.Collections.Generic;
using System.ServiceModel;
using Common.Entiteti;
using System.Security;

namespace Common.Contracts
{
	[ServiceContract]
	public interface IServer
	{
		[OperationContract]
        [FaultContract(typeof(AuthorizationException))]
		Dictionary<string, DataObj> PrikazInformacija();

		[OperationContract]
        [FaultContract(typeof(AuthorizationException))]
        double SrednjaVrednostPotrosnje(string grad, int year);

		[OperationContract]
        [FaultContract(typeof(AuthorizationException))]
        bool AzurirajPotrosnju(string id, int month, double consumption);

		[OperationContract]
        [FaultContract(typeof(AuthorizationException))]
        bool DodajEntitet(DataObj noviPotrosac);

		[OperationContract]
        [FaultContract(typeof(AuthorizationException))]
        bool ObrisiEntitet(string id);

        [OperationContract]
        [FaultContract(typeof(AuthorizationException))]
        List<string> GetRoles();
    }
}
