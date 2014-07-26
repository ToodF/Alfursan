using Alfursan.Infrastructure;
using Alfursan.Infrastructure.Interceptor;
using Alfursan.IRepository;
using Alfursan.IService;
using Alfursan.Repository;
using Alfursan.Service;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Alfursan.Web
{
    public class BootstrapContainer : IBootstrapContainer
    {
        public IWindsorContainer InstallServices(IWindsorContainer container)
        {
            container.Register(
                 Component.For<Logging>().LifestyleTransient(),
                  Component.For<Caching>().LifestyleTransient(),
                 Component.For<IUserService>().ImplementedBy<UserService>().LifestyleTransient()
                  .Interceptors(InterceptorReference.ForType<Logging>()).First
                  .Interceptors(InterceptorReference.ForType<Caching>()).First
                  , Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleTransient()
                 );

            return container;
        }
    }
}