using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CorkCollector.Data;
using CorkCollector.Web.API.Models;
using Microsoft.AspNet.Identity;
using Raven.Client.Documents;
//using Raven.Client.Documents;

namespace CorkCollector.Web.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/User")]
    public class UserController : CorkCollectorBaseController
    {
        private AuthRepository _repo = null;

        public UserController() : base()
        {
            _repo = new AuthRepository();
        }

        // POST api/User/Register
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            CreateNewUser(userModel);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private string GetMonth(int monthInt)
        {
            switch (monthInt)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }

            return monthInt.ToString();
        }

        public void CreateNewUser(UserModel userModel)
        {
            var userId = Guid.NewGuid();
            var fullId = string.Format("UserProfiles/{0}", userId);

            string DateJoinedDisplay = string.Empty;

            DateTimeOffset now = DateTimeOffset.Now;

            DateJoinedDisplay = string.Format("{0} {1}, {2}", now.Day, GetMonth(now.Month), now.Year);

            UserProfile user = new UserProfile()
            {
                UserId = fullId,
                CellarBottles = new List<CellarBottle>(),
                CheckIns = new List<CheckIn>(),
                Email = userModel.Email,
                Friends = new List<string>(),
                PersonalComments = new List<PersonalComment>(),
                Tastings = new List<string>(),
                Username = userModel.UserName,
                Name = userModel.Name,
                DateJoined = DateJoinedDisplay
            };

            using (var session = ravenStore.OpenSession())
            {
                session.Store(user, fullId);
                session.SaveChanges();
            }

        }

        [System.Web.Http.Route("Profile")]
        public UserProfile Get(string username)
        {
            UserProfile user = new UserProfile();
            using (var session = ravenStore.OpenSession())
            {
                user = session.Query<UserProfile>().FirstOrDefault(x=> x.Username==username);
            }

            return user;
        }

        [System.Web.Http.Route("Tastings")]
        public List<TastingListItem> Get(string userId, string type)
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

        [System.Web.Http.Route("Checkins")]
        public List<CheckIn> Get(string userId, bool type)
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

        //public HttpResponseMessage Post(string userId, string friendid)
        //{
        //    UserProfile user;
        //    using (var session = ravenStore.OpenSession())
        //    {
        //        user = session.Load<UserProfile>(userId);
        //        if (user.Friends == null)
        //            user.Friends = new List<string>();
        //        user.Friends.Add(friendid);
        //        session.SaveChanges();
        //    }

        //    var response = new HttpResponseMessage(HttpStatusCode.Created);

        //    return response;
        //}
    }
}