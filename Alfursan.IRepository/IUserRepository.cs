using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        EntityResponder<User> Get(string emailOrUsername);

        EntityResponder<User> Get(string emailOrUsername, string password);

        EntityResponder<List<User>> GetAllByUserType(EnumProfile profile);

        Responder ChangePassword(string emailOrUsername, string oldPassword, string newPassword);

        Responder SaveRelationCustomerCustomOfficer(RelationCustomerCustomOfficer relationCustomerCustomOfficer);

        Responder ChangeStatus(int userId, bool status);

        EntityResponder<User> GetCustomerUserId(int customOfficerId);
    }
}
