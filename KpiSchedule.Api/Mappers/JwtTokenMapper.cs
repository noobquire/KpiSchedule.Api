using KpiSchedule.Api.Models.Responses;
using KpiSchedule.Services.Models;

namespace KpiSchedule.Api.Mappers
{
    public static class JwtTokenMapper
    {
        public static JwtTokenResponse MapToResponse(this JwtToken model)
        {
            var response = new JwtTokenResponse
            {
                Token = model.Token,
                ExpiresAt = model.ExpiresAt,
            };
            return response;
        }
    }
}
