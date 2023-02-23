using AutoMapper;
using KpiSchedule.Common.Repositories;
using KpiSchedule.DataAccess.Entities;
using KpiSchedule.DataAccess.Interfaces;

namespace KpiSchedule.DataAccess
{
    /// <summary>
    /// Group schedules repository that wraps roz.kpi.ua group schedules extracted via ETL.
    /// </summary>
    public class GroupSchedulesRepository : IGroupSchedulesRepository
    {
        private readonly RozKpiGroupSchedulesRepository rozKpiGroupSchedules;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialize a new instance of the <see cref="GroupSchedulesRepository"/> class.
        /// </summary>
        /// <param name="rozKpiGroupSchedules">roz.kpi.ua group schedules repository.</param>
        /// <param name="mapper">Mapping interface.</param>
        public GroupSchedulesRepository(RozKpiGroupSchedulesRepository rozKpiGroupSchedules, IMapper mapper)
        {
            this.rozKpiGroupSchedules = rozKpiGroupSchedules;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<GroupScheduleEntity> GetScheduleById(Guid scheduleId)
        {
            var rozKpiSchedule = await rozKpiGroupSchedules.GetScheduleById(scheduleId);

            var mappedSchedule = mapper.Map<GroupScheduleEntity>(rozKpiSchedule);

            return mappedSchedule;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SubjectEntity>> GetScheduleSubjects(Guid scheduleId)
        {
            var schedule = await GetScheduleById(scheduleId);

            var firstWeekSubjects = schedule.FirstWeek.SelectMany(d => d.Pairs).Select(p => p.Subject);
            var secondWeekSubjects = schedule.SecondWeek.SelectMany(d => d.Pairs).Select(p => p.Subject);

            var allSubjects = firstWeekSubjects.Concat(secondWeekSubjects);
            var uniqueSubjects = allSubjects.DistinctBy(s => s.SubjectName);

            return uniqueSubjects;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<GroupEntity>> SearchGroupSchedules(string groupNamePrefix)
        {
            var rozKpiSchedules = await rozKpiGroupSchedules.SearchScheduleId(groupNamePrefix);

            var mappedSchedules = mapper.Map<IEnumerable<GroupEntity>>(rozKpiSchedules);

            return mappedSchedules;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TeacherEntity>> GetTeachersInGroupSchedule(Guid groupScheduleId)
        {
            var schedule = await GetScheduleById(groupScheduleId);

            var firstWeekTeachers = schedule.FirstWeek.SelectMany(d => d.Pairs).SelectMany(p => p.Teachers);
            var secondWeekTeachers = schedule.SecondWeek.SelectMany(d => d.Pairs).SelectMany(p => p.Teachers);

            var allTeachers = firstWeekTeachers.Concat(secondWeekTeachers);
            var uniqueTeachers = allTeachers.DistinctBy(s => s.ScheduleId);

            return uniqueTeachers;
        }
    }
}
