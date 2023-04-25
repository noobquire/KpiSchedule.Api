namespace KpiSchedule.Api.Models.Responses
{
    /// <summary>
    /// Response model for JWT token.
    /// </summary>
    public class JwtTokenResponse
    {
        /// <summary>
        /// Token value.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token expiration UTC datetime.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
