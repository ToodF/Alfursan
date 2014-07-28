
using System;

namespace Alfursan.Domain
{
    [Flags]
    public enum EnumProfile
    {
        None = 0,
        Admin = 1,
        User = 2,
        Customer = 3,
        CustomOfficer = 4
    }

    [Flags]
    public enum EnumFileType
    {
        Other = 0,
        ShipmentDoc = 1,
        AccountDoc = 2
    }

    [Flags]
    public enum EnumRoleType
    {
        None = 0,
        FileRole = 1,
        UserRole = 2
    }

    [Flags]
    public enum EnumRole
    {
        None,
        AddFile,
        DeleteFile,
        ReadFile,
        DeleteAllFile,
        AddUser,
        UpdateUser,
        ChangeUserStatus,
        DeleteUser,
        ChangePassword,
        CustomOfficer,
        UpdateCustomOfficer,
        DeleteCustomOfficer,
        Authorization
    }
}
