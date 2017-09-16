using System.Web.Mvc;

namespace InsuranceEngine.Web.Areas.Customer.Controllers
{
    [RouteArea("Admin")]
    public class HomeController : Controller
    {
        //
        // GET: /Customer/Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LayoutSample()
        {
            return View();
        }

        public ActionResult VisibilityTest()
        {
            return View();
        }

	}
}