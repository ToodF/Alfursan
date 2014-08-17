using System.Reflection;
using Alfursan.Domain;
using Castle.DynamicProxy;
using System;

namespace Alfursan.Infrastructure.Interceptor
{
    public class ExceptionHandling : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var hasError = false;
            var responder = new Responder();
            try
            {
                invocation.Proceed();
            }
            catch (System.Data.SqlClient.SqlException exception)
            {
                hasError = true;
                responder.ResponseCode = EnumResponseCode.DbError;
                responder.ResponseErrorMessage = exception.Message;
                responder.ResponseUserFriendlyMessageKey = "Error_SqlException";
            }
            catch (Exception exception)
            {
                hasError = true;
                responder.ResponseCode = EnumResponseCode.Unexpected;
                responder.ResponseErrorMessage = exception.Message;
                responder.ResponseUserFriendlyMessageKey = "Error_Unexpected";
            }
            finally
            {
                if (hasError)
                {
                    if (invocation.Method.ReturnParameter.ParameterType.FullName.Equals(typeof (Responder).FullName))
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
    }
}
