using System.Web;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Filters;
using Alfursan.Web.Helpers;
using Alfursan.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    [Authentication]
    public class ManagementController : BaseController
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            ViewBag.DeleteMessage = @Resources.MessageResource.Warning_DeleteUser;
            var userService = IocContainer.Resolve<IUserService>();
            var users = userService.GetAll();
            Mapper.CreateMap<User, UserListViewModel>();
            var userListViewModels = Mapper.Map<List<User>, List<UserListViewModel>>(users.Data);
            foreach (var user in userListViewModels)
            {
                if (user.ProfileId == EnumProfile.Admin)
                    user.ProfileName = Resources.Management.Profile_Admin;
                if (user.ProfileId == EnumProfile.CustomOfficer)

                    user.ProfileName = Resources.Management.Profile_CustomOfficer;
                if (user.ProfileId == EnumProfile.Customer)
                    user.ProfileName = Resources.Management.Profile_Customer;
                if (user.ProfileId == EnumProfile.User)
                    user.ProfileName = Resources.Management.Profile_User;
            }
            return View(userListViewModels);
        }

        public PartialViewResult _CreateUserView(int id)
        {
            var userService = IocContainer.Resolve<IUserService>();
            if (id > 0)
            {
                var user = userService.Get(id);
                Mapper.CreateMap<User, UserViewModel>();
                var userViewModel = Mapper.Map<User, UserViewModel>(user.Data);
                ViewData.Model = userViewModel;
            }
            var customOfficerList = new List<SelectListItem>();
            customOfficerList.Add(new SelectListItem
            {
                Text = Resources.Shared.drp_Select,
                Value = "0"
            });
            var customOfficers = userService.GetCustomOfficers();
            if (customOfficers.ResponseCode == EnumResponseCode.Successful)
            {

                foreach (var customOfficer in customOfficers.Data)
                {
                    customOfficerList.Add(new SelectListItem
                    {
                        Value = customOfficer.UserId.ToString(),
                        Text = customOfficer.FullName
                    });
                }
            }
            ViewData.Add("CustomOfficers", customOfficerList);

            var countryList = new List<SelectListItem>();
            countryList.Add(new SelectListItem
            {
                Text = Resources.Shared.drp_Select,
                Value = "0"
            });
            var countries = userService.GetCountries();
            if (countries.ResponseCode == EnumResponseCode.Successful)
            {

                foreach (var country in countries.Data)
                {
                    countryList.Add(new SelectListItem
                    {
                        Value = country.CountryId.ToString(),
                        Text = country.CountryName
                    });
                }
            }
            ViewData.Add("CountryList", countryList);


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

        [HttpPost]
        public JsonResult _ChangePass(ChangePassViewModel changePassViewModel)
        {
            HttpResponseModel result;
            if (ModelState.IsValid)
            {
                var userService = IocContainer.Resolve<IUserService>();
                var changePassResult = userService.ChangePassword(changePassViewModel.Email,
                    changePassViewModel.Password);
                if (changePassResult.ResponseCode == EnumResponseCode.Successful)
                {
                    var sendMailResult = SendMessageHelper.SendMessageChangePass(changePassViewModel);
                    if (sendMailResult.ResponseCode == EnumResponseCode.Successful)
                    {
                        result = new HttpResponseModel()
                        {
                            ReturnCode = EnumResponseStatusCode.Success,
                            ResponseMessage = Resources.MessageResource.Info_ChangedPassword
                        };
                    }
                    else
                    {
                        result = new HttpResponseModel()
                        {
                            ReturnCode = EnumResponseStatusCode.Error,
                            ResponseMessage =
                                Resources.MessageResource.Warning_ChangedPasswordSendMail
                        };
                    }
                }
                else
                {
                    result = new HttpResponseModel()
                    {
                        ReturnCode = EnumResponseStatusCode.Error,
                        ResponseMessage =
                            Resources.MessageResource.ResourceManager.GetString(
                                changePassResult.ResponseUserFriendlyMessageKey)
                    };
                }
            }
            else
            {
                result = new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Error,
                    ResponseMessage = Resources.MessageResource.Error_ModelNotValid
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
                    user.ProfileName = Resources.Management.Profile_Admin;
                if (user.ProfileId == EnumProfile.CustomOfficer)

                    user.ProfileName = Resources.Management.Profile_CustomOfficer;
                if (user.ProfileId == EnumProfile.Customer)
                    user.ProfileName = Resources.Management.Profile_Customer;
                if (user.ProfileId == EnumProfile.User)
                    user.ProfileName = Resources.Management.Profile_User;
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
                ViewData["warning"] = Resources.MessageResource.Warning_NotUpdateAdminProfile;
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
                    System.Web.HttpContext.Current.Application["Roles"] = roleService.GetAll().Data;
                }

                ViewData["success"] = Resources.MessageResource.Info_UpdateProfile;
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

        [HttpGet]
        public JsonResult GetCustomOfficers()
        {
            var userService = IocContainer.Resolve<IUserService>();
            var customOfficerList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = Resources.Shared.drp_Select,
                    Value = "0"
                }
            };
            var customOfficers = userService.GetCustomOfficers();
            if (customOfficers.ResponseCode == EnumResponseCode.Successful)
            {
                customOfficerList.AddRange(customOfficers.Data.Select(customOfficer => new SelectListItem
                {
                    Value = customOfficer.UserId.ToString(),
                    Text = customOfficer.FullName
                }));
            }
            var result = new HttpResponseModel()
               {
                   ReturnCode = EnumResponseStatusCode.Success,
                   Data = customOfficerList
               };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult _SendMail(FormCollection formCollection)
        {
            HttpResponseModel result;
            var mailTo = formCollection["mailTo"];
            var subject = formCollection["Subject"];
            var mailBody = formCollection["MailBody"];

            var mailResult = SendMessageHelper.SendMessage(mailTo.Split(';').ToList(), mailBody, subject);
            if (mailResult.ResponseCode == EnumResponseCode.Successful)
            {
                result = new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Success,
                    ResponseMessage = Resources.MessageResource.Info_SendMail
                };
            }
            else
            {
                result = new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Error,
                    ResponseMessage = Resources.MessageResource.ResourceManager.GetString(mailResult.ResponseUserFriendlyMessageKey)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}