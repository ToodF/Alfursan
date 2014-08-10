using System.Collections.Generic;
using System.Resources;
using System.Web.Mvc;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Models;
using AutoMapper;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult EditUser()
        {
            return View();
        }
        public ActionResult UserList()
        {
            var userService = IocContainer.Resolve<IUserService>();
            var users = userService.GetAll();
            Mapper.CreateMap<User, UserListViewModel>();
            var userListViewModels = Mapper.Map<List<User>, List<UserListViewModel>>(users);
            foreach (var user in userListViewModels)
            {
                if (user.ProfileId == EnumProfile.Admin)
                    user.ProfileName = Alfursan.Resx.Management.Profile_Admin;
                if (user.ProfileId == EnumProfile.CustomOfficer)

                    user.ProfileName = Alfursan.Resx.Management.Profile_CustomOfficer;
                if (user.ProfileId == EnumProfile.Customer)
                    user.ProfileName = Alfursan.Resx.Management.Profile_Customer;
                if (user.ProfileId == EnumProfile.User)
                    user.ProfileName = Alfursan.Resx.Management.Profile_User;
            }
            return View(userListViewModels);
        }

        public PartialViewResult _CreateUserView()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult _CreateUserView(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<UserViewModel, User>();
                var user = Mapper.Map<UserViewModel, User>(userViewModel);
                var userService = IocContainer.Resolve<IUserService>();
                userService.Set(user);
            }
            return PartialView();
        }
    }
}