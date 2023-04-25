namespace KpiSchedule.Api.Models.Requests
{
    public class DeleteSchedulePairRequest
    {
        public int WeekNumber { get; set; }
        public int DayNumber { get; set; }
        public int PairNumber { get; set; }
        public int DuplicatePairNumber { get; set; }
    }
}
