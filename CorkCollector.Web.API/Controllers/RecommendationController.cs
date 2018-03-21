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
        public List<Wine> Get(string userId)
        {


            return new List<Wine>();
        }


    }
}