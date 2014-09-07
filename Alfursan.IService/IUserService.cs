using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IService
{
    public interface IUserService
    {
        EntityResponder<User> Login(string email, string pass);

        EntityResponder<List<User>> GetAll();

        EntityResponder<List<User>> GetCustomers();

        EntityResponder<List<User>> GetAllByUserType(EnumProfile profile);

        Responder Set(User user);

        EntityResponder<User> Get(int userId);

        Responder Delete(int id);

        Responder ChangePassword(string emailOrUsername, string oldPassword, string newPassword);

        Responder SaveRelationCustomerCustomOfficer(RelationCustomerCustomOfficer relationCustomerCustomOfficer);

        Responder ChangeStatus(int userId, bool status);

        EntityResponder<User> GetCustomerUser(int customOfficerId);

        EntityResponder<User> GetActiveUserByEmail(string email);

        EntityResponder<List<Country>> GetCountries();

        EntityResponder<List<User>> GetCustomOfficers();
    }
}
