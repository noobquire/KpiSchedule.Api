using System.ComponentModel.DataAnnotations;

namespace KpiSchedule.Api.Models.Requests
{
    public class UpdateScheduleNameRequest
    {
        /// <summary>
        /// New student schedule name.
        /// </summary>
        [Required]
        public string ScheduleName { get; set; }
    }
}
