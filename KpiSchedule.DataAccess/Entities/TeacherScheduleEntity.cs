namespace KpiSchedule.DataAccess.Entities
{
    /// <summary>
    /// Teacher schedule DB entity.
    /// </summary>
    public class TeacherScheduleEntity : BaseScheduleEntity<TeacherScheduleDayEntity, TeacherSchedulePairEntity>
    {
        /// <summary>
        /// Teacher name as listed in schedule title.
        /// </summary>
        public string TeacherName { get; set; }
    }
}
