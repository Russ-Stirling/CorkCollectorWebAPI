using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CorkCollector.Data;
using Raven.Client.Documents;

namespace CorkCollector.Web.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/Winery")]
    public class WineryController : CorkCollectorBaseController
    {
        public WineryController() : base()
        {

        }

        public WineryController(DocumentStore _ravenStore = null): base(_ravenStore)
        {

        }

        // GET api/winery       route: api/winery       returns: All wineries
        [System.Web.Http.Authorize]
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

        [System.Web.Http.Route("Review")]
        public HttpResponseMessage Post(ReviewSubmitModel reviewModel)
        {
            Winery winery;
            Review review = new Review()
            {
                UserId = "Unknown User",
                UserName = reviewModel.UserName,
                Text = reviewModel.Text,
                Rating = reviewModel.Rating
            };
            using (var session = ravenStore.OpenSession())
            {
                winery = session.Load<Winery>(reviewModel.SubjectId);

                var userId = session.Query<UserProfile>().FirstOrDefault(x => x.Username == reviewModel.UserName);
                if (userId != null)
                    review.UserId = userId.UserId;

                if (winery.Reviews == null)
                    winery.Reviews = new List<Review>();
                winery.Reviews.Add(review);

                var rating = winery.Reviews.Average(x => x.Rating);
                winery.Rating = rating;
                
                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }

        [System.Web.Http.Route("Review")]
        public HttpResponseMessage Put(ReviewSubmitModel reviewModel)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            using (var session = ravenStore.OpenSession())
            {
                Winery winery = session.Load<Winery>(reviewModel.SubjectId);

                var review = winery.Reviews.FirstOrDefault(x => x.UserId == reviewModel.UserId);

                review.Rating = reviewModel.Rating;
                review.Text = reviewModel.Text;

                var rating = winery.Reviews.Average(x => x.Rating);
                winery.Rating = rating;

                session.SaveChanges();

                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }

        
        [System.Web.Http.Route("Remove")]
        public HttpResponseMessage Put(DeleteReviewSubmitModel delModel)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            using (var session = ravenStore.OpenSession())
            {
                Winery winery = session.Load<Winery>(delModel.SubjectId);

                Review myReview = winery.Reviews.FirstOrDefault(x => x.UserId == delModel.UserId);
                winery.Reviews.Remove(myReview);


                if (winery.Reviews.Count > 0)
                {
                    var rating = winery.Reviews.Average(x => x.Rating);
                    winery.Rating = rating;
                }
                else
                {
                    winery.Rating = 0;
                }

                session.SaveChanges();

                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }


        [System.Web.Http.Route("Checkin")]
        public HttpResponseMessage Post(CheckInSubmitModel checkin)
        {
            
            using (var session = ravenStore.OpenSession())
            {
                var winery = session.Load<Winery>(checkin.WineryId);

                //TODO check against checkin radius


                var user = session.Load<UserProfile>(checkin.UserId);
                if(user.CheckIns==null)
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