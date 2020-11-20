using ChatApplicationDotNet;
using ChatModelLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatApplicationServiceLayer
{
    public class UserService:IUserService
    {
        IUserRepository userRepository;
        IConfiguration Configration;

        public UserService(IUserRepository userRepository,IConfiguration configuration)
        {
            this.userRepository = userRepository;
            Configration = configuration;
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
        public String UserLogin(Login login)
        {
            try
            {
                string convertedPassword = PasswordEncryption.EncodePasswordToBase64(login.Password);
                login.Password = convertedPassword;
                userRepository.UserLogin(login);
                return CreateToken(login);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string CreateToken(Login login)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configration["Jwt:Key"]));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("Email", login.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var token = new JwtSecurityToken(Configration["Jwt:Issuer"],
                    Configration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
