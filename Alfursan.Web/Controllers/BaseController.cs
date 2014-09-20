using System;
using System.Web;
using System.Web.Security;
using Alfursan.Domain;
using System.Web.Mvc;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Helpers;
using Alfursan.Web.Models;

namespace Alfursan.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string LoginCookieKey = Util.Util.BasicEncrypt("LoginCookieKey", true, "Login");
        protected string UserNameKey = Util.Util.BasicEncrypt("UserName", true, "Login");
        protected string PasswordKey = Util.Util.BasicEncrypt("Password", true, "Login");


        public User CurrentUser
        {
            get
            {
                if (Session["CurrentUser"] != null)
                    return (User)Session["CurrentUser"];

                return null;
            }
            set { Session["CurrentUser"] = value; }
        }

        public bool LoginAndSetCookie(LoginViewModel model, string returnUrl)
        {
            var userService = IocContainer.Resolve<IUserService>();
            var response = userService.Login(model.Email, model.Password);
            if (response.ResponseCode == EnumResponseCode.Successful)
            {
                var user = response.Data;
                FormsAuthentication.SetAuthCookie(user.Email, model.RememberMe);
                CurrentUser = user;

                if (model.RememberMe)
                {
                    var isSecure = Request.Url.Scheme.Equals("https") ? true : false;
                    var cookie = new HttpCookie(LoginCookieKey) { HttpOnly = true, Secure = isSecure };
                    cookie.Values.Add(UserNameKey, Util.Util.EncryptString(user.Email));
                    cookie.Values.Add(PasswordKey, Util.Util.EncryptString(user.Password));
                    var dtExpiry = DateTime.UtcNow.AddDays(14);
                    cookie.Expires = dtExpiry;
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
                return true;
            }
            else
            {

                ModelState.AddModelError("", ResourceHelper.GetGlobalMessageResource(response.ResponseUserFriendlyMessageKey));
            }
            return false;
        }
    }
}