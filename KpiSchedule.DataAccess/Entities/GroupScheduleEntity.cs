namespace KpiSchedule.DataAccess.Entities
{
    /// <summary>
    /// Group schedule DB entity.
    /// </summary>
    public class GroupScheduleEntity : BaseScheduleEntity<GroupScheduleDayEntity, GroupSchedulePairEntity>
    {
        /// <summary>
        /// Group name.
        /// </summary>
        public string GroupName { get; set; }
    }
}
