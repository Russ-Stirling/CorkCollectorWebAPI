using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CorkCollector.Data;

namespace CorkCollector.Web.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/Tasting")]
    public class TastingController : CorkCollectorBaseController
    {
        [System.Web.Http.Route("List")]
        public List<TastingListItem> Get(string userId)
        {
            List<TastingListItem> wines = new List<TastingListItem>();

            using (var session = ravenStore.OpenSession())
            {
                UserProfile user = session.Load<UserProfile>(userId);

                var wineList = session.Query<Wine>().ToList();
                var wineryList = session.Query<Winery>().ToList();

                if (user.Tastings == null || user.Tastings.Count == 0)
                    return wines;

                foreach (var wineId in user.Tastings)
                {
                    var wine = wineList.FirstOrDefault(x=> x.WineId == wineId);
                    var winery = wineryList.FirstOrDefault(x => x.WineryId == wine.WineryId);
                    string wineryName = string.Empty;
                    if (winery!=null)
                        wineryName = winery.WineryName;
                        
                    wines.Add(new TastingListItem(wine, wineryName));
                }
            }

            return wines;
        }

        [System.Web.Http.Route("New")]
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