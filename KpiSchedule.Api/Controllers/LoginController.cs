using KpiSchedule.Api.Filters;
using KpiSchedule.Api.Mappers;
using KpiSchedule.Api.Models.Requests;
using KpiSchedule.Api.Models.Responses;
using KpiSchedule.Services.Interfaces;
using KpiSchedule.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace KpiSchedule.Api.Controllers
{
    /// <summary>
    /// User authentication related actions.
    /// </summary>
    [Route("login")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticate user with auth data received from Telegram login widget callback.
        /// </summary>
        /// <param name="telegramAuth">Telegram auth data.</param>
        /// <returns>JWT token response.</returns>
        /// <response code="401">Telegram auth data is invalid.</response>
        /// <response code="200">Telegram auth data is valid, use this bearer token to call authorized actions.</response>
        [HttpPost("telegram")]
        [ProducesResponseType(typeof(JwtToken), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 401)]
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
