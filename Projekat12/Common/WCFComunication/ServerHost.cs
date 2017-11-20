﻿using Common.CertManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class ServerHost<TInterface, TClass>
	{
		private ServiceHost _serviceHost;

		public ServerHost(string endpointName, string port, bool useCertAuth = false)
		{
			var endpoint = $"net.tcp://localhost:{port}/{endpointName}";
			_serviceHost = new ServiceHost(typeof (TClass));
			var binding = new NetTcpBinding();
			if (useCertAuth)
			{
				var srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
				_serviceHost.AddServiceEndpoint(typeof (TInterface), binding, endpoint);
				_serviceHost.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
				_serviceHost.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

				_serviceHost.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My,
					StoreLocation.LocalMachine, srvCertCN);
			}
			else
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