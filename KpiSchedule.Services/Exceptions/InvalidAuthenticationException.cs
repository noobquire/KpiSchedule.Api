using System.Runtime.Serialization;

namespace KpiSchedule.Services.Exceptions
{
    /// <summary>
    /// Exception indicating that authentication request contains invalid data.
    /// </summary>
    [Serializable]
    public class InvalidAuthenticationException : Exception
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="InvalidAuthenticationException"/> class.
        /// </summary>
        public InvalidAuthenticationException() : this("Invalid authentication data.")
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="InvalidAuthenticationException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidAuthenticationException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="InvalidAuthenticationException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>

        public InvalidAuthenticationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="InvalidAuthenticationException"/> class.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected InvalidAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}