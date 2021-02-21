using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WeatherMap.API.Models;

namespace WeatherMap.API.Controllers
{
    public class BaseController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Response(int statusCode, RequestResult requestResult)
        {
            var result = requestResult.Result;
            var notifications = requestResult.Notifications;

            if (!notifications.Any())
            {
                return StatusCode(statusCode, new
                {
                    success = true,
                    data = result
                });
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = notifications
                });

            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> InternalErrorRequestResponse(string mensagem)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                success = false,
                errors = $"Ocorreu uma falha interna [{mensagem}]"
            });
        }
    }
}
