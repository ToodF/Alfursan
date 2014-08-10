using System.Collections.Generic;
using System.Resources;
using System.Web.Mvc;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Service;
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

        public PartialViewResult _CreateUserView(int id)
        {
            if (id > 0)
            {
                var userService = IocContainer.Resolve<IUserService>();
                var user = userService.Get(id);

                Mapper.CreateMap<User, UserViewModel>();
                var userViewModel = Mapper.Map<User, UserViewModel>(user);
                ViewData.Model = userViewModel;
            }

            var listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "Exemplo1",
                Value = "1"
            });
            listItems.Add(new SelectListItem
            {
                Text = "Exemplo2",
                Value = "1",
                Selected = true
            });
            listItems.Add(new SelectListItem
            {
                Text = "Exemplo3",
                Value = "1"
            });
            ViewData.Add("List", listItems);
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