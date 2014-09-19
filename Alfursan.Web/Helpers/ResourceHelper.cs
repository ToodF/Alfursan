using Alfursan.Domain;
using Alfursan.Resx;
using System;
using Resources;

namespace Alfursan.Web.Helpers
{
    public class ResourceHelper
    {
        public static string GetGlobalMessageResource(string key)
        {
            var value = MessageResource.ResourceManager.GetString(key);
            if (value == null)
            {
                return MessageResource.ResourceManager.GetString(Const.Error_InvalidResourceKey);
            }
            return value.ToString();
        }

        public static string GetGlobalManagementResource(string key)
        {
            var value = Management.ResourceManager.GetString(key);
            if (value == null)
            {
                return MessageResource.ResourceManager.GetString(Const.Error_InvalidResourceKey);
            }
            return value.ToString();
        }

    
    }
}