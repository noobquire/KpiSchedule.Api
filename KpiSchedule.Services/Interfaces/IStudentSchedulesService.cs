using KpiSchedule.Common.Entities.Base;
using KpiSchedule.Common.Entities.Student;
using KpiSchedule.Common.Models;

namespace KpiSchedule.Services.Interfaces
{
    /// <summary>
    /// Service used to perform operations with group schedules.
    /// </summary>
    public interface IStudentSchedulesService
    {
        /// <summary>
        /// Get student schedule data by its scheduleId.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>Schedule data.</returns>
        Task<StudentScheduleEntity> GetStudentScheduleById(Guid scheduleId);

        /// <summary>
        /// Get list of subjects in a student schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>List of subjects.</returns>
        Task<IEnumerable<SubjectEntity>> GetSubjectsInStudentSchedule(Guid scheduleId);


        /// <summary>
        /// Create a new student schedule from group schedule with a list of subjects to use.
        /// </summary>
        /// <param name="groupScheduleId">Group schedule unique identifier.</param>
        /// <param name="subjectNames">List of subject names to include in student schedule.</param>
        /// <returns>Created student schedule data.</returns>
        Task<StudentScheduleEntity> CreateStudentScheduleFromGroupSchedule(Guid groupScheduleId, IEnumerable<string> subjectNames, string scheduleName);

        /// <summary>
        /// Search student schedules by ID of user which owns them.
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>Search results data.</returns>
        Task<IEnumerable<StudentScheduleSearchResult>> GetSchedulesForStudent(string userId);

        /// <summary>
        /// Update pair in a student schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <param name="pairId">Pair in a schedule identifier.</param>
        /// <param name="pair">Pair data to update.</param>
        /// <returns>Updated student schedule data.</returns>
        Task<StudentScheduleEntity> UpdatePair(Guid scheduleId, PairIdentifier pairId, StudentSchedulePairEntity pair);

        /// <summary>
        /// Delete pair in a student schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <param name="pairId">Pair in a schedule identifier.</param>
        /// <returns>Updated student schedule data.</returns>
        Task<StudentScheduleEntity> DeletePair(Guid scheduleId, PairIdentifier pairId);

        /// <summary>
        /// Update schedule visibility for other users.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <param name="isPublic">Boolean indicating if schedule can be accessed by other users.</param>
        /// <returns>Updated student schedule data.</returns>
        Task<StudentScheduleEntity> UpdateScheduleVisibility(Guid scheduleId, bool isPublic);

        /// <summary>
        /// Update schedule name.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <param name="newName">New schedule name.</param>
        /// <returns>Updated student schedule data.</returns>
        Task<StudentScheduleEntity> UpdateScheduleName(Guid scheduleId, string newName);

        /// <summary>
        /// Delete schedule data.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>Task.</returns>
        Task DeleteSchedule(Guid scheduleId);
    }
}
