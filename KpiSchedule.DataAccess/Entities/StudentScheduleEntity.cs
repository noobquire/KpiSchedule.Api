namespace KpiSchedule.DataAccess.Entities
{
    /// <summary>
    /// DB entity for personal student schedule.
    /// </summary>
    public class StudentScheduleEntity : BaseScheduleEntity<StudentScheduleDayEntity, StudentSchedulePairEntity>
    {
        /// <summary>
        /// Boolean indicating if this schedule should be made public to other users.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Id of the user who owns this schedule.
        /// </summary>
        public Guid OwnerId { get; set; }
    }
}
