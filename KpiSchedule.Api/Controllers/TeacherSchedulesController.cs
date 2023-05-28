using KpiSchedule.Api.Filters;
using KpiSchedule.Api.Models.Responses;
using KpiSchedule.Common.Entities.Base;
using KpiSchedule.Common.Entities.Teacher;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KpiSchedule.Api.Controllers
{
    [Route("schedules/teacher")]
    [ResponseCache(Duration = 600)]
    [ApiController]
    public class TeacherSchedulesController : ControllerBase
    {
        private readonly ITeacherSchedulesService teacherSchedulesService;

        public TeacherSchedulesController(ITeacherSchedulesService teacherSchedulesService)
        {
            this.teacherSchedulesService = teacherSchedulesService;
        }

        /// <summary>
        /// Get teacher schedule data by ID.
        /// </summary>
        /// <param name="scheduleId">Schedule UUID.</param>
        /// <returns>Schedule data.</returns>
        /// <response code="404">Schedule not found.</response>
        /// <response code="200">Schedule data.</response>
        [HttpGet("{scheduleId}")]
        [HandleScheduleNotFoundException]
        [ProducesResponseType(typeof(TeacherScheduleEntity), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetTeacherSchedule([FromRoute] Guid scheduleId)
        {
            var schedule = await teacherSchedulesService.GetTeacherScheduleById(scheduleId);
            return Ok(schedule);
        }

        /// <summary>
        /// Get subjects in teacher schedule by schedule ID.
        /// </summary>
        /// <param name="scheduleId">Schedule UUID.</param>
        /// <returns>Subjects in schedule.</returns>
        /// <response code="404">Schedule not found.</response>
        /// <response code="200">Subjects in schedule.</response>
        [HttpGet("{scheduleId}/subjects")]
        [HandleScheduleNotFoundException]
        [ProducesResponseType(typeof(IEnumerable<SubjectEntity>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetSubjectsInTeacherSchedule([FromRoute] Guid scheduleId)
        {
            var subjects = await teacherSchedulesService.GetSubjectsInTeacherSchedule(scheduleId);
            return Ok(subjects);
        }

        /// <summary>
        /// Search teacher schedules by teacher name prefix.
        /// </summary>
        /// <param name="teacherNamePrefix">Teacher name prefix query.</param>
        /// <param name="maxResults">Max number of results.</param>
        /// <returns>Teacher schedules search results.</returns>
        /// <response code="200">Teacher schedule search results. Empty if none are found.</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<TeacherScheduleSearchResult>), 200)]
        public async Task<IActionResult> SearchTeacherSchedules([FromQuery] string teacherNamePrefix, [FromQuery, Range(1, 30)] int maxResults = 10)
        {
            var results = (await teacherSchedulesService.SearchTeacherSchedules(teacherNamePrefix))
                .Take(maxResults);
            return Ok(results);
        }
    }
}
