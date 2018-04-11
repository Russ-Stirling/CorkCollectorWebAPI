using System.Net;
using System.Security.Cryptography.X509Certificates;
using Raven.Client.Documents;

namespace CorkCollector.Test
{
    public class TestCaseBase
    {
        private readonly X509Certificate2 Cert;
        protected readonly DocumentStore RavenStore;

        public TestCaseBase()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Cert = new X509Certificate2();

            //TODO: move pfx location and password to app config
            Cert.Import("D:\\CorkCollector\\DBServer\\CorkCollectorTest.pfx", "Cork123", X509KeyStorageFlags.DefaultKeySet);

            RavenStore = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] { "https://a.corkdb.ravendb.community" },
                Certificate = Cert
            };

            RavenStore.Initialize();

            
        }
        
    }

}
