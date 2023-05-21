using System.ComponentModel.DataAnnotations;

namespace KpiSchedule.Api.Models.Requests
{
    public class CreateStudentScheduleRequest
    {
        [Required]
        public Guid GroupScheduleId { get; set; }

        [Required]
        public IEnumerable<string> SubjectNames { get; set; }

        [Required]
        [MaxLength(100)]
        public string ScheduleName { get; set; }
    }
}
