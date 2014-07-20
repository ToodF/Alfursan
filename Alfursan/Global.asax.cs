using Alfursan.App_Start;
using Alfursan.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;
using Alfursan.IService;

namespace Alfursan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            IocContainer.Initialize(new BootstrapContainer());
            var userService = IocContainer.Resolve<IUserService>();
            var user = userService.Login("a@a.com", "23");
        }
    }
}
