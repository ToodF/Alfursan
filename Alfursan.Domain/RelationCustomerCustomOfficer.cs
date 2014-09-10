
namespace Alfursan.Domain
{
    public class RelationCustomerCustomOfficer
    {
        public int RelationId { get; set; }
        public int CustomerUserId { get; set; }
        public int CustomOfficerId { get; set; }
        public int CreatedUserId { get; set; }
    }
}
