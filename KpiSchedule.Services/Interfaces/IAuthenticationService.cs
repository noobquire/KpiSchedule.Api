using KpiSchedule.Services.Models;

namespace KpiSchedule.Services.Interfaces
{
    public interface IAuthenticationService
    {
        JwtToken AuthenticateTelegramUser(TelegramAuthentication telegramAuth);
    }
}
