using KpiSchedule.Services.Models;

namespace KpiSchedule.Services.Interfaces
{
    /// <summary>
    /// Service used to verify Telegram authentication data.
    /// </summary>
    public interface ITelegramAuthenticationService
    {
        /// <summary>
        /// Checks if Telegram authentication data is valid.
        /// </summary>
        /// <param name="telegramAuth">Telegram authentication data.</param>
        /// <returns>Boolean indicating if authentication is valid.</returns>
        bool IsValidTelegramLoginHash(TelegramAuthentication telegramAuth);
    }
}
