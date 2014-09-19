using System.Web.Mvc;
using Alfursan.Web.Filters;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    [Authentication]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = Resources.Index.Title;
            return View();
        }
    }
}