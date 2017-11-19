using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{
	[ServiceContract]
	public interface IMainServer
	{
		[OperationContract]
		Dictionary<string, DataObj> IntegrityUpdate(Dictionary<string, DataObj> lokalnaBazaServera, string imeServera);
	}
}
