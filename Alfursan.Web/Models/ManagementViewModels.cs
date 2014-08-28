using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alfursan.Domain;

namespace Alfursan.Web.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Alfursan.Resx.Management))]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Alfursan.Resx.Management))]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Profile", ResourceType = typeof(Alfursan.Resx.Management))]
        public EnumProfile ProfileId { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Alfursan.Resx.Management))]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Phone", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Phone { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Address { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Alfursan.Resx.Management))]
        public int CountryId { get; set; }

        [Display(Name = "CustomOfficer", ResourceType = typeof(Alfursan.Resx.Management))]
        public int CustomOfficerId { get; set; }
    }

    public class UserListViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "UserName", ResourceType = typeof(Alfursan.Resx.Management))]
        public string UserName { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Email { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Name { get; set; }
        [Display(Name = "Surname", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Surname { get; set; }

        public EnumProfile ProfileId { get; set; }

        [Display(Name = "Profile", ResourceType = typeof(Alfursan.Resx.Management))]
        public string ProfileName { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Alfursan.Resx.Management))]
        public string Phone { get; set; }

        public bool Status { get; set; }
    }

    public class RoleViewModel
    {
        [Required]
        public EnumProfile ProfileId { get; set; }
        public List<RoleModel> Roles { get; set; }
    }

    public class RoleModel
    {
        public EnumRole Role { get; set; }
        public EnumRoleType RoleType { get; set; }
        public bool IsActive { get; set; }
    }
}