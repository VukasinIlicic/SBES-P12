using Common.CertManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Authorization;
using System.IdentityModel.Policy;
using System.ServiceModel.Description;
using Common.Entiteti;

namespace Common
{
	public class ServerHost<TInterface, TClass>
	{
		private ServiceHost _serviceHost;

		public ServerHost(string endpointName, string port, AuthType authType = AuthType.NoAuth)
		{
			var endpoint = $"net.tcp://localhost:{port}/{endpointName}";
			_serviceHost = new ServiceHost(typeof (TClass));
			var binding = new NetTcpBinding();

			if (authType == AuthType.CertAuth)
			{
				var srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
				_serviceHost.AddServiceEndpoint(typeof (TInterface), binding, endpoint);
				_serviceHost.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
				_serviceHost.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

				_serviceHost.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My,
					StoreLocation.LocalMachine, srvCertCN);

                _serviceHost.Authorization.ServiceAuthorizationManager = new CustomAuthorizationManager();
                List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
                policies.Add(new CustomAuthorizationPolicy());
                _serviceHost.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

                _serviceHost.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            }
            else if (authType == AuthType.WinAuth)
            {
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                _serviceHost.AddServiceEndpoint(typeof(TInterface), binding, endpoint);

            }
            else if(authType == AuthType.NoAuth)
			{
				_serviceHost.AddServiceEndpoint(typeof (TInterface), binding, endpoint);
            }
		}

		public void Open()
		{
			_serviceHost.Open();
		}

		public void Close()
		{
			_serviceHost.Close();
		}
	}
}