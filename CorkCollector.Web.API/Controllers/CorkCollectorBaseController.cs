using System.Web.Http;
using Raven.Client.Documents;

namespace CorkCollector.Web.API.Controllers
{
    public class CorkCollectorBaseController : ApiController
    {
        protected static DocumentStore ravenStore;

        public CorkCollectorBaseController()
        {
            ravenStore = WebApiApplication.RavenStore;
        }

        public CorkCollectorBaseController(DocumentStore _ravenStore = null)
        {
            ravenStore = _ravenStore ?? WebApiApplication.RavenStore;
        }
    }
}