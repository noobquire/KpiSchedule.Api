using KpiSchedule.Api.Filters;
using KpiSchedule.Api.Mappers;
using KpiSchedule.Api.Models.Requests;
using KpiSchedule.Common.Models;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KpiSchedule.Api.Controllers
{
    [Route("schedules/student")]
    [Authorize]
    [ApiController]
    public class StudentSchedulesController : ControllerBase
    {
        private readonly IStudentSchedulesService studentSchedulesService;

        public StudentSchedulesController(IStudentSchedulesService studentSchedulesService)
        {
            this.studentSchedulesService = studentSchedulesService;
        }

        /// <summary>
        /// Get student schedule data by ID.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <returns>Schedule data.</returns>
        [HttpGet("{scheduleId}")]
        [HandleScheduleNotFoundException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> GetStudentSchedule([FromRoute] Guid scheduleId)
        {
            var schedule = await studentSchedulesService.GetStudentScheduleById(scheduleId);
            return Ok(schedule);
        }

        /// <summary>
        /// Get subjects in student schedule by schedule ID.
        /// </summary>
        /// <param name="scheduleId">Schedule UUID.</param>
        /// <returns>Subjects in schedule.</returns>
        [HttpGet("{scheduleId}/subjects")]
        [HandleScheduleNotFoundException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> GetSubjectsInStudentSchedule([FromRoute] Guid scheduleId)
        {
            var subjects = await studentSchedulesService.GetSubjectsInStudentSchedule(scheduleId);
            return Ok(subjects);
        }

        /// <summary>
        /// Create student schedule from group schedule.
        /// </summary>
        /// <param name="request">Create schedule request data.</param>
        /// <returns>Created student schedule.</returns>
        [HttpPost]
        [HandleScheduleNotFoundException]
        [HandleScheduleServiceException]
        public async Task<IActionResult> CreateStudentSchedule([FromBody] CreateStudentScheduleRequest request)
        {
            var studentSchedule = await studentSchedulesService.CreateStudentScheduleFromGroupSchedule(request.GroupScheduleId, request.SubjectNames, request.ScheduleName);
            return Ok(studentSchedule);
        }

        /// <summary>
        /// Deletes student schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule ID to delete.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{scheduleId}")]
        [HandleScheduleNotFoundException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> DeleteSchedule([FromRoute] Guid scheduleId)
        {
            await studentSchedulesService.DeleteSchedule(scheduleId);
            return NoContent();
        }

        /// <summary>
        /// Deletes pair in student schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <param name="request">Pair identifier data.</param>
        /// <returns>Updated student schedule.</returns>
        [HttpDelete("{scheduleId}/pair")]
        [HandleScheduleNotFoundException]
        [HandleScheduleServiceException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> DeleteSchedulePair([FromRoute] Guid scheduleId, [FromQuery] DeleteSchedulePairRequest request)
        {
            var pairId = new PairIdentifier
            {
                WeekNumber = request.Week,
                DayNumber = request.Day,
                PairNumber = request.Pair,
                DuplicatePairNumber = request.DupPair
            };
            var schedule = await studentSchedulesService.DeletePair(scheduleId, pairId);
            return Ok(schedule);
        }

        /// <summary>
        /// Updates student schedule pair data.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <param name="request">Pair identifier and updated data./param>
        /// <returns>Updated student schedule.</returns>
        [HttpPut("{scheduleId}/pair")]
        [HandleScheduleNotFoundException]
        [HandleScheduleServiceException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> UpdateSchedulePair([FromRoute] Guid scheduleId, [FromBody] UpdateSchedulePairRequest request)
        {
            var pairEntity = request.MapToEntity();
            var updatedSchedule = await studentSchedulesService.UpdatePair(scheduleId, request.PairId, pairEntity);
            return Ok(updatedSchedule);
        }

        /// <summary>
        /// Get schedules for specified user.
        /// If user is currently logged in, returns all of their schedules.
        /// If requesting for another user, only returns their public schedules.
        /// </summary>
        /// <param name="studentId">Student user ID.</param>
        /// <returns>Schedule search results.</returns>
        [HttpGet("~/student/{studentId}/schedules")]
        public async Task<IActionResult> GetSchedulesForStudent([FromRoute] string studentId)
        {
            var schedules = await studentSchedulesService.GetSchedulesForStudent(studentId);
            return Ok(schedules);
        }

        /// <summary>
        /// Updates student schedule visibility flag.
        /// If set to true, all users can see this schedule.
        /// If set to false, only owner can see this schedule.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <param name="request">Schedule visibility request data.</param>
        /// <returns>Updated student schedule.</returns>
        [HttpPut("{scheduleId}/visibility")]
        [HandleScheduleNotFoundException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> UpdateScheduleVisibility([FromRoute] Guid scheduleId, [FromBody] UpdateScheduleVisibilityRequest request)
        {
            var schedule = await studentSchedulesService.UpdateScheduleVisibility(scheduleId, request.IsPublic);
            return Ok(schedule);
        }

        /// <summary>
        /// Update student schedule name.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <param name="request">Schedule name change data.</param>
        /// <returns>Updated student schedule.</returns>
        [HttpPut("{scheduleId}/name")]
        [HandleScheduleNotFoundException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> UpdateScheduleName([FromRoute] Guid scheduleId, [FromBody] UpdateScheduleNameRequest request)
        {
            var schedule = await studentSchedulesService.UpdateScheduleName(scheduleId, request.ScheduleName);
            return Ok(schedule);
        }
    }
}
