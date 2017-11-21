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
using Common.Helpers;
using Common.Entiteti;

namespace Common
{
	public class ClientProxy
	{
		public static TInterface GetProxy<TInterface>(string address, string port, string endpointName, AuthType authType = AuthType.NoAuth)
		{
			var binding = new NetTcpBinding();
			ChannelFactory<TInterface> factory;

			if (authType == AuthType.CertAuth)
			{
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
				var srvCert = CertificateManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, Konstanta.SRV_CERT_CN);

				factory = new ChannelFactory<TInterface>(binding,
					new EndpointAddress(new Uri($"net.tcp://{address}:{port}/{endpointName}"), new X509CertificateEndpointIdentity(srvCert)));
				factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
				factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

				var clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
				var clientCert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);

				factory.Credentials.ClientCertificate.Certificate = clientCert;
			}
            else if (authType == AuthType.WinAuth)
            {
                factory = new ChannelFactory<TInterface>(binding, new EndpointAddress($"net.tcp://{address}:{port}/{endpointName}"));
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