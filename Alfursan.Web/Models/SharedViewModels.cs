using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alfursan.Web.Models
{
    public enum EnumResponseStatusCode
    {
        None = 0,
        Success = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }
    public class DataGirdModelView
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object[] data { get; set; }
        public string error { get; set; }
    }

    public class HttpResponseModel
    {
        public EnumResponseStatusCode ReturnCode { get; set; }
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }
}