using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CorkCollector.Data;
using Newtonsoft.Json;
using Raven.Client.Documents;

namespace CorkCollector.Web.API.Controllers
{
    public class WineController : CorkCollectorBaseController
    {
        public WineController() : base()
        {

        }

        public WineController(DocumentStore _ravenStore = null): base(_ravenStore)
        {

        }

        // GET api/wine       route: api/wine       returns: All wineries
        public List<Wine> Get()
        {
            List<Wine> wines = new List<Wine>();
            using (var session = ravenStore.OpenSession())
            {
                wines = session.Query<Wine>().ToList();
            }

            return wines;
        }

        public List<Wine> Get(string wineryId, bool onMenu)
        {
            List<Wine> wines = new List<Wine>();
            using (var session = ravenStore.OpenSession())
            {
                wines = session.Query<Wine>().Where(x=>x.WineryId==wineryId).ToList();
            }

            return wines;
        }

        // GET api/wine/id         route: api/wine?id=wineries/[id]     Returns: SPecified wine
        public Wine Get(string id)
        {
            Wine wine = new Wine();
            using (var session = ravenStore.OpenSession())
            {
                wine = session.Load<Wine>(id);
            }

            return wine;
        }

        public HttpResponseMessage Post(Review review, string wineId)
        {
            Wine wine;
            using (var session = ravenStore.OpenSession())
            {
                wine = session.Load<Wine>(wineId);
                if(wine.Reviews==null)
                    wine.Reviews = new List<Review>();
                wine.Reviews.Add(review);
                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }
        public HttpResponseMessage Post(Wine newWinery)
        {
            Wine wine = newWinery;
            using (var session = ravenStore.OpenSession())
            {
                session.Store(wine);
                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }
    }
}