using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherMap.Domain.Services;
using WeatherMap.Domain.User.Entities;
using WeatherMap.Shared.Common;

namespace WeatherMap.Infra.Services
{
    public class UserService : IUserService
    {
        private readonly UserConfig _config;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private static Lazy<UserService> _instance;

        public static UserService Instance(UserConfig config)
        {
            try
            {
                if (_instance == null)
                    _instance = new Lazy<UserService>(() => new UserService(config));

                return _instance.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private UserService(UserConfig config)
        {
            try
            {
                _config = config;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UserService(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public string GenerateToken(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                    throw new Exception("Username cannot be null.");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(RunTime.key);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username.ToString()),
                        new Claim(ClaimTypes.Role, _config.Role.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                
                var token = tokenHandler.CreateToken(tokenDescriptor);
                
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    
    }
}
