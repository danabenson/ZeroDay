using System.Web.Mvc;

namespace ZeroDay.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}