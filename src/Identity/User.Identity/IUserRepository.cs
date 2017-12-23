using System;
using System.Collections.Generic;

namespace Super.User.Identity
{
    public interface IUserRepository
    {
        List<UserModle> GetUsers();

        UserModle GetUser(Guid id);

        Guid AddUser(UserModle user);

        bool UpdateUser(UserModle user);

        bool DeleteUser(Guid id);
    }
}
