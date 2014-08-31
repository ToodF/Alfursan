using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Alfursan.Web.Filters
{
    public class AuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var identity = filterContext.HttpContext.User;

            if (!identity.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["CurrentUser"] == null)
            {
                var userService = IocContainer.Resolve<IUserService>();
                var response = userService.GetUserByEmail(identity.Identity.Name);
                if (response.ResponseCode == EnumResponseCode.Successful)
                {
                    var user = response.Data;

                    filterContext.HttpContext.Session["CurrentUser"] = user;

                    if (user.ProfileId == (int)EnumProfile.CustomOfficer)
                    {
                        var customerUser = userService.GetCustomerUser(user.UserId);
                        filterContext.HttpContext.Session["CustomerUserIdForCustomerOfficer"] = customerUser.Data.UserId;
                    }
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}