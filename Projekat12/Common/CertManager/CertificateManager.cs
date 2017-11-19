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

	        return certCollection.Cast<X509Certificate2>().FirstOrDefault(c => c.SubjectName.Name.Equals($"CN={subjectName}"));
        }
    }
}
