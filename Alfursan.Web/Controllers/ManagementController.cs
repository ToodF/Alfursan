using System.Web.Mvc;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult EditUser()
        {
            return View();
        }
    }
}