using System.Web.Mvc;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "DPS Rocks...";
            return View();
        }
    }
}
