﻿using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IService
{
    public interface IUserService
    {
        EntityResponder<User> Login(string email, string pass);

        EntityResponder<List<User>> GetAll();
        EntityResponder<List<User>> GetAllByUserType(EnumProfile profile);

        Responder Set(User user);

        EntityResponder<User> Get(int userId);

        Responder Delete(int id);
    }
}
