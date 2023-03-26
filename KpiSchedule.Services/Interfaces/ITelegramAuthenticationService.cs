using KpiSchedule.Services.Models;

namespace KpiSchedule.Services.Interfaces
{
    public interface ITelegramAuthenticationService
    {
        bool IsValidTelegramLoginHash(TelegramAuthentication telegramAuth);
    }
}
