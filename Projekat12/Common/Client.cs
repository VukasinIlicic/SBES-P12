using Common.CertManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Client
    {
        public static T GetProxy<T>(string address, int port, string endpointName, string cltCertCN = null)
        {
            var binding = new NetTcpBinding();
            ChannelFactory<T> factory;


            if (cltCertCN != null)
            {
                var srvCert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
                factory = new ChannelFactory<T>(binding, new EndpointAddress(new Uri($"net.tcp://{address}:{port}/{endpointName}"), new X509CertificateEndpointIdentity(srvCert)));
                factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
                factory.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
                factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                factory.Credentials.ClientCertificate.Certificate = srvCert;
            }
            else
            {
                factory = new ChannelFactory<T>(binding, new EndpointAddress($"net.tcp://{address}:{port}/{endpointName}"));
            }
            var proxy = factory.CreateChannel();
            return proxy;
        }
    }
}
