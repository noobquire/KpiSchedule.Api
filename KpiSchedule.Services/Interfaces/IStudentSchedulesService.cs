using KpiSchedule.Common.Entities;

namespace KpiSchedule.Services.Interfaces
{
    public interface IStudentSchedulesService
    {
        Task<StudentScheduleEntity> GetStudentScheduleById(Guid scheduleId);
        Task<IEnumerable<SubjectEntity>> GetSubjectsInStudentSchedule(Guid scheduleId);
        Task<StudentScheduleEntity> CreateStudentScheduleFromGroupSchedule(Guid groupScheduleId, IEnumerable<string> subjectNames);
        Task<IEnumerable<StudentScheduleEntity>> GetSchedulesForStudent(Guid userId);
        Task<StudentScheduleEntity> UpdatePair(Guid scheduleId, PairIdentifier pairId, StudentSchedulePairEntity pair);
        Task<StudentScheduleEntity> DeletePair(Guid scheduleId, PairIdentifier pairId);
        Task<StudentScheduleEntity> UpdateScheduleVisibility(Guid scheduleId, bool isPublic);
        Task DeleteSchedule(Guid scheduleId);
    }
}
