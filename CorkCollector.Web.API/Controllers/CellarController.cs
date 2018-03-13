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
    [System.Web.Http.RoutePrefix("api/Cellar")]
    public class CellarController : CorkCollectorBaseController
    {
        [System.Web.Http.Route("InCellar")]
        public List<CellarBottle> Get(string userId)
        {
            List<CellarBottle> wines = new List<CellarBottle>();

            using (var session = ravenStore.OpenSession())
            {
                UserProfile user = session.Load<UserProfile>(userId);
                if (user.CellarBottles == null || user.CellarBottles.Count == 0)
                    return wines;

                foreach (var cellarBottle in user.CellarBottles)
                {
                    wines.Add(cellarBottle);
                }
            }
            return wines;
        }

        [System.Web.Http.Route("New")]
        public HttpResponseMessage Post(CellarSubmitModel cellarItem)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            using (var session = ravenStore.OpenSession())
            {
                UserProfile user = session.Load<UserProfile>(cellarItem.UserId);
                Wine wine = session.Load<Wine>(cellarItem.WineId);

                if (user != null && wine != null)
                {
                    
                    if(user.CellarBottles==null)
                        user.CellarBottles = new List<CellarBottle>();

                    if (user.CellarBottles.Any(x => x.WineId == cellarItem.WineId))
                    {
                        var item = user.CellarBottles.FirstOrDefault(x => x.WineId == cellarItem.WineId);
                        item.IncreaseBottleCount();
                        if (item.Finished)
                            item.Finished = false;
                        session.SaveChanges();
                        response = new HttpResponseMessage(HttpStatusCode.Created);
                        return response;
                    }

                    CellarBottle newItem = new CellarBottle(wine);
                    user.CellarBottles.Add(newItem);

                    session.SaveChanges();

                    response = new HttpResponseMessage(HttpStatusCode.Created);
                    return response;
                }
            }

            return response;
        }

        [System.Web.Http.Route("Finish")]
        public HttpResponseMessage Put(CellarSubmitModel cellarItem)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            using (var session = ravenStore.OpenSession())
            {
                UserProfile user = session.Load<UserProfile>(cellarItem.UserId);

                if (user != null)
                {

                    if (user.CellarBottles == null)
                        return new HttpResponseMessage(HttpStatusCode.NotFound);


                    if (user.CellarBottles.Any(x => x.WineId == cellarItem.WineId))
                    {
                        var item = user.CellarBottles.FirstOrDefault(x => x.WineId == cellarItem.WineId);
                        item.FinishBottle();
                        if (item.BottleCount == 0)
                            item.Finished = true;
                        session.SaveChanges();
                        response = new HttpResponseMessage(HttpStatusCode.OK);
                        return response;
                    }

                    return response;
                }
            }
            return response;
        }
    }
}