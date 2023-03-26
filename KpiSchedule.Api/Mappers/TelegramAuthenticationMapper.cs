using KpiSchedule.Api.Models.Requests;
using KpiSchedule.Services.Models;
using KpiSchedule.Services.Utility;

namespace KpiSchedule.Api.Mappers
{
    public static class TelegramAuthenticationMapper
    {
        public static TelegramAuthentication MapToModel(this TelegramAuthenticationRequest request)
        {
            var model = new TelegramAuthentication
            {
                Id = request.Id.ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                AuthDate = DateTimeUtility.UnixTimestampToDateTime(request.AuthDate),
                PhotoUrl = request.PhotoUrl,
                Hash = request.Hash
            };
            return model;
        }
    }
}
