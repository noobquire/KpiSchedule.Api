using System.ComponentModel.DataAnnotations;

namespace KpiSchedule.Api.Models.Requests
{
    public class DeleteSchedulePairRequest
    {
        [Required]
        public int Week { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public int Pair { get; set; }

        public int DupPair { get; set; } = 1;
    }
}
