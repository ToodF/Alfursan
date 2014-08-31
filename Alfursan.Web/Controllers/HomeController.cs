using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = Alfursan.Resx.Index.Title;
            return View();
        }
    }
}