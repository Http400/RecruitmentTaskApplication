using System.Web.Mvc;

namespace Web.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Layout()
        {
            return PartialView();
        }

        public ActionResult HomePage()
        {
            return PartialView();
        }

        public ActionResult AccountPage()
        {
            return PartialView();
        }
    }
}
