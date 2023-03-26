using KpiSchedule.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace KpiSchedule.Services.Authorization
{
    /// <summary>
    /// Allows all users to perform read operations on public schedules.
    /// </summary>
    public class PublicScheduleAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, StudentScheduleEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, StudentScheduleEntity resource)
        {
            if (!resource.IsPrivate && requirement.Name == StudentScheduleRequirements.ReadSchedule.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
