using KpiSchedule.DataAccess.Entities;

namespace KpiSchedule.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for reading and writing student schedules in DB.
    /// </summary>
    public interface IStudentSchedulesRepository : ISchedulesRepository<StudentScheduleEntity, StudentScheduleDayEntity, StudentSchedulePairEntity>
    {
        /// <summary>
        /// Write student schedule to DB.
        /// </summary>
        /// <param name="studentSchedule">Student schedule entity.</param>
        /// <returns>Task.</returns>
        Task CreateStudentSchedule(StudentScheduleEntity studentSchedule);

        /// <summary>
        /// Soft delete student schedule from DB by setting IsDeleted flag to true.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <returns>Task.</returns>
        Task DeleteStudentSchedule(Guid scheduleId);

        /// <summary>
        /// Get all schedules for given user.
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>List of schedules for given student. Empty list if none are found.</returns>
        Task<IEnumerable<StudentScheduleEntity>> GetSchedulesForStudent(Guid userId);

        /// <summary>
        /// Create or update a pair in schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule unique identifier.</param>
        /// <param name="pairId">Pair identifier.</param>
        /// <param name="pair">Pair entity.</param>
        /// <returns>Updated student schedule.</returns>
        Task<StudentScheduleEntity> UpdatePair(Guid scheduleId, PairIdentifier pairId, StudentSchedulePairEntity pair);

        /// <summary>
        /// Hard delete pair from given student schedule.
        /// </summary>
        /// <param name="scheduleId">Student schedule unique identifier.</param>
        /// <param name="pairId">Pair identifier.</param>
        /// <returns>Task.</returns>
        Task DeletePair(Guid scheduleId, PairIdentifier pairId);
    }
}
