using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CorkCollector.Data;
using Raven.Client.Documents;

namespace CorkCollector.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            var Cert = new X509Certificate2();
            Cert.Import("D:\\CorkCollector\\DBServer\\CorkCollectorTest.pfx", "Cork123", X509KeyStorageFlags.DefaultKeySet);
            var store = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] {"https://a.corkcollector.dbs.local.ravendb.net:8080"},
                Certificate = Cert

            };

            store.Initialize();

            var x = store.OpenSession();

            x.Store(new Wine());

            x.SaveChanges();

        }
    }
}
