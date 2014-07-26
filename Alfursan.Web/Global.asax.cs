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

            //var userService = IocContainer.Resolve<IUserService>();
            //var user = userService.Login("a@a.com", "23");
        }
    }
}
