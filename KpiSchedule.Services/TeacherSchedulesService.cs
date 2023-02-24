using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Exceptions;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Services.Interfaces;

namespace KpiSchedule.Services
{
    public class TeacherSchedulesService : ITeacherSchedulesService
    {
        private readonly ITeacherSchedulesRepository teacherSchedulesRepository;

        public TeacherSchedulesService(ITeacherSchedulesRepository teacherSchedulesRepository)
        {
            this.teacherSchedulesRepository = teacherSchedulesRepository;
        }

        public async Task<IEnumerable<SubjectEntity>> GetSubjectsInTeacherSchedule(Guid scheduleId)
        {
            var subjects = await teacherSchedulesRepository.GetScheduleSubjects(scheduleId);
            return subjects;
        }

        public async Task<TeacherScheduleEntity> GetTeacherScheduleById(Guid scheduleId)
        {
            var schedule = await teacherSchedulesRepository.GetScheduleById(scheduleId);
            if(schedule is null)
            {
                throw new ScheduleNotFoundException();
            }
            return schedule;
        }

        public async Task<IEnumerable<TeacherScheduleSearchResult>> SearchTeacherSchedules(string teacherNamePrefix)
        {
            // capitalize first letter
            var queryEnd = teacherNamePrefix.Length > 1 ? teacherNamePrefix.Substring(1) : string.Empty;
            var query = teacherNamePrefix[0].ToString().ToUpper() + queryEnd;

            var searchResults = await teacherSchedulesRepository.SearchTeacherSchedules(query);
            return searchResults;
        }
    }
}
