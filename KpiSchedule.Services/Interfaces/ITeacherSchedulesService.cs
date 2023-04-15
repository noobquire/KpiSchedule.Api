using KpiSchedule.Common.Entities;

namespace KpiSchedule.Services.Interfaces
{
    /// <summary>
    /// Service used to perform operations with teacher schedules.
    /// </summary>
    public interface ITeacherSchedulesService
    {
        /// <summary>
        /// Get teacher schedule data by its scheduleId.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>Schedule data.</returns>
        Task<TeacherScheduleEntity> GetTeacherScheduleById(Guid scheduleId);

        /// <summary>
        /// Get list of subjects in a teacher schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>List of subjects.</returns>
        Task<IEnumerable<SubjectEntity>> GetSubjectsInTeacherSchedule(Guid scheduleId);

        /// <summary>
        /// Search teacher schedules by group name.
        /// </summary>
        /// <param name="teacherNamePrefix">Prefix of teacher's name to search.</param>
        /// <returns>Group schedule search results.</returns>
        Task<IEnumerable<TeacherScheduleSearchResult>> SearchTeacherSchedules(string teacherNamePrefix);
    }
}
