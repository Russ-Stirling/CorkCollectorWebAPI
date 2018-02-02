using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CorkCollector.Data;
using Raven.Client.Documents;
//using Raven.Client.Documents;

namespace CorkCollector.Web.API.Controllers
{
    public class UserController : CorkCollectorBaseController
    {
        public UserController() : base()
        {

        }

        public UserController(DocumentStore _ravenStore = null): base(_ravenStore)
        {
            
        }
        // GET api/wine       route: api/wine       returns: All wineries
        public List<UserProfile> Get()
        {
            List<UserProfile> users = new List<UserProfile>();
            using (var session = ravenStore.OpenSession())
            {
                users = session.Query<UserProfile>().ToList();
            }

            return users;
        }

        // GET api/wine/id         route: api/wine?id=wineries/[id]     Returns: SPecified wine
        public UserProfile Get(string id)
        {
            UserProfile user = new UserProfile();
            using (var session = ravenStore.OpenSession())
            {
                user = session.Load<UserProfile>(id);
            }

            return user;
        }

        public HttpResponseMessage Post(UserProfile newUser)
        {
            
            using (var session = ravenStore.OpenSession())
            {
                session.Store(newUser);
                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }

        public HttpResponseMessage Post(string userId, string friendid)
        {
            UserProfile user;
            using (var session = ravenStore.OpenSession())
            {
                user = session.Load<UserProfile>(userId);
                if (user.Friends == null)
                    user.Friends = new List<string>();
                user.Friends.Add(friendid);
                session.SaveChanges();
            }

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;
        }
    }
}