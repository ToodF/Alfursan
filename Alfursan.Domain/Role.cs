
namespace Alfursan.Domain
{
    public class Role
    {
        public int ProfileRoleId { get; set; }
        public int RoleId { get; set; }
        public int ProfileId { get; set; }
        public string RoleName { get; set; }
        public EnumRoleType RoleType { get; set; }
        public EnumFileType FileType { get; set; }
    }
}
