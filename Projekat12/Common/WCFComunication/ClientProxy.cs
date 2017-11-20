using Common.CertManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class ClientProxy
	{
		public static TInterface GetProxy<TInterface>(string address, int port, string endpointName, bool useCertAuth = false)
		{
			var binding = new NetTcpBinding();
			ChannelFactory<TInterface> factory;

			if (useCertAuth)
			{
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
				var srvCert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "wcfservice");

				factory = new ChannelFactory<TInterface>(binding,
					new EndpointAddress(new Uri($"net.tcp://{address}:{port}/{endpointName}"), new X509CertificateEndpointIdentity(srvCert)));
				factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
				factory.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
				factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

				var clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
				var clientCert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);

				factory.Credentials.ClientCertificate.Certificate = clientCert;
			}
			else
			{
				factory = new ChannelFactory<TInterface>(binding, new EndpointAddress($"net.tcp://{address}:{port}/{endpointName}"));
			}
			var proxy = factory.CreateChannel();
			return proxy;
		}
	}
}