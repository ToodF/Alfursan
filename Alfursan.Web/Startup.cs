using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alfursan.Web.Startup))]
namespace Alfursan.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
