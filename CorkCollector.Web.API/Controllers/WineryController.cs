using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CorkCollector.Data;
using Newtonsoft.Json;
using Raven.Client.Documents;

namespace CorkCollector.Web.API.Controllers
{
    public class WineryController : ApiController
    {
        public DocumentStore ravenStore;

        public WineryController()
        {
            if(ravenStore==null)
                { ravenStore = WebApiApplication.RavenStore;}
        }

        // GET api/winery       route: api/winery       returns: All wineries
        public string Get()
        {
            List<Winery> wineries = new List<Winery>();
            using (var session = ravenStore.OpenSession())
            {
                wineries = session.Query<Winery>().ToList();
            }

            return JsonConvert.SerializeObject(wineries);
        }

        // GET api/winery/id         route: api/winery?id=wineries/[id]     Returns: SPecified winery
        public string Get(string id)
        {
            Winery winery = new Winery();
            using (var session = ravenStore.OpenSession())
            {
                winery = session.Load<Winery>(id);
            }

            return JsonConvert.SerializeObject(winery);
        }
    }
}