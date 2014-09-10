using System.Net.Mail;
using System.Web;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Helpers;
using Alfursan.Web.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Security;

namespace Alfursan.Web.Api
{
    public class UserApiController : ApiController
    {
        // GET: api/UserApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserApi/5
        public HttpResponseModel Get(int id)
        {
            var userService = IocContainer.Resolve<IUserService>();
            var response = userService.Get(id);
            if (response.ResponseCode == EnumResponseCode.Successful)
            {
                Mapper.CreateMap<User, UserViewModel>();
                var userViewModel = Mapper.Map<User, UserViewModel>(response.Data);
                userViewModel.ConfirmPassword = userViewModel.Password;
                if (response.Data.RelationCustomerCustomOfficer != null)
                    userViewModel.CustomOfficerId = response.Data.RelationCustomerCustomOfficer.CustomOfficerId;
                return new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Success,
                    Data = userViewModel
                };
            }
            else
            {
                return new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Error,
                    ResponseMessage = ResourceHelper.GetGlobalMessageResource(response.ResponseUserFriendlyMessageKey)
                };
            }
        }

        // POST: api/UserApi
        public HttpResponseModel Post(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<UserViewModel, User>();
                var user = Mapper.Map<UserViewModel, User>(userViewModel);
                var userService = IocContainer.Resolve<IUserService>();
                var result = userService.Set(user);

                if (result.ResponseCode == EnumResponseCode.Successful)
                {
                    if (userViewModel.ProfileId == EnumProfile.Customer)
                    {
                        if (userViewModel.CustomOfficerId > 0)
                        {
                            var relationCustomerCustomOfficer = new RelationCustomerCustomOfficer
                            {
                                CreatedUserId = 0, // ((User)HttpContext.Current.Session["CurrentUser"]).UserId,
                                CustomOfficerId = userViewModel.CustomOfficerId,
                                CustomerUserId = user.UserId
                            };
                            userService.SaveRelationCustomerCustomOfficer(relationCustomerCustomOfficer);
                        }
                        else
                        {
                            userService.DeleteRelationCustomerCustomOfficer(user.UserId);
                        }
                    }
                    if (userViewModel.UserId == 0)
                    {
                        SendMessageHelper.SendMessageNewUser(userViewModel);
                    }
                    return new HttpResponseModel()
                    {
                        ReturnCode = EnumResponseStatusCode.Success,
                        ResponseMessage = Alfursan.Resx.MessageResource.Info_SetUser
                    };
                }
                else
                {
                    return new HttpResponseModel()
                    {
                        ReturnCode = EnumResponseStatusCode.Error,
                        ResponseMessage = Alfursan.Resx.MessageResource.ResourceManager.GetString(result.ResponseUserFriendlyMessageKey)
                    };
                }
            }
            return new HttpResponseModel() { ReturnCode = EnumResponseStatusCode.Error, ResponseMessage = Alfursan.Resx.MessageResource.Error_ModelNotValid };
        }

        [Route("api/UserApi/{id}/{status}")]
        public HttpResponseModel Post(int id, bool status)
        {
            var userService = IocContainer.Resolve<IUserService>();
            var response = userService.ChangeStatus(id, status);
            if (response.ResponseCode == EnumResponseCode.Successful)
            {
                return new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Success,
                    ResponseMessage = Alfursan.Resx.MessageResource.Info_DeleteUser
                };
            }
            else
            {
                return new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Error,
                    ResponseMessage = ResourceHelper.GetGlobalMessageResource(response.ResponseUserFriendlyMessageKey)
                };
            }
        }

        // DELETE: api/UserApi/5
        /// <summary>
        /// User silme Delete method için iss de düzenleme yapmak gerekli
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseModel Post(int id)
        {
            var userService = IocContainer.Resolve<IUserService>();
            var response = userService.Delete(id);
            if (response.ResponseCode == EnumResponseCode.Successful)
            {
                return new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Success,
                    ResponseMessage = Alfursan.Resx.MessageResource.Info_DeleteUser
                };
            }
            else
            {
                return new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Error,
                    ResponseMessage = ResourceHelper.GetGlobalMessageResource(response.ResponseUserFriendlyMessageKey)
                };
            }
        }
    }
}
