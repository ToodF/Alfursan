using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Alfursan.Infrastructure;
using Alfursan.IService;
using System.Web.Http;
namespace Alfursan.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
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
                string lang = "en";
                //string lang = Request.RequestContext.RouteData.Values["culture"].ToString();
                //CultureInfo culture = CultureInfo.InvariantCulture;//if need invariant
                CultureInfo culture = CultureInfo.GetCultureInfo(lang);

                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }
    }
}
