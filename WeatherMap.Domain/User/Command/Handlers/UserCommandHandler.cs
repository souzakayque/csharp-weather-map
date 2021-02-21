using Flunt.Notifications;
using System;
using WeatherMap.Domain.Repositories;
using WeatherMap.Domain.Services;
using WeatherMap.Domain.User.Command.Inputs;
using WeatherMap.Domain.User.Command.Results;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.User.Command.Handlers
{
    public class UserCommandHandler : Notifiable, IUserCommandHandler
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;

        public UserCommandHandler(IUserRepository repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

        public ICommandResult Handle(LoginCommand command, string id)
        {
            try
            {
                Entities.User login = new Entities.User(command.Username, command.Password);
                var encodedPassword = login.EncodePassword(login.Password);
                var user = _repository.Authentication(login.Username, encodedPassword);

                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    AddNotification("Authentication", "Incorrect username or password.");
                    return null;
                }

                var token = _service.GenerateToken(user);
                
                return new LoginCommandResult(user.Username, "Bearer " + token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
