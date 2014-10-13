using System.Linq;
using System.Reflection;
using System.Text;
using Alfursan.Domain;
using Alfursan.IRepository;
using Castle.DynamicProxy;
using System;

namespace Alfursan.Infrastructure.Interceptor
{
    public class ExceptionHandling : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Exception ex = null;
            var hasError = false;
            var responder = new Responder();
            try
            {
                invocation.Proceed();
            }
            catch (System.Data.SqlClient.SqlException exception)
            {
                hasError = true;
                ex = exception;
                responder.ResponseCode = EnumResponseCode.DbError;
                responder.ResponseErrorMessage = exception.Message;
                responder.ResponseUserFriendlyMessageKey = "Error_SqlException";
            }
            catch (Exception exception)
            {
                hasError = true;
                ex = exception;
                responder.ResponseCode = EnumResponseCode.Unexpected;
                responder.ResponseErrorMessage = exception.Message;
                responder.ResponseUserFriendlyMessageKey = "Error_Unexpected";
            }
            finally
            {
                if (hasError)
                {

                    var sb = new StringBuilder();
                    sb.AppendFormat("Called: {0}.{1}(", invocation.TargetType.Name, invocation.Method.Name);
                    for (var i = 0; i < invocation.Arguments.Count(); i++)
                    {
                        var paramInfo = invocation.Method.GetParameters()[1].ToString();
                        var argument = invocation.Arguments[i];
                        var argumentDescription = argument == null ? "null" : argument.ToString();
                        sb.AppendFormat("{0} : {1} ;", paramInfo, argumentDescription);
                    }
                    if (invocation.Arguments.Any()) sb.Length--;
                    sb.Append(")");

                    var logger = IocContainer.Resolve<ILoggerRepository>();
                    logger.Log(ex, sb.ToString());
                    if (invocation.Method.ReturnParameter.ParameterType.FullName.Equals(typeof(Responder).FullName))
                    {
                        invocation.ReturnValue = responder;
                    }
                    else
                    {
                        Type listType = invocation.Method.ReturnType;
                        object ret = null;
                        ret = Activator.CreateInstance(listType);
                        PropertyInfo responseCode = listType.GetProperty("ResponseCode");
                        responseCode.SetValue(ret, responder.ResponseCode, null);
                        PropertyInfo responseErrorMessage = listType.GetProperty("ResponseErrorMessage");
                        responseErrorMessage.SetValue(ret, responder.ResponseErrorMessage, null);
                        PropertyInfo responseUserFriendlyMessageKey = listType.GetProperty("ResponseUserFriendlyMessageKey");
                        responseUserFriendlyMessageKey.SetValue(ret, responder.ResponseUserFriendlyMessageKey, null);
                        PropertyInfo responseMessage = listType.GetProperty("ResponseMessage");
                        responseMessage.SetValue(ret, responder.ResponseMessage, null);
                        invocation.ReturnValue = ret;
                    }
                }
            }
        }

        private static string DumpObject(object argument)
        {
            Type objtype = argument.GetType();
            if (objtype == typeof(String) || objtype.IsPrimitive || !objtype.IsClass) { }
            //return objtype.ToString();
            return "";

            //return DataContractSerialize(argument, objtype);
        }
    }
}
