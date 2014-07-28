
namespace Alfursan.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EnumProfile Profile { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CreatedUserId { get; set; }
        public bool Isdeleted { get; set; }
    }
}
