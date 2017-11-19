using System.ServiceModel;

namespace Common.Contracts
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
