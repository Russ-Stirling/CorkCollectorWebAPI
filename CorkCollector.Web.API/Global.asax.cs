using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;

namespace CorkCollector.Web.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private X509Certificate2 Cert;
        public static DocumentStore RavenStore; 
  

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Cert = new X509Certificate2();


            
            
            string path = ConfigurationManager.AppSettings["CertificatePath"];
            if (!File.Exists(path))
            {
                throw new Exception("Couldn't find file");
            }
            //Console.WriteLine(path);

            //TODO: move pfx location and password to app config
            Cert.Import(path, "Cork123", X509KeyStorageFlags.DefaultKeySet);

            RavenStore = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] { "https://a.corkcollector.dbs.local.ravendb.net:8080" },
                Certificate = Cert
            };

            RavenStore.Initialize();

        }
    }
}
