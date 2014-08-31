using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Helpers;
using Alfursan.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    public class ManagementController : BaseController
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
            var userListViewModels = Mapper.Map<List<User>, List<UserListViewModel>>(users.Data);
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
                var userViewModel = Mapper.Map<User, UserViewModel>(user.Data);
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
                Value = "2",
                Selected = true
            });
            listItems.Add(new SelectListItem
            {
                Text = "Exemplo3",
                Value = "3"
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

        public PartialViewResult _UserList()
        {
            var userService = IocContainer.Resolve<IUserService>();
            var users = userService.GetAll();
            Mapper.CreateMap<User, UserListViewModel>();
            var userListViewModels = Mapper.Map<List<User>, List<UserListViewModel>>(users.Data);
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
            return PartialView(userListViewModels);
        }

        public ActionResult EditProfileRole()
        {
            GetProfiles();
            return View();
        }

        [HttpPost]
        public ActionResult EditProfileRole(FormCollection collection)
        {
            var profileId = Convert.ToInt32(collection["ProfileId"]);
            if (profileId == (int)EnumProfile.Admin)
            {
                ViewData["warning"] = Alfursan.Resx.MessageResource.Warning_NotUpdateAdminProfile;
            }
            else
            {
                var roles = new List<Role>();
                foreach (var item in Enum.GetValues(typeof(EnumRole)))
                {
                    var isActive = !collection[item.ToString()].Equals("false");
                    if (isActive)
                    {
                        roles.Add(new Role() { ProfileId = profileId, RoleId = (int)(EnumRole)item });
                    }
                }

                if (roles.Any())
                {
                    var roleService = IocContainer.Resolve<IRoleService>();
                    var deleteOldRoles = roleService.DeleteRolesByProfileId(profileId);
                    if (deleteOldRoles.ResponseCode == EnumResponseCode.Successful)
                    {
                        roleService.SetRoles(roles);
                    }
                }
                ViewData["success"] = Alfursan.Resx.MessageResource.Info_UpdateProfile;
            }
            GetProfiles();
            return View();
        }

        void GetProfiles()
        {
            var profiles = (from object profile in Enum.GetValues(typeof(EnumProfile))
                            select new SelectListItem()
                            {
                                Text = ResourceHelper.GetGlobalManagementResource("EnumProfile_" + Enum.GetName(typeof(EnumProfile), profile)),
                                Value = ((int)profile).ToString()
                            }).ToList();
            ViewData["Profiles"] = profiles;
        }

        public PartialViewResult _Roles(int id)
        {
            var roleService = IocContainer.Resolve<IRoleService>();
            var roles = roleService.GetRolesByProfileId((int)id);
            var roleViewModel = new RoleViewModel { ProfileId = (EnumProfile)id, Roles = new List<RoleModel>() };
            foreach (var item in Enum.GetValues(typeof(EnumRole)))
            {
                RoleModel roleModel;
                if ((EnumRole)item == EnumRole.AddFile || (EnumRole)item == EnumRole.DeleteFile)
                {
                    roleModel = new RoleModel() { Role = (EnumRole)item, RoleType = EnumRoleType.FileRole };
                }
                else
                {
                    roleModel = new RoleModel() { Role = (EnumRole)item, RoleType = EnumRoleType.UserRole };
                }
                if (roles.ResponseCode == EnumResponseCode.Successful)
                {
                    roleModel.IsActive = roles.Data.Exists(f => f.RoleId == (int)(EnumRole)item);
                }

                roleViewModel.Roles.Add(roleModel);
            }
            return PartialView(roleViewModel);
        }
    }
}