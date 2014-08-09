using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alfursan.Web.Models;

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

        // POST: api/AlfuranApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AlfuranApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AlfuranApi/5
        public void Delete(int id)
        {
        }

        [Route("api/alfursanapi/GetUsersByType/{userType}")]
        public List<UserListViewModel> Get(string userType)
        {
            return new List<UserListViewModel>() { new UserListViewModel() { Email = "dfdf" } };
        }
    }
}
