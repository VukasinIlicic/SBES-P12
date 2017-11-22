using Common.CertManager;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authorization
{
    public class CustomPrincipal : IPrincipal
    {
        private GenericIdentity identity = null;
        private List<string> roles = new List<string>();

        public CustomPrincipal(GenericIdentity genericIdentity)
        {
            this.identity = genericIdentity;
            string name = Formatter.ParseSubjectName(identity.Name);
            X509Certificate2 clientCert = CertificateManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, name);

            if (clientCert != null)
            {
                string subjectName = clientCert.SubjectName.Name;
                string[] parseStrings = subjectName.Split(' ');

                string[] groups = parseStrings[1].Substring(3).Split('/');

                foreach (var g in groups)
                    roles.Add(g);
            }
        }

        public List<string> Roles
        {
            get { return roles; }
        }

        public IIdentity Identity
        {
            get { return this.identity; }
        }

        public bool IsInRole(string funkcija)
        {
            return MapiranjeUloga.Provera(funkcija, roles);
        }
    }
}
