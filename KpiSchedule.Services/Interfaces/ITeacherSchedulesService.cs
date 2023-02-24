using KpiSchedule.Common.Entities;

namespace KpiSchedule.Services.Interfaces
{
    public interface ITeacherSchedulesService
    {
        Task<TeacherScheduleEntity> GetTeacherScheduleById(Guid scheduleId);
        Task<IEnumerable<SubjectEntity>> GetSubjectsInTeacherSchedule(Guid scheduleId);
        Task<IEnumerable<TeacherScheduleSearchResult>> SearchTeacherSchedules(string teacherNamePrefix);
    }
}
