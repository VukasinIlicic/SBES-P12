using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Common.CertManager
{
    public class CertificateManager
    {
        public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            var store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            var certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

            return certCollection.Cast<X509Certificate2>().FirstOrDefault(c => IsEqual(c.SubjectName.Name, subjectName));
        }

        private static bool IsEqual(string param, string subjectName)
        {
            var grupe = param.Split(' ');
            if (grupe.Length > 1 && grupe[0].Equals($"CN=\"{subjectName}"))
            {
                return true;
            }
            else if (grupe[0].Equals($"CN={subjectName}"))
            {
                return true;
            }
            return false;
        }
    }
}
