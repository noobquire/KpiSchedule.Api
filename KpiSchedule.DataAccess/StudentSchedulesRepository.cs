using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using KpiSchedule.DataAccess.Entities;
using KpiSchedule.DataAccess.Interfaces;

namespace KpiSchedule.DataAccess
{
    /// <summary>
    /// Student schedules repository which reads and writes student schedule data to/from DynamoDB.
    /// </summary>
    public class StudentSchedulesRepository : IStudentSchedulesRepository
    {
        private readonly IDynamoDBContext dynamoDbContext;

        /// <summary>
        /// Initialize a new instance of the <see cref="TeacherSchedulesRepository"/> class.
        /// </summary>
        /// <param name="dynamoDbContext">DynamoDbContext.</param>
        public StudentSchedulesRepository(IDynamoDBContext dynamoDbContext)
        {
            this.dynamoDbContext = dynamoDbContext;
        }

        public async Task CreateStudentSchedule(StudentScheduleEntity studentSchedule)
        {
            if (studentSchedule == null)
            {
                throw new ArgumentNullException(nameof(studentSchedule));
            }

            await dynamoDbContext.SaveAsync(studentSchedule);
        }

        public async Task DeletePair(Guid scheduleId, PairIdentifier pairId)
        {
            var schedule = await GetScheduleById(scheduleId);

            schedule.RemoveSchedulePair(pairId);

            await dynamoDbContext.SaveAsync(schedule);
        }

        public async Task DeleteStudentSchedule(Guid scheduleId)
        {
            var schedule = await GetScheduleById(scheduleId);

            schedule.IsDeleted = true;

            await dynamoDbContext.SaveAsync(schedule);
        }

        public async Task<StudentScheduleEntity> GetScheduleById(Guid scheduleId)
        {
            var schedule = await dynamoDbContext.LoadAsync<StudentScheduleEntity>(scheduleId);
            return schedule;
        }

        public async Task<IEnumerable<StudentScheduleEntity>> GetSchedulesForStudent(Guid userId)
        {
            var schedulesQuery = new QueryCondition("OwnerId", QueryOperator.Equal, userId);
            var results = await dynamoDbContext.QueryAsync<StudentScheduleEntity>(new[] { schedulesQuery }).GetRemainingAsync();
            return results;
        }

        public async Task<IEnumerable<SubjectEntity>> GetScheduleSubjects(Guid scheduleId)
        {
            var schedule = await GetScheduleById(scheduleId);

            var firstWeekSubjects = schedule.FirstWeek.SelectMany(d => d.Pairs).Select(p => p.Subject);
            var secondWeekSubjects = schedule.SecondWeek.SelectMany(d => d.Pairs).Select(p => p.Subject);

            var allSubjects = firstWeekSubjects.Concat(secondWeekSubjects);
            var uniqueSubjects = allSubjects.DistinctBy(s => s.SubjectName);

            return uniqueSubjects;
        }

        public async Task<StudentScheduleEntity> UpdatePair(Guid scheduleId, PairIdentifier pairId, StudentSchedulePairEntity pair)
        {
            var schedule = await GetScheduleById(scheduleId);

            schedule.UpdateSchedulePair(pairId, pair);

            await dynamoDbContext.SaveAsync(schedule);

            return schedule;
        }
    }
}
