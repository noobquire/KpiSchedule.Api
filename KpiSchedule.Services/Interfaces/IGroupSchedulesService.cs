using KpiSchedule.Common.Entities;

namespace KpiSchedule.Services.Interfaces
{
    /// <summary>
    /// Service used to perform operations with group schedules.
    /// </summary>
    public interface IGroupSchedulesService
    {
        /// <summary>
        /// Get group schedule data by its scheduleId.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>Schedule data.</returns>
        Task<GroupScheduleEntity> GetGroupScheduleById(Guid scheduleId);

        /// <summary>
        /// Get list of subjects in a group schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>List of subjects.</returns>
        Task<IEnumerable<SubjectEntity>> GetSubjectsInGroupSchedule(Guid scheduleId);

        /// <summary>
        /// Get list of teachers in a group schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>List of teachers.</returns>
        Task<IEnumerable<TeacherEntity>> GetTeachersInGroupSchedule(Guid scheduleId);

        /// <summary>
        /// Search group schedules by group name.
        /// </summary>
        /// <param name="groupNamePrefix">Prefix of group name to search.</param>
        /// <returns>Group schedule search results.</returns>
        Task<IEnumerable<GroupScheduleSearchResult>> SearchGroupSchedules(string groupNamePrefix);
    }
}
