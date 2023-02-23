using KpiSchedule.DataAccess.Entities;

namespace KpiSchedule.DataAccess.Interfaces
{
    /// <summary>
    /// Interface used to get teacher schedules data from DB.
    /// </summary>
    public interface ITeacherSchedulesRepository : ISchedulesRepository<TeacherScheduleEntity, TeacherScheduleDayEntity, TeacherSchedulePairEntity>
    {
        /// <summary>
        /// Lookup teacher schedule IDs for teacher names starting with specified prefix.
        /// </summary>
        /// <param name="teacherNamePrefix">Teacher name prefix to search for.</param>
        /// <returns>List of groups starting with specified prefix.</returns>
        Task<IEnumerable<TeacherEntity>> SearchTeacherSchedules(string teacherNamePrefix);
    }
}
