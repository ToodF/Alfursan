﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Models;
using AutoMapper;

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
        public string Get(int id)
        {
            return "value";
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
                return new HttpResponseModel() { ReturnCode =  EnumResponseStatusCode.Success,ResponseMessage = Alfursan.Resx.Management.Message_SetUserSuccess};
            }
            return new HttpResponseModel() { ReturnCode = EnumResponseStatusCode.Error ,ResponseMessage = Alfursan.Resx.Management.ErrorMessage_ModelNotValid};
        }

        // PUT: api/UserApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserApi/5
        public void Delete(int id)
        {
        }
    }
}
