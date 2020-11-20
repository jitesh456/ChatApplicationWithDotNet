using ChatModelLayer;
using System;

namespace ChatApplicationServiceLayer
{
    public interface IUserService
    {
         Boolean AddUser(UserDetails userDetails);
         string UserLogin(Login login);
    }
}