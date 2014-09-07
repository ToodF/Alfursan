
namespace Alfursan.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ProfileId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CreatedUserId { get; set; }
        public bool Isdeleted { get; set; }
        public int CountryId { get; set; }
        public RelationCustomerCustomOfficer RelationCustomerCustomOfficer { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", Name, Surname); }
        }

        public bool Status { get; set; }

        public string ConfirmKey { get; set; }
    }
}
