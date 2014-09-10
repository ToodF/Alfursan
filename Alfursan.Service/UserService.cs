using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IRepository;
using Alfursan.IService;
using System;
using System.Collections.Generic;

namespace Alfursan.Service
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService()
        {
            userRepository = IocContainer.Resolve<IUserRepository>();
        }
        public EntityResponder<User> Login(string email, string pass)
        {
            return userRepository.Get(email, pass);
        }

        public EntityResponder<List<User>> GetCustomers()
        {
            return userRepository.GetCustomers();
        }

        public EntityResponder<List<User>> GetAllByUserType(EnumProfile profile)
        {
            throw new NotImplementedException();
        }

        public Responder Set(User user)
        {
            if (user.UserId == 0)
                return userRepository.Set(user);
            else
                return userRepository.Update(user);
        }

        public EntityResponder<User> Get(int userId)
        {
            return userRepository.Get(userId);
        }

        public EntityResponder<List<User>> GetAll()
        {
            var users = userRepository.GetAll();
            return users;
        }

        public Responder Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public Responder ChangePassword(string emailOrUsername, string newPassword)
        {
            return userRepository.ChangePassword(emailOrUsername, newPassword);
        }

        public Responder SaveRelationCustomerCustomOfficer(RelationCustomerCustomOfficer relationCustomerCustomOfficer)
        {
            return userRepository.SaveRelationCustomerCustomOfficer(relationCustomerCustomOfficer);
        }

        public Responder ChangeStatus(int userId, bool status)
        {
            return userRepository.ChangeStatus(userId, status);
        }

        public EntityResponder<User> GetCustomerUser(int customOfficerId)
        {
            return userRepository.GetCustomerUserId(customOfficerId);
        }

        public EntityResponder<User> GetActiveUserByEmail(string email)
        {
            return userRepository.GetActiveUserByEmail(email);
        }

        public EntityResponder<List<Country>> GetCountries()
        {
            return userRepository.GetCountries();
        }

        public EntityResponder<List<User>> GetCustomOfficers()
        {
            return userRepository.GetCustomOfficers();
        }

        public Responder SetConfirmKey(string email, string confirmKey)
        {
            return userRepository.SetConfirmKey(email, confirmKey);
        }

        public Responder DeleteRelationCustomerCustomOfficer(int customerUserId)
        {
            return userRepository.DeleteRelationCustomerCustomOfficer(customerUserId);
        }

        public EntityResponder<List<User>> GetUsersForNotificationByCustomerUserId(int customerUserId)
        {
            return userRepository.GetUsersForNotificationByCustomerUserId(customerUserId);            
        }
    }
}
