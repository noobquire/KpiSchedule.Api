using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KpiSchedule.Services.Builders
{
    public class TelegramClaimsIdentityBuilder
    {
        private readonly List<Claim> claims = new List<Claim>();

        public TelegramClaimsIdentityBuilder AddTelegramUserId(string id)
        {
            claims.Add(new Claim("userId", id));
            return this;
        }

        public TelegramClaimsIdentityBuilder AddUniqueJwtIdentifier()
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            return this;
        }

        public TelegramClaimsIdentityBuilder AddJwtSubject(string id)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, id));
            return this;
        }

        public TelegramClaimsIdentityBuilder AddTelegramFirstName(string firstName)
        {
            claims.Add(new Claim("firstName", firstName));
            return this;
        }

        public TelegramClaimsIdentityBuilder AddOptionalTelegramLastName(string lastName)
        {
            if(lastName is null) return this;

            claims.Add(new Claim("lastName", lastName));
            return this;
        }

        public TelegramClaimsIdentityBuilder AddOptionalTelegramPhotoUrl(string photoUrl)
        {
            if (photoUrl is null) return this;

            claims.Add(new Claim("photoUrl", photoUrl));
            return this;
        }

        public TelegramClaimsIdentityBuilder AddOptionalTelegramUsername(string username)
        {
            if (username is null) return this;

            claims.Add(new Claim("username", username));
            return this;
        }

        public ClaimsIdentity Build()
        {
            return new ClaimsIdentity(claims);
        }
    }
}
