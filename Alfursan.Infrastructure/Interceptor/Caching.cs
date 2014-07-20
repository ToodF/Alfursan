using Castle.DynamicProxy;

namespace Alfursan.Infrastructure.Interceptor
{
    public class Caching : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            /*DoSomethings*/
            invocation.Proceed();
        }
    }
}
