using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CorkCollector.Data;
using Raven.Client.Documents;

namespace CorkCollector.Web.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/Wine")]
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

        [System.Web.Http.Route("Review")]
        public HttpResponseMessage Post(ReviewSubmitModel reviewModel)
        {
            Wine wine;
            Review review = new Review()
            {
                UserId = "fakefornow",
                UserName = reviewModel.UserName,
                Text = reviewModel.Text,
                Rating = reviewModel.Rating
            };
            using (var session = ravenStore.OpenSession())
            {
                wine = session.Load<Wine>(reviewModel.SubjectId);
                if(wine.Reviews==null)
                    wine.Reviews = new List<Review>();
                wine.Reviews.Add(review);

                var rating = wine.Reviews.Average(x => x.Rating);
                wine.Rating = rating;

                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }

        public HttpResponseMessage Post(Wine newWine)
        {
            Wine wine = newWine;
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