using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Exceptions;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Services.Interfaces;

namespace KpiSchedule.Services
{
    public class GroupSchedulesService : IGroupSchedulesService
    {
        private readonly IGroupSchedulesRepository groupSchedulesRepository;

        public GroupSchedulesService(IGroupSchedulesRepository groupSchedulesRepository)
        {
            this.groupSchedulesRepository = groupSchedulesRepository;
        }

        public async Task<GroupScheduleEntity> GetGroupScheduleById(Guid scheduleId)
        {
            var schedule = await groupSchedulesRepository.GetScheduleById(scheduleId);
            if(schedule is null)
            {
                throw new ScheduleNotFoundException();
            }
            return schedule;
        }

        public async Task<IEnumerable<SubjectEntity>> GetSubjectsInGroupSchedule(Guid scheduleId)
        {
            var subjects = await groupSchedulesRepository.GetScheduleSubjects(scheduleId);

            return subjects;
        }

        public async Task<IEnumerable<TeacherEntity>> GetTeachersInGroupSchedule(Guid scheduleId)
        {
            var teachers = await groupSchedulesRepository.GetTeachersInGroupSchedule(scheduleId);

            return teachers;
        }

        public async Task<IEnumerable<GroupScheduleSearchResult>> SearchGroupSchedules(string groupNamePrefix)
        {
            var uppercasePrefix = groupNamePrefix.ToUpper();
            var results = await groupSchedulesRepository.SearchGroupSchedules(uppercasePrefix);

            return results;
        }
    }
}
