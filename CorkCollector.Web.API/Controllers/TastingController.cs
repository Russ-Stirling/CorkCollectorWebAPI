using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CorkCollector.Data;

namespace CorkCollector.Web.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/Tasting")]
    public class TastingController : CorkCollectorBaseController
    {
        [System.Web.Http.Route("Tastings")]
        public List<TastingListItem> Get(string userId)
        {
            List<TastingListItem> wines = new List<TastingListItem>();

            using (var session = ravenStore.OpenSession())
            {
                UserProfile user = session.Load<UserProfile>(userId);
                if (user.Tastings == null || user.Tastings.Count == 0)
                    return wines;

                foreach (var wineId in user.Tastings)
                {
                    var wine = session.Load<Wine>(wineId);
                    var winery = session.Load<Winery>(wine.WineryId);
                    wines.Add(new TastingListItem(wine, winery.WineryName));
                }
            }

            return wines;
        }

        [System.Web.Http.Route("Taste")]
        public HttpResponseMessage Post(TasteSubmitModel taste)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            using (var session = ravenStore.OpenSession())
            {
                var wine = session.Load<Wine>(taste.WineId);

                if (wine != null)
                {
                    var user = session.Load<UserProfile>(taste.UserId);
                    if (user.Tastings == null)
                        user.Tastings = new List<string>();

                    user.Tastings.Add(taste.WineId);
                    session.SaveChanges();
                    response = new HttpResponseMessage(HttpStatusCode.Created);
                    return response;
                }
            }
            return response;
        }

    }
}