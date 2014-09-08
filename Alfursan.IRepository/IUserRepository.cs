using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        EntityResponder<User> Get(string emailOrUsername, string password);

        EntityResponder<List<User>> GetAllByUserType(EnumProfile profile);

        Responder ChangePassword(string emailOrUsername, string newPassword);

        Responder SaveRelationCustomerCustomOfficer(RelationCustomerCustomOfficer relationCustomerCustomOfficer);

        Responder ChangeStatus(int userId, bool status);

        EntityResponder<User> GetCustomerUserId(int customOfficerId);
        EntityResponder<List<User>> GetCustomers();

        EntityResponder<User> GetActiveUserByEmail(string email);
        EntityResponder<List<Country>> GetCountries();

        EntityResponder<List<User>> GetCustomOfficers();

        Responder SetConfirmKey(string email, string confirmKey);
    }
}
