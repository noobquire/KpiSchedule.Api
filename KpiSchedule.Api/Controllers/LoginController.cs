using KpiSchedule.Api.Filters;
using KpiSchedule.Api.Mappers;
using KpiSchedule.Api.Models.Requests;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KpiSchedule.Api.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("telegram")]
        [HandleInvalidAuthentication]
        public IActionResult AuthenticateTelegramUser([FromBody] TelegramAuthenticationRequest telegramAuth)
        {
            var authModel = telegramAuth.MapToModel();
            var token = authenticationService.AuthenticateTelegramUser(authModel);
            var tokenResponse = token.MapToResponse();
            return Ok(tokenResponse);
        }

    }
}
