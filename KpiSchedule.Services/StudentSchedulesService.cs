using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Exceptions;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Services.Authorization;
using KpiSchedule.Services.Exceptions;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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

            var userId = GetUserId();

            var studentSchedule = new StudentScheduleEntity()
            {
                IsPublic = false,
                ScheduleId = Guid.NewGuid(),
                FirstWeek = FilterGroupScheduleWeek(groupSchedule.FirstWeek, subjectNames),
                SecondWeek = FilterGroupScheduleWeek(groupSchedule.SecondWeek, subjectNames),
                OwnerId = userId
            };

            await studentSchedulesRepository.PutSchedule(studentSchedule);

            return studentSchedule;
        }

        public async Task<StudentScheduleEntity> DeletePair(Guid scheduleId, PairIdentifier pairId)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeOperation(StudentScheduleRequirements.ReadSchedule, schedule);
            try
            {
                schedule.RemoveSchedulePair(pairId);
            }
            catch (ArgumentException ex)
            {
                throw new KpiScheduleServiceException($"Unable to delete pair from schedule: {ex.Message}");
            }

            await studentSchedulesRepository.PutSchedule(schedule);
            return schedule;
        }

        public async Task DeleteSchedule(Guid scheduleId)
        {
            await studentSchedulesRepository.DeleteSchedule(scheduleId);
        }

        public async Task<IEnumerable<StudentScheduleSearchResult>> GetSchedulesForStudent(string userId)
        {
            var schedules = await studentSchedulesRepository.GetSchedulesForStudent(userId);
            var currentUserId = GetUserId();
            // if schedules belong to current user, just return them
            // if they belong to another user, only return public schedules
            return userId == currentUserId ? schedules : schedules.Where(s => s.IsPublic);
        }

        public async Task<StudentScheduleEntity> GetStudentScheduleById(Guid scheduleId)
        {
            var schedule = await studentSchedulesRepository.GetScheduleById(scheduleId);
            await AuthorizeOperation(StudentScheduleRequirements.ReadSchedule, schedule);

            return schedule;
        }

        public async Task<IEnumerable<SubjectEntity>> GetSubjectsInStudentSchedule(Guid scheduleId)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeOperation(StudentScheduleRequirements.ReadSchedule, schedule);

            var subjects = await studentSchedulesRepository.GetScheduleSubjects(scheduleId);
            return subjects;
        }

        public async Task<StudentScheduleEntity> UpdatePair(Guid scheduleId, PairIdentifier pairId, StudentSchedulePairEntity pair)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeOperation(StudentScheduleRequirements.WriteSchedule, schedule);
            schedule.UpdateSchedulePair(pairId, pair);
            await studentSchedulesRepository.PutSchedule(schedule);
            return schedule;
        }

        public async Task<StudentScheduleEntity> UpdateScheduleVisibility(Guid scheduleId, bool isPublic)
        {
            var schedule = await GetStudentScheduleById(scheduleId);
            await AuthorizeOperation(StudentScheduleRequirements.WriteSchedule, schedule);
            schedule.IsPublic = isPublic;
            await studentSchedulesRepository.PutSchedule(schedule);
            return schedule;
        }

        private async Task<bool> GroupScheduleContainsAllSubjects(Guid groupScheduleId, IEnumerable<string> subjects)
        {
            var subjectsInGroupSchedule = await groupSchedulesRepository.GetScheduleSubjects(groupScheduleId);
            var scheduleSubjectNames = subjectsInGroupSchedule.Select(x => x.SubjectName);
            return subjects.All(s => scheduleSubjectNames.Contains(s));
        }

        private List<StudentScheduleDayEntity> FilterGroupScheduleWeek(IEnumerable<GroupScheduleDayEntity> groupScheduleWeek, IEnumerable<string> subjectNames)
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

        private async Task AuthorizeOperation(OperationAuthorizationRequirement operation, StudentScheduleEntity studentSchedule)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(user, studentSchedule, operation);
            if (!authorizationResult.Succeeded)
            {
                throw new ScheduleOperationUnauthorizedException(operation);
            }
        }

        private string GetUserId()
        {
            var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            return userId;
        }
    }
}
