﻿using System.Collections.Generic;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.Infrastructure.Exceptions;
using Alfursan.IRepository;
using Alfursan.IService;
using System;

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
            var user = userRepository.Get(email, pass);
            if (user == null)
                throw new NotFoundUserException();
            return user;
        }

        public EntityResponder<List<User>> GetCustomers()
        {
            var users = userRepository.GetCustomers();
            return users;
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
            var user = userRepository.Get(userId);
            return user;
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
    }
}
