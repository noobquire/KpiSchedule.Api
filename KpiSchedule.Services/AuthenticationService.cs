using KpiSchedule.Services.Builders;
using KpiSchedule.Services.Exceptions;
using KpiSchedule.Services.Interfaces;
using KpiSchedule.Services.Models;
using KpiSchedule.Services.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace KpiSchedule.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITelegramAuthenticationService telegramAuthenticationService;
        private readonly JwtOptions jwtOptions;

        public AuthenticationService(ITelegramAuthenticationService telegramAuthenticationService, IOptions<JwtOptions> jwtOptions)
        {
            this.telegramAuthenticationService = telegramAuthenticationService;
            this.jwtOptions = jwtOptions.Value;
        }

        public JwtToken AuthenticateTelegramUser(TelegramAuthentication telegramAuth)
        {
            if(!telegramAuthenticationService.IsValidTelegramLoginHash(telegramAuth))
            {
                throw new InvalidAuthenticationException();
            }

            var identityBuilder = new TelegramClaimsIdentityBuilder();
            var identity = identityBuilder
                .AddTelegramUserId(telegramAuth.Id) // if more authentication methods are added, we need to generate our own IDs and store them somewhere
                                                    // for now the authentication works only with Telegram and does not require a database to store user data
                .AddUniqueJwtIdentifier()
                .AddJwtSubject(telegramAuth.Id)
                .AddTelegramFirstName(telegramAuth.FirstName)
                .AddOptionalTelegramLastName(telegramAuth.LastName)
                .AddOptionalTelegramPhotoUrl(telegramAuth.PhotoUrl)
                .AddOptionalTelegramUsername(telegramAuth.Username)
                .Build();

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtOptions.KeyBytes), SecurityAlgorithms.HmacSha512Signature);

            var expiresAtDateTime = DateTime.UtcNow.AddSeconds(jwtOptions.LifespanSeconds);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expiresAtDateTime,
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            var jwtToken = new JwtToken
            {
                Token = stringToken,
                ExpiresAt = expiresAtDateTime
            };

            return jwtToken;
        }
    }
}
