using System;
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
using Microsoft.Owin;
using FormCollection = System.Web.Mvc.FormCollection;

namespace Alfursan.Web.Controllers
{
    public class AlfursanApiController : ApiController
    {
        // GET: api/Api
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AlfuranApi/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/AlfuranApi
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/AlfuranApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AlfuranApi/5
        public void Delete(int id)
        {
        }

        [Route("api/alfursanapi/GetUsersByType/{userType}")]
        public DataGirdModelView Get(string userType)
        {
            var userService = IocContainer.Resolve<IUserService>();
            var users = userService.GetAll();
            Mapper.CreateMap<User, UserListViewModel>();
            var userListViewModel = Mapper.Map<List<User>, List<UserListViewModel>>(users);
            var dataGirdModelView = new DataGirdModelView();
            dataGirdModelView.recordsTotal = userListViewModel.Count;
            dataGirdModelView.draw = 1;
            dataGirdModelView.recordsFiltered = userListViewModel.Count;
            dataGirdModelView.data = userListViewModel.ToArray();
            var jsonResult = new System.Web.Mvc.JsonResult
            {
                Data = dataGirdModelView,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
            return dataGirdModelView;
        }

       
    }
}
