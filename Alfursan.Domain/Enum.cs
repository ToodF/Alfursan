
using System;
using System.ComponentModel.DataAnnotations;

namespace Alfursan.Domain
{
    public enum EnumProfile
    {
        //None = 0,
        Admin = 1,
        User = 2,
        Customer = 3,
        CustomOfficer = 4
    }

    public enum EnumFileType : byte
    {
        None = 0,
        [Display(Name = "Shipment Document")]
        ShipmentDoc = 1,
        [Display(Name = "Account Document")]
        AccountDoc = 2,
        [Display(Name = "Other")]
        Other = 3,
    }

    public enum EnumRoleType
    {
        None = 0,
        FileRole = 1,
        UserRole = 2
    }

    public enum EnumRole
    {
        //None,
        AddUser,
        UpdateUser,
        DeleteUser,

        AddCustomer,
        UpdateCustomer,
        DeleteCustomer,

        AddCustomOfficer,
        UpdateCustomOfficer,
        DeleteCustomOfficer,

        ChangePassword,

        AddFile,
        DeleteFile
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
        Unexpected = 10,
        NotInserted,
        NotUpdated
    }

    public enum EnumLanguage
    {
        English = 1,
        Arabic = 2
    }
}
