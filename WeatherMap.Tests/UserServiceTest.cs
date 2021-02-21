using AutoMapper;
using Moq;
using Xunit;
using WeatherMap.Domain.Services;
using WeatherMap.Infra.Services;
using System;
using WeatherMap.Domain.User.Entities;

namespace WeatherMap.Tests
{
    public class UserServiceTest
    {
        private UserService _service;

        public UserServiceTest()
        {
            _service = new UserService(new Mock<IUserService>().Object, new Mock<IMapper>().Object);
        }

        [Fact]
        public void GenerateToken_SendingEmptyValues()
        {
            User user = new User(null, null);
            var exception = Assert.Throws<Exception>(() => _service.GenerateToken(user));
        }
    }
}
