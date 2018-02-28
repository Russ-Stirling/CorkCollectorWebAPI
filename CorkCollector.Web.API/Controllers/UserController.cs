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



        //public UserController() : base()
        //{

        //}

        //public UserController(DocumentStore _ravenStore = null): base(_ravenStore)
        //{
            
        //}
        //// GET api/wine       route: api/wine       returns: All wineries
        //public List<UserProfile> Get()
        //{
        //    List<UserProfile> users = new List<UserProfile>();
        //    using (var session = ravenStore.OpenSession())
        //    {
        //        users = session.Query<UserProfile>().ToList();
        //    }

        //    return users;
        //}

        //// GET api/wine/id         route: api/wine?id=wineries/[id]     Returns: SPecified wine
        //public UserProfile Get(string id)
        //{
        //    UserProfile user = new UserProfile();
        //    using (var session = ravenStore.OpenSession())
        //    {
        //        user = session.Load<UserProfile>(id);
        //    }

        //    return user;
        //}

        //public HttpResponseMessage Post(UserProfile newUser)
        //{
            
        //    using (var session = ravenStore.OpenSession())
        //    {
        //        session.Store(newUser);
        //        session.SaveChanges();
        //    }

        //    var response = new HttpResponseMessage(HttpStatusCode.Created);

        //    return response;
        //}

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