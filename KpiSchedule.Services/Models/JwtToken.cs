namespace KpiSchedule.Services.Models
{
    /// <summary>
    /// Model to contain JWT token with authentication data.
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// JWT access token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token expiration UTC timestamp.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
