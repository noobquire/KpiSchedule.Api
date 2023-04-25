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
        /// <param name="scheduleId">Schedule UUID.</param>
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

        [HttpPost]
        [HandleScheduleNotFoundException]
        [HandleScheduleServiceException]
        public async Task<IActionResult> CreateStudentSchedule([FromBody] CreateStudentScheduleRequest request)
        {
            var studentSchedule = await studentSchedulesService.CreateStudentScheduleFromGroupSchedule(request.GroupScheduleId, request.SubjectNames);
            return Ok(studentSchedule);
        }

        [HttpDelete("{scheduleId}")]
        [HandleScheduleNotFoundException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> DeleteSchedule([FromRoute] Guid scheduleId)
        {
            await studentSchedulesService.DeleteSchedule(scheduleId);
            return NoContent();
        }

        [HttpDelete("{scheduleId}/pair")]
        [HandleScheduleNotFoundException]
        [HandleScheduleServiceException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> DeleteSchedulePair([FromRoute] Guid scheduleId, [FromQuery] DeleteSchedulePairRequest request)
        {
            var pairId = new PairIdentifier
            {
                WeekNumber = request.WeekNumber,
                DayNumber = request.DayNumber,
                PairNumber = request.PairNumber,
                DuplicatePairNumber = request.DuplicatePairNumber
            };
            var schedule = await studentSchedulesService.DeletePair(scheduleId, pairId);
            return Ok(schedule);
        }

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

        [HttpGet("~/student/{studentId}/schedules")]
        public async Task<IActionResult> GetSchedulesForStudent([FromRoute] string studentId)
        {
            var schedules = await studentSchedulesService.GetSchedulesForStudent(studentId);
            return Ok(schedules);
        }

        [HttpPatch("{scheduleId}/visibility")]
        [HandleScheduleNotFoundException]
        [HandleScheduleOperationUnauthorizedException]
        public async Task<IActionResult> UpdateScheduleVisibility([FromRoute] Guid scheduleId, [FromQuery] bool isPublic)
        {
            var schedule = await studentSchedulesService.UpdateScheduleVisibility(scheduleId, isPublic);
            return Ok(schedule);
        }
    }
}
