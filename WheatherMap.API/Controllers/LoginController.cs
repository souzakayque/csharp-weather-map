using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherMap.API.Models;
using WeatherMap.Domain.User.Command.Handlers;
using WeatherMap.Domain.User.Command.Inputs;
using WeatherMap.Domain.User.Command.Results;

namespace WeatherMap.API.Controllers
{
    [Route("/api/v1/login")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IUserCommandHandler _commandHandler;

        public LoginController(IUserCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        /// <summary>
        /// Realiza autenticação do usuário e obtem um token
        /// </summary>
        /// <param name="command">Returns the token authentication</param>
        [HttpPost]
        [ProducesResponseType(typeof(LoginCommandResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            try
            {
                var result = _commandHandler.Handle(command, null);

                return await Response(200, new RequestResult(result, ((Notifiable)_commandHandler).Notifications));
            }
            catch (Exception e)
            {
                return await InternalErrorRequestResponse("Error: " + e.Message);
            }
        }
    }
}
