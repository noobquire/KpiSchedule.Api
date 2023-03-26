using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace KpiSchedule.Services.Authorization
{
    /// <summary>
    /// Student schedule authorization requirements used for resource-based authorization.
    /// </summary>
    public static class StudentScheduleRequirements
    {
        /// <summary>
        /// Authorization requirement indicating that user can perform read operations (Read) on schedule.
        /// </summary>
        public static OperationAuthorizationRequirement ReadSchedule = new OperationAuthorizationRequirement()
        {
            Name = nameof(ReadSchedule)
        };

        /// <summary>
        /// Authorization requirement indicating that user can perform write operations (Update/Delete) on schedule.
        /// </summary>
        public static OperationAuthorizationRequirement WriteSchedule = new OperationAuthorizationRequirement()
        {
            Name = nameof(WriteSchedule)
        };
    }
}
