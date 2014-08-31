using System.Web.Mvc;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = Alfursan.Resx.Index.Title;
            return View();
        }
    }
}