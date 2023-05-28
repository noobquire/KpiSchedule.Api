using KpiSchedule.Api.Filters;
using KpiSchedule.Api.Models.Responses;
using KpiSchedule.Common.Entities.Base;
using KpiSchedule.Common.Entities.Group;
using KpiSchedule.Common.Entities.Teacher;
using KpiSchedule.Services.Interfaces;
using KpiSchedule.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KpiSchedule.Api.Controllers
{
    /// <summary>
    /// Academic group schedule related actions.
    /// </summary>
    [Route("schedules/group")]
    [ResponseCache(Duration = 600)]
    [ApiController]
    public class GroupSchedulesController : ControllerBase
    {
        private readonly IGroupSchedulesService groupSchedulesService;

        public GroupSchedulesController(IGroupSchedulesService groupSchedulesService)
        {
            this.groupSchedulesService = groupSchedulesService;
        }

        /// <summary>
        /// Get group schedule data by ID.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <returns>Schedule data.</returns>
        /// <response code="404">Schedule not found.</response>
        /// <response code="200">Schedule data.</response>
        [HttpGet("{scheduleId}")]
        [ProducesResponseType(typeof(GroupScheduleEntity), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HandleScheduleNotFoundException]
        public async Task<IActionResult> GetGroupSchedule([FromRoute] Guid scheduleId)
        {
            var schedule = await groupSchedulesService.GetGroupScheduleById(scheduleId);
            return Ok(schedule);
        }

        /// <summary>
        /// Get subjects in group schedule by schedule ID.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <returns>Subjects in group schedule.</returns>
        /// <response code="404">Schedule not found.</response>
        /// <response code="200">Subjects in group schedule.</response>
        [HttpGet("{scheduleId}/subjects")]
        [ProducesResponseType(typeof(IEnumerable<SubjectEntity>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HandleScheduleNotFoundException]
        public async Task<IActionResult> GetSubjectsInGroupSchedule([FromRoute] Guid scheduleId)
        {
            var subjects = await groupSchedulesService.GetSubjectsInGroupSchedule(scheduleId);
            return Ok(subjects);
        }

        /// <summary>
        /// Get teachers in group schedule by schedule ID.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <returns>Teachers in schedule.</returns>
        /// <response code="404">Schedule not found.</response>
        /// <response code="200">Subjects in group schedule.</response>
        [HttpGet("{scheduleId}/teachers")]
        [ProducesResponseType(typeof(IEnumerable<TeacherEntity>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HandleScheduleNotFoundException]
        public async Task<IActionResult> GetTeachersInGroupSchedule([FromRoute] Guid scheduleId)
        {
            var teachers = await groupSchedulesService.GetTeachersInGroupSchedule(scheduleId);
            return Ok(teachers);
        }

        /// <summary>
        /// Search group schedules by group name prefix.
        /// </summary>
        /// <param name="groupNamePrefix">Group name prefix query.</param>
        /// <param name="maxResults"></param>
        /// <returns>Group schedules search results.</returns>
        /// <response code="200">List of search results. Empty if nothing is found.</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<GroupScheduleSearchResult>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> SearchGroupSchedules([FromQuery] string groupNamePrefix, [FromQuery, Range(1, 30)] int maxResults = 10)
        {
            var results = (await groupSchedulesService.SearchGroupSchedules(groupNamePrefix))
                .Take(maxResults);
            return Ok(results);
        }
    }
}
