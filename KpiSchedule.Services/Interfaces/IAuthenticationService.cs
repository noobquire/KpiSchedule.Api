using KpiSchedule.Services.Models;

namespace KpiSchedule.Services.Interfaces
{
    /// <summary>
    /// Service used to authenticate users and issue them access tokens.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Check Telegram authentication data and isse a JWT token containing user data.
        /// </summary>
        /// <param name="telegramAuth">Telegram authentication data.</param>
        /// <returns></returns>
        JwtToken AuthenticateTelegramUser(TelegramAuthentication telegramAuth);
    }
}
