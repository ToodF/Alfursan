using System.Collections.Generic;
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
        public User Login(string email, string pass)
        {
            var user = userRepository.Get(email, pass);
            if (user == null)
                throw new NotFoundUserException();
            return user;
        }

        public List<User> GetAllByUserType(EnumProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
