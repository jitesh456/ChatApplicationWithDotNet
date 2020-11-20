using ChatModelLayer;
using System;

namespace ChatApplicationDotNet
{
    public interface IUserRepository
    {
        Boolean AddUser(UserDetails userDetails);
        Boolean UserLogin(Login login);
    }
}