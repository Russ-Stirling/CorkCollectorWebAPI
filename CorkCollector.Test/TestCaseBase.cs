using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CorkCollector.Web.API.Controllers;
using Raven.Client.Documents;
using Xunit;

namespace CorkCollector.Test
{
    public class TestCaseBase
    {
        private readonly X509Certificate2 Cert;
        private readonly DocumentStore RavenStore;
        private readonly WineryController wineryController;

        public TestCaseBase()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Cert = new X509Certificate2();

            //TODO: move pfx location and password to app config
            Cert.Import("D:\\CorkCollector\\DBServer\\CorkCollectorTest.pfx", "Cork123", X509KeyStorageFlags.DefaultKeySet);

            RavenStore = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] { "https://a.corkcollector.dbs.local.ravendb.net:8080" },
                Certificate = Cert
            };

            RavenStore.Initialize();

            wineryController = new WineryController()
            {
                ravenStore = RavenStore
            };
        }
        [Fact]
        public void PassingTest()
        {
            
            wineryController.Get();
        }
    }
}
