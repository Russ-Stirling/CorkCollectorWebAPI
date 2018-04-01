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
        public Wine Get(string userId)
        {
            Wine bestChoice = null;

            using (var session = ravenStore.OpenSession())
            {
                var user = session.Load<UserProfile>(userId);

                if (user != null)
                {
                    var wineList = session.Query<Wine>().ToList();

                    var backupWine = wineList.FirstOrDefault(x => x.Rating == null);

                    wineList.RemoveAll(x => x.Rating == null);

                    wineList = wineList.OrderByDescending(x => x.Rating).ToList();

                    bestChoice = wineList.FirstOrDefault(x =>
                        x.Reviews.All(y => y.UserId != userId) && !user.Tastings.Contains(x.WineId));

                    if (bestChoice == null)
                        bestChoice = backupWine;


                }

                
            }

            return bestChoice;
        }


    }
}