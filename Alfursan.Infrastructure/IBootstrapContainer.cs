using Castle.Windsor;

namespace Alfursan.Infrastructure
{
    public interface IBootstrapContainer
    {
        IWindsorContainer InstallServices(IWindsorContainer container);
    }
}
