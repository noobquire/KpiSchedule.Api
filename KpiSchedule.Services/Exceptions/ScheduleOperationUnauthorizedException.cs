using KpiSchedule.Services.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Runtime.Serialization;

namespace KpiSchedule.Services.Exceptions
{
    /// <summary>
    /// Exception indicating that schedule operation is unauthorized.
    /// </summary>
    [Serializable]
    public class ScheduleOperationUnauthorizedException : Exception
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="ScheduleOperationUnauthorizedException"/> class.
        /// </summary>
        public ScheduleOperationUnauthorizedException(OperationAuthorizationRequirement operation) : this($"Operation {operation.Name} is unauthorized.")
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="ScheduleOperationUnauthorizedException"/> class.
        /// </summary>
        public ScheduleOperationUnauthorizedException() : this("Operation is unauthorized.")
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="ScheduleOperationUnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ScheduleOperationUnauthorizedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="ScheduleOperationUnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>

        public ScheduleOperationUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="ScheduleOperationUnauthorizedException"/> class.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected ScheduleOperationUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}