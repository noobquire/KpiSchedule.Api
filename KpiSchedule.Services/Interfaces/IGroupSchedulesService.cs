using KpiSchedule.Common.Entities;

namespace KpiSchedule.Services.Interfaces
{
    public interface IGroupSchedulesService
    {
        Task<GroupScheduleEntity> GetGroupScheduleById(Guid scheduleId);
        Task<IEnumerable<SubjectEntity>> GetSubjectsInGroupSchedule(Guid scheduleId);
        Task<IEnumerable<TeacherEntity>> GetTeachersInGroupSchedule(Guid scheduleId);
        Task<IEnumerable<GroupScheduleSearchResult>> SearchGroupSchedules(string groupNamePrefix);
    }
}
