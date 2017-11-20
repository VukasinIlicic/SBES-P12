using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{
	[ServiceContract]
	public interface IServer : IUpdate
	{
		[OperationContract]
		Dictionary<string, DataObj> PrikazInformacija();

		[OperationContract]
		double SrednjaVrednostPotrosnje(string grad, int year);

		[OperationContract]
		bool AzurirajPotrosnju(string id, int month, double consumption);

		[OperationContract]
		bool DodajEntitet(DataObj noviPotrosac);

		[OperationContract]
		bool ObrisiEntitet(string id); 
    }
}
