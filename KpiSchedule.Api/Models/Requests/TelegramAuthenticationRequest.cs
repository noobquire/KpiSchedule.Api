using System.Text.Json.Serialization;

namespace KpiSchedule.Api.Models.Requests
{
    public class TelegramAuthenticationRequest
    {
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        public string Username { get; set; }

        [JsonPropertyName("photo_url")]
        public string PhotoUrl { get; set; }

        [JsonPropertyName("auth_date")]
        public long AuthDate { get; set; }

        public string Hash { get; set; }
    }
}
