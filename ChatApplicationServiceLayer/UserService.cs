using ChatApplicationDotNet;
using ChatModelLayer;
using System;

namespace ChatApplicationServiceLayer
{
    public class UserService:IUserService
    {
        IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Boolean AddUser(UserDetails userDetails)
        {
            try
            {
                string convertedPassword=PasswordEncryption.EncodePasswordToBase64(userDetails.Password);
                userDetails.Password = convertedPassword;
                return userRepository.AddUser(userDetails);
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
