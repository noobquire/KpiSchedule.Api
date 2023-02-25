using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Exceptions;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Services.Exceptions;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KpiSchedule.Services
{
    public class StudentSchedulesService : IStudentSchedulesService
    {
        private readonly IStudentSchedulesRepository studentSchedulesRepository;
        private readonly IGroupSchedulesRepository groupSchedulesRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StudentSchedulesService(
            IStudentSchedulesRepository studentSchedulesRepository,
            IGroupSchedulesRepository groupSchedulesRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.studentSchedulesRepository = studentSchedulesRepository;
            this.groupSchedulesRepository = groupSchedulesRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        private async Task<bool> GroupScheduleContainsAllSubjects(Guid groupScheduleId, IEnumerable<string> subjects)
        {
            var subjectsInGroupSchedule = await groupSchedulesRepository.GetScheduleSubjects(groupScheduleId);
            var scheduleSubjectNames = subjectsInGroupSchedule.Select(x => x.SubjectName);
            return subjects.All(s => scheduleSubjectNames.Contains(s));
        }

        public async Task<StudentScheduleEntity> CreateStudentScheduleFromGroupSchedule(Guid groupScheduleId, IEnumerable<string> subjectNames)
        {
            var groupSchedule = await groupSchedulesRepository.GetScheduleById(groupScheduleId);
            if (groupSchedule is null)
            {
                throw new ScheduleNotFoundException("Provided group schedule was not found.");
            }

            if (!await GroupScheduleContainsAllSubjects(groupScheduleId, subjectNames))
            {
                throw new KpiScheduleServiceException("Provided group schedule does not contain all the provieded subjects.");
            }

            var studentSchedule = new StudentScheduleEntity()
            {
                IsPrivate = true,
                ScheduleId = Guid.NewGuid(),

            };
            return studentSchedule;
        }

        public Task<StudentScheduleEntity> DeletePair(Guid scheduleId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSchedule(Guid scheduleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentScheduleEntity>> GetSchedulesForStudent(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<StudentScheduleEntity> GetStudentScheduleById(Guid scheduleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubjectEntity>> GetSubjectsInStudentSchedule(Guid scheduleId)
        {
            throw new NotImplementedException();
        }

        public Task<StudentScheduleEntity> UpdatePair(Guid scheduleId, PairIdentifier pairId, StudentSchedulePairEntity pair)
        {
            throw new NotImplementedException();
        }

        public Task<StudentScheduleEntity> UpdateScheduleVisibility(Guid scheduleId, bool isPublic)
        {
            throw new NotImplementedException();
        }
    }
}
