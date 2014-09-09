using System;
using System.Configuration;
using Alfursan.Domain;
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
                Component.For<ExceptionHandling>().LifestyleTransient(),
                Component.For<IUserService>().ImplementedBy<UserService>().LifestyleTransient()
                    .Interceptors(InterceptorReference.ForType<Logging>()).First
                    .Interceptors(InterceptorReference.ForType<Caching>()).First
                    .Interceptors(InterceptorReference.ForType<ExceptionHandling>()).First,
                Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleTransient(),
                Component.For<IAlfursanFileRepository>().ImplementedBy<AlfursanFileRespository>().LifestyleTransient(),
                Component.For<IAlfursanFileService>().ImplementedBy<AlfursanFileService>().LifestyleTransient(),
                Component.For<IRoleRepository>().ImplementedBy<RoleRepository>().LifestyleTransient(),
                Component.For<IRoleService>().ImplementedBy<RoleService>().LifestyleTransient(),
                Component.For<IMessageSender>().ImplementedBy<MailService>().DependsOn(
                    Dependency.OnValue("MailProvider", new MailProvider()
                    {
                        Host = ConfigurationManager.AppSettings["MailServerHost"],
                        Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailServerPort"]),
                        UserName = ConfigurationManager.AppSettings["MailServerUserName"],
                        Password = ConfigurationManager.AppSettings["MailServerPassword"],
                        EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                    }))
                     .Interceptors(InterceptorReference.ForType<Logging>()).First
                    .Interceptors(InterceptorReference.ForType<Caching>()).First
                    .Interceptors(InterceptorReference.ForType<ExceptionHandling>()).First
                );

            return container;
        }
    }
}