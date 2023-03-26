using System.Text;

namespace KpiSchedule.Services.Options
{
    /// <summary>
    /// JWT authentication options.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Token issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Secret key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Token life span in seconds.
        /// </summary>
        public int LifespanSeconds { get; set; }

        /// <summary>
        /// Secret key bytes.
        /// </summary>
        public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
    }
}
