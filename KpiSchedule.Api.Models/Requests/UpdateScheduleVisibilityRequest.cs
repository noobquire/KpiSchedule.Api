using System.ComponentModel.DataAnnotations;

namespace KpiSchedule.Api.Models.Requests
{
    public class UpdateScheduleVisibilityRequest
    {
        /// <summary>
        /// Boolean indicating if student schedule should be available to other users (read only).
        /// </summary>
        [Required]
        public bool IsPublic { get; set; }
    }
}
