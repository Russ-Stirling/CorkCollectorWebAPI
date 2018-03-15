using System.Web.Mvc;

namespace CorkCollector.Web.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = Server.MapPath("CorkCollectorTest.pfx"); ;

            return View();
        }
    }
}
