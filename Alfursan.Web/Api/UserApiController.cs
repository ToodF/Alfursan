using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Helpers;
using Alfursan.Web.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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
                Mapper.CreateMap<User,UserViewModel>();
                var userViewModel = Mapper.Map<User, UserViewModel>(response.Data);
                userViewModel.ConfirmPassword = userViewModel.Password;
                return new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Success,
                    ResponseMessage = Alfursan.Resx.MessageResource.Info_DeleteUser,
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
                userService.Set(user);
                if (userViewModel.ProfileId == EnumProfile.Customer && userViewModel.CustomOfficerId > 0)
                {
                    
                }
                return new HttpResponseModel() { ReturnCode = EnumResponseStatusCode.Success, ResponseMessage = Alfursan.Resx.MessageResource.Info_SetUser };
            }
            return new HttpResponseModel() { ReturnCode = EnumResponseStatusCode.Error, ResponseMessage = Alfursan.Resx.MessageResource.Error_ModelNotValid };
        }

        // PUT: api/UserApi/5
        public void Put(int id, [FromBody]string value)
        {
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
