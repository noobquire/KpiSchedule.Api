using AutoMapper;
using KpiSchedule.Common.Repositories;
using KpiSchedule.DataAccess.Entities;
using KpiSchedule.DataAccess.Interfaces;

namespace KpiSchedule.DataAccess
{
    /// <summary>
    /// Teacher schedules repository that wraps roz.kpi.ua teacher schedules extracted via ETL.
    /// </summary>
    public class TeacherSchedulesRepository : ITeacherSchedulesRepository
    {
        private readonly RozKpiTeacherSchedulesRepository rozKpiTeacherSchedules;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialize a new instance of the <see cref="TeacherSchedulesRepository"/> class.
        /// </summary>
        /// <param name="rozKpiTeacherSchedules">roz.kpi.ua teacher schedules repository.</param>
        /// <param name="mapper">Mapping interface.</param>
        public TeacherSchedulesRepository(RozKpiTeacherSchedulesRepository rozKpiTeacherSchedules, IMapper mapper)
        {
            this.rozKpiTeacherSchedules = rozKpiTeacherSchedules;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<TeacherScheduleEntity> GetScheduleById(Guid scheduleId)
        {
            var rozKpiSchedule = await rozKpiTeacherSchedules.GetScheduleById(scheduleId);

            var mappedSchedule = mapper.Map<TeacherScheduleEntity>(rozKpiSchedule);

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
        public async Task<IEnumerable<TeacherEntity>> SearchTeacherSchedules(string teacherNamePrefix)
        {
            var rozKpiSchedules = await rozKpiTeacherSchedules.SearchScheduleId(teacherNamePrefix);

            var mappedSchedules = mapper.Map<IEnumerable<TeacherEntity>>(rozKpiSchedules);

            return mappedSchedules;
        }
    }
}
