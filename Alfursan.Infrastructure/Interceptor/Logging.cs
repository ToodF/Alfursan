using Castle.DynamicProxy;

namespace Alfursan.Infrastructure.Interceptor
{
    public class Logging : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            /*DoSomethings*/
            invocation.Proceed();
        }
    }
}
