using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using CorkCollector.Data;

namespace CorkCollector.Web.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/Checkin")]
    public class CheckinController : CorkCollectorBaseController
    {
        [System.Web.Http.Route("New")]
        public HttpResponseMessage Post(CheckInSubmitModel checkin)
        {

            using (var session = ravenStore.OpenSession())
            {
                var winery = session.Load<Winery>(checkin.WineryId);

                //TODO check against checkin radius


                var user = session.Load<UserProfile>(checkin.UserId);
                if (user.CheckIns == null)
                    user.CheckIns = new List<CheckIn>();

                CheckIn newCheckin = new CheckIn()
                {
                    WineryName = winery.WineryName,
                    WineryId = winery.WineryId,
                    VisitTime = DateTimeOffset.Now
                };

                user.CheckIns.Add(newCheckin);

                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }

        [System.Web.Http.Route("List")]
        public List<CheckIn> Get(string userId)
        {
            List<CheckIn> checkins = new List<CheckIn>();

            using (var session = ravenStore.OpenSession())
            {
                UserProfile user = session.Load<UserProfile>(userId);
                if (user.CheckIns == null || user.CheckIns.Count == 0)
                    return checkins;

                checkins.AddRange(user.CheckIns);
            }

            return checkins;
        }
    }
}