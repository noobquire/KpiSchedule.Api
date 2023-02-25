using System.Runtime.Serialization;

namespace KpiSchedule.Services.Exceptions
{
    /// <summary>
    /// Exception indicating a business logic issue in KpiSchedule API.
    /// </summary>
    [Serializable]
    internal class KpiScheduleServiceException : Exception
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="KpiScheduleServiceException"/> class.
        /// </summary>
        public KpiScheduleServiceException()
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="KpiScheduleServiceException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public KpiScheduleServiceException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="KpiScheduleServiceException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>

        public KpiScheduleServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="KpiScheduleServiceException"/> class.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected KpiScheduleServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}