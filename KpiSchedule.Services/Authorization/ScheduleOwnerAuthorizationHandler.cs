using KpiSchedule.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace KpiSchedule.Services.Authorization
{
    /// <summary>
    /// Allows schedule owner to perform read and write operations on schedules they own.
    /// </summary>
    public class ScheduleOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, StudentScheduleEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, StudentScheduleEntity resource)
        {
            var userIdClaim = context.User.FindFirst(c => c.Type == ClaimNames.UserId);

            if (userIdClaim == null)
            {
                return Task.CompletedTask;
            }

            if (userIdClaim.Value == resource.OwnerId.ToString() &&
            requirement.Name == StudentScheduleRequirements.ReadSchedule.Name ||
            requirement.Name == StudentScheduleRequirements.WriteSchedule.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
