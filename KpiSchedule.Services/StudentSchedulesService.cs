using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Exceptions;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Services.Authorization;
using KpiSchedule.Services.Exceptions;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KpiSchedule.Services
{
    public class StudentSchedulesService : IStudentSchedulesService
    {
        private readonly IStudentSchedulesRepository studentSchedulesRepository;
        private readonly IGroupSchedulesRepository groupSchedulesRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthorizationService authorizationService;
        private readonly ClaimsPrincipal user;

        public StudentSchedulesService(
            IStudentSchedulesRepository studentSchedulesRepository,
            IGroupSchedulesRepository groupSchedulesRepository,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            this.studentSchedulesRepository = studentSchedulesRepository;
            this.groupSchedulesRepository = groupSchedulesRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.authorizationService = authorizationService;
            
            this.user = httpContextAccessor.HttpContext.User;
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
                FirstWeek = FilterScheduleWeek(groupSchedule.FirstWeek, subjectNames),
                SecondWeek = FilterScheduleWeek(groupSchedule.SecondWeek, subjectNames)
            };

            await studentSchedulesRepository.PutSchedule(studentSchedule);

            return studentSchedule;
        }

        public async Task<StudentScheduleEntity> DeletePair(Guid scheduleId, PairIdentifier pairId)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeWriteOperation(schedule);
            schedule.RemoveSchedulePair(pairId);
            await studentSchedulesRepository.PutSchedule(schedule);
            return schedule;
        }

        public async Task DeleteSchedule(Guid scheduleId)
        {
            await studentSchedulesRepository.DeleteSchedule(scheduleId);
        }

        public Task<IEnumerable<StudentScheduleEntity>> GetSchedulesForStudent(Guid userId)
        {
            var schedules = studentSchedulesRepository.GetSchedulesForStudent(userId);
            return schedules;
        }

        public async Task<StudentScheduleEntity> GetStudentScheduleById(Guid scheduleId)
        {
            var schedule = await studentSchedulesRepository.GetScheduleById(scheduleId);
            await AuthorizeReadOperation(schedule);

            return schedule;
        }

        public async Task<IEnumerable<SubjectEntity>> GetSubjectsInStudentSchedule(Guid scheduleId)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeReadOperation(schedule);

            var subjects = await studentSchedulesRepository.GetScheduleSubjects(scheduleId);
            return subjects;
        }

        public async Task<StudentScheduleEntity> UpdatePair(Guid scheduleId, PairIdentifier pairId, StudentSchedulePairEntity pair)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeWriteOperation(schedule);
            schedule.UpdateSchedulePair(pairId, pair);
            await studentSchedulesRepository.PutSchedule(schedule);
            return schedule;
        }

        public async Task<StudentScheduleEntity> UpdateScheduleVisibility(Guid scheduleId, bool isPublic)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeWriteOperation(schedule);
            schedule.IsPrivate = isPublic;
            await studentSchedulesRepository.PutSchedule(schedule);
            return schedule;
        }

        private async Task<bool> GroupScheduleContainsAllSubjects(Guid groupScheduleId, IEnumerable<string> subjects)
        {
            var subjectsInGroupSchedule = await groupSchedulesRepository.GetScheduleSubjects(groupScheduleId);
            var scheduleSubjectNames = subjectsInGroupSchedule.Select(x => x.SubjectName);
            return subjects.All(s => scheduleSubjectNames.Contains(s));
        }

        private List<StudentScheduleDayEntity> FilterScheduleWeek(IEnumerable<GroupScheduleDayEntity> groupScheduleWeek, IEnumerable<string> subjectNames)
        {
            var studentScheduleWeek = groupScheduleWeek.Select(d => new StudentScheduleDayEntity()
            {
                DayNumber = d.DayNumber,
                Pairs = d.Pairs.Where(p => subjectNames.Contains(p.Subject.SubjectName)).Select(p => new StudentSchedulePairEntity()
                {
                    Teachers = p.Teachers,
                    OnlineConferenceUrl = string.Empty,
                    PairNumber = p.PairNumber,
                    StartTime = p.StartTime,
                    EndTime = p.EndTime,
                    PairType = p.PairType,
                    IsOnline = p.IsOnline,
                    Subject = p.Subject,
                    Rooms = p.Rooms
                }).ToList()
            }).ToList();
            return studentScheduleWeek;
        }

        private async Task AuthorizeReadOperation(StudentScheduleEntity studentSchedule)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(user, studentSchedule, StudentScheduleRequirements.ReadSchedule);
            if (!authorizationResult.Succeeded)
            {
                throw new ScheduleOperationUnauthorizedException(StudentScheduleRequirements.ReadSchedule);
            }
        }

        private async Task AuthorizeWriteOperation(StudentScheduleEntity studentSchedule)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(user, studentSchedule, StudentScheduleRequirements.WriteSchedule);
            if (!authorizationResult.Succeeded)
            {
                throw new ScheduleOperationUnauthorizedException(StudentScheduleRequirements.WriteSchedule);
            }
        }
    }
}
