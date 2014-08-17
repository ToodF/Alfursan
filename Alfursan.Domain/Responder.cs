
namespace Alfursan.Domain
{
    public class Responder
    {
        public EnumResponseCode ResponseCode { get; set; }
        
        public string ResponseMessage { get; set; }
        
        public string ResponseValue { get; set; }

        public string ResponseUserFriendlyMessageKey { get; set; }
        
        public string ResponseErrorMessage { get; set; }
    }
}
