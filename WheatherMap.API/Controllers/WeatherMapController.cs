using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherMap.API.Models;
using WeatherMap.Domain.WeatherMap.Command.Handlers;
using WeatherMap.Domain.WeatherMap.Command.Inputs;
using WeatherMap.Domain.WeatherMap.Queries.Results;

namespace WeatherMap.API.Controllers
{
    [Route("/api/v1/weathermap")]
    [ApiController]
    public class WeatherMapController : BaseController
    {
        private readonly IWeatherMapCommandHandler _commandHandler;

        public WeatherMapController(IWeatherMapCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        /// <summary>
        /// Get temperature of specific city.
        /// </summary>
        /// /// <param name="initialDate">Initial Date of range of dates that will be returned at list.</param>
        /// /// <param name="finalDate">Final Date of range of dates that will be returned at list.</param>
        /// /// <param name="cityName">Name of the city to be searched.</param>
        /// <returns>WeatherMapQueryResult</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetWeatherMapQueryResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromHeader(Name = "Authorization")]string authorization, DateTime initialDate, DateTime finalDate, string cityName = null)
        {
            try
            {
                WeatherMapCommand command = new WeatherMapCommand();

                if (!string.IsNullOrWhiteSpace(authorization))
                {
                    var token = new JwtSecurityTokenHandler().ReadJwtToken(authorization.Split(' ')[1]);
                    command.Token = token.Claims.FirstOrDefault().Value;
                }
                else
                {
                    return await Response(401, new RequestResult(null, ((Notifiable)_commandHandler).Notifications));
                }

                command.SetToken(authorization);
                command.SetData(cityName, initialDate, finalDate);

                var result = _commandHandler.Handle(command, null);

                var weather = (GetWeatherMapQueryResult)result;
                
                return await Response(200, new RequestResult(weather.Data, ((Notifiable)_commandHandler).Notifications));
            }
            catch (Exception e)
            {
                return await InternalErrorRequestResponse("Error: " + e.Message);
            }
        }

        /// <summary>
        /// Get a list of cities and the required temperature of each one.
        /// </summary>
        /// <returns>WeatherMapQueryResult</returns>        
        [HttpGet("periodically")]
        [ProducesResponseType(typeof(GetWeathersMapQueryResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPeriodically([FromHeader(Name = "Authorization")]string authorization)
        {
            try
            {
                WeatherMapPeriodicallyCommand command = new WeatherMapPeriodicallyCommand();

                if (!string.IsNullOrWhiteSpace(authorization))
                {
                    var token = new JwtSecurityTokenHandler().ReadJwtToken(authorization.Split(' ')[1]);
                    command.Token = token.Claims.FirstOrDefault().Value;
                }
                else
                {
                    return await Response(401, new RequestResult(null, ((Notifiable)_commandHandler).Notifications));
                }

                command.SetDate(DateTime.Now);

                var result = _commandHandler.Handle(command, null);
                var weather = (GetWeathersMapQueryResult)result;
                return await Response(200, new RequestResult(weather.Data, ((Notifiable)_commandHandler).Notifications));
            }
            catch (Exception e)
            {
                return await InternalErrorRequestResponse("Error: " + e.Message);
            }
        }
        
    }
}
