using System.Web.Mvc;

namespace Web.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return PartialView();
        }

        public ActionResult Layout()
        {
            return PartialView();
        }
    }
}
