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
    public class WineryController : CorkCollectorBaseController
    {
        public WineryController() : base()
        {

        }

        public WineryController(DocumentStore _ravenStore = null): base(_ravenStore)
        {

        }

        // GET api/winery       route: api/winery       returns: All wineries
        public List<Winery> Get()
        {

            List<Winery> wineries = new List<Winery>();
            using (var session = ravenStore.OpenSession())
            {
                wineries = session.Query<Winery>().ToList();
            }

            return wineries;
        }

        // GET api/winery/id         route: api/winery?id=wineries/[id]     Returns: SPecified winery
        public Winery Get(string id)
        {
            Winery winery = new Winery();
            using (var session = ravenStore.OpenSession())
            {
                winery = session.Load<Winery>(id);
            }

            return winery;
        }

        public HttpResponseMessage Post(Review review, string wineryId)
        {
            Winery winery;
            using (var session = ravenStore.OpenSession())
            {
                winery = session.Load<Winery>(wineryId);
                if (winery.Reviews == null)
                    winery.Reviews = new List<Review>();
                winery.Reviews.Add(review);
                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }

        public HttpResponseMessage Post(Winery newWinery)
        {
            Winery winery = newWinery;
            using (var session = ravenStore.OpenSession())
            {
                session.Store(winery);
                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }
    }
}