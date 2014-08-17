using Alfursan.Domain;
using Alfursan.Resx;
using System;

namespace Alfursan.Web.Helpers
{
    public class ResourceHelper
    {
        public static string GetGlobalResource(Type resource, string key)
        {
            var value = System.Web.HttpContext.GetGlobalResourceObject(resource.Name, key);
            if (value == null)
            {
                return System.Web.HttpContext.GetGlobalResourceObject(typeof(MessageResource).Name, Const.Error_InvalidResourceKey).ToString();
            }
            return value.ToString();
        }

        public static string GetGlobalMessageResource(string key)
        {
            var value = System.Web.HttpContext.GetGlobalResourceObject(typeof(MessageResource).Name, key);
            if (value == null)
            {
                return System.Web.HttpContext.GetGlobalResourceObject(typeof(MessageResource).Name, Const.Error_InvalidResourceKey).ToString();
            }
            return value.ToString();
        }

        public static string GetGlobalManagementResource(string key)
        {
            var value = System.Web.HttpContext.GetGlobalResourceObject(typeof(Resx.Management).Name, key);
            if (value == null)
            {
                return System.Web.HttpContext.GetGlobalResourceObject(typeof(Resx.MessageResource).Name, Const.Error_InvalidResourceKey).ToString();
            }
            return value.ToString();
        }

    
    }
}