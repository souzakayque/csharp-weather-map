using System;
using System.Collections.Generic;
using System.Text;
using WeatherMap.Domain.Repositories;
using WeatherMap.Domain.User.Entities;

namespace WeatherMap.Infra.Data.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly AuthConfig _config;
        private static Lazy<UserRepository> _instance;

        public static UserRepository Instance(AuthConfig config)
        {
            try
            {
                if (_instance == null)
                    _instance = new Lazy<UserRepository>(() => new UserRepository(config));

                return _instance.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private UserRepository(AuthConfig config)
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

        public User Authentication(string username, string password)
        {
            try
            {
                User result = new User(string.Empty, string.Empty);

                if (username.Equals(_config.Username) && password.Equals(_config.Password))
                    return new User(_config.Username, _config.Password);

                return result;
            }
            catch (Exception e)
            {
                return new User(string.Empty, string.Empty);
            }
        }

    }
}
