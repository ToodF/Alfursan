using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Alfursan.Infrastructure;
using Alfursan.IService;

namespace Alfursan.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IocContainer.Initialize(new BootstrapContainer());
        }
        protected void Application_BeginRequest()
        {
            //if (Request.RequestContext.RouteData.Values["culture"] != null)
            {
                string lang = "ar";
                //string lang = Request.RequestContext.RouteData.Values["culture"].ToString();
                //CultureInfo culture = CultureInfo.InvariantCulture;//if need invariant
                CultureInfo culture = CultureInfo.GetCultureInfo(lang);

                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }
    }
}
