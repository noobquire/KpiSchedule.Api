using KpiSchedule.Services.Interfaces;
using KpiSchedule.Services.Models;
using KpiSchedule.Services.Options;
using KpiSchedule.Services.Utility;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace KpiSchedule.Services
{
    public class TelegramAuthenticationService : ITelegramAuthenticationService
    {
        private readonly TelegramOptions telegramOptions;

        public TelegramAuthenticationService(IOptions<TelegramOptions> telegramOptions)
        {
            this.telegramOptions = telegramOptions.Value;
        }

        public bool IsValidTelegramLoginHash(TelegramAuthentication telegramAuth)
        {
            var authDataString = CombineAuthString(telegramAuth);
            var actualHash = HashHMAC(authDataString);

            return actualHash == telegramAuth.Hash;
        }

        private string HashHMAC(string message)
        {
            using var hasher = SHA256.Create();
            var keyBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(telegramOptions.BotToken));

            var messageBytes = Encoding.UTF8.GetBytes(message);
            var hash = new HMACSHA256(keyBytes);
            var computedHash = hash.ComputeHash(messageBytes);
            return Convert.ToHexString(computedHash).ToLower();
        }

        private string CombineAuthString(TelegramAuthentication telegramAuth)
        {
            var builder = new StringBuilder();

            TryAppend("auth_date", DateTimeUtility.DateTimeToUnixTimestamp(telegramAuth.AuthDate).ToString());
            TryAppend("first_name", telegramAuth.FirstName);
            TryAppend("id", telegramAuth.Id);
            TryAppend("last_name", telegramAuth.LastName);
            TryAppend("photo_url", telegramAuth.PhotoUrl);
            TryAppend("username", telegramAuth.Username, true);

            return builder.ToString();

            void TryAppend(string key, string value, bool isLast = false)
            {
                if (value is not null)
                    builder.Append($"{key}={value}{(isLast ? "" : "\n")}");
            }
        }
    }
}
