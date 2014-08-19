
using System;

namespace Alfursan.Domain
{
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
        None = 0,
        ShipmentDoc = 1,
        AccountDoc = 2,
        Other = 3,
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
    public enum EnumResponseCode
    {
        Successful = 0,
        SystemError = 1,
        DbError = 2,
        NoRecordFound = 3,
        Authorized = 4,
        NotAuthorized = 5,
        AlreadyDefined = 6,
        MissingData = 7,
        InvalidOperation = 8,
        ValidationError = 9,
        Unexpected = 10
    }
}
