using Castle.Windsor;

namespace Alfursan.Infrastructure
{
    public static class IocContainer
    {
        private static readonly object LockObj = new object();

        private static IWindsorContainer _container;

        private static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (LockObj)
                    {
                        if (_container == null)
                        {
                            BootstrapContainer();
                        }
                    }
                }
                return _container;
            }
        }

        private static void BootstrapContainer()
        {

        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static IWindsorContainer Initialize(IBootstrapContainer container)
        {
            if (_container == null)
            {
                lock (LockObj)
                {
                    if (_container == null)
                    {
                        _container = new WindsorContainer();
                        _container = container.InstallServices(_container);
                    }
                }
            }
            return _container;
        }
    }
}
