using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorkCollector.Data;

namespace CorkCollector.Web.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/Recommendation")]
    public class RecommendationController : CorkCollectorBaseController
    {
        [System.Web.Http.Route("List")]
        public List<TastingListItem> Get(string userId)
        {
            Wine bestChoice = null;
            List <TastingListItem> results = new List<TastingListItem>();

            using (var session = ravenStore.OpenSession())
            {
                var user = session.Load<UserProfile>(userId);

                if (user != null)
                {
                    var wineList = session.Query<Wine>().ToList();
                    var backupWines = wineList.Where(x => x.Rating == null).ToList();
                    wineList.RemoveAll(x => x.Rating == null);
                    wineList = wineList.OrderByDescending(x => x.Rating).ToList();

                    for (int i = 0; i < 5; i++)
                    {
                        bestChoice = wineList.FirstOrDefault(x =>
                            x.Reviews.All(y => y.UserId != userId) && !user.Tastings.Contains(x.WineId));

                        if (bestChoice == null)
                        {
                            bestChoice = backupWines.FirstOrDefault();
                            backupWines.Remove(bestChoice);
                        }
                        else
                        {
                            wineList.Remove(bestChoice);
                        }


                        if (bestChoice == null)
                            return results;

                        var Winery = session.Load<Winery>(bestChoice.WineryId);

                        string wineryName = string.Empty;

                        if (Winery != null && !string.IsNullOrEmpty(Winery.WineryName))
                            wineryName = Winery.WineryName;

                        results.Add(new TastingListItem(bestChoice, wineryName));
                        


                    }


                }


            }

            return results;
        }


    }
}