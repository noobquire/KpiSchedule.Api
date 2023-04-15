namespace KpiSchedule.Services.Models
{
    /// <summary>
    /// Telegram authentication data returned from login widget callback.
    /// </summary>
    public class TelegramAuthentication
    {
        /// <summary>
        /// User unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name. Optional.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Username (@tag). Optional.
        /// </summary>
        public string? Username { get; set; }
        
        /// <summary>
        /// User photo URL. Optional.
        /// </summary>
        public string? PhotoUrl { get; set; }

        /// <summary>
        /// Authentication UTC timestamp.
        /// </summary>
        public DateTime AuthDate { get; set; }

        /// <summary>
        /// Authentication hash computed from data in other fields and bot private token.
        /// </summary>
        public string Hash { get; set; }
    }
}
