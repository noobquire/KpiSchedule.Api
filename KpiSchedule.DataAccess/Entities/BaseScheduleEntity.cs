namespace KpiSchedule.DataAccess.Entities
{
    /// <summary>
    /// Base DB entity for schedules.
    /// </summary>
    /// <typeparam name="TScheduleDay">Schedule day type.</typeparam>
    public abstract class BaseScheduleEntity<TScheduleDay, TSchedulePair> : BaseEntity where TScheduleDay : BaseScheduleDayEntity<TSchedulePair> where TSchedulePair : BaseSchedulePairEntity
    {
        /// <summary>
        /// Schedule unique identifier.
        /// </summary>
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// First week of the schedule.
        /// </summary>
        public List<TScheduleDay> FirstWeek { get; set; }

        /// <summary>
        /// Second week of the schedule.
        /// </summary>
        public List<TScheduleDay> SecondWeek { get; set; }
    }
}
