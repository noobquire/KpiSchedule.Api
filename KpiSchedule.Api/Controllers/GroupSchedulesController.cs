using KpiSchedule.Api.Filters;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KpiSchedule.Api.Controllers
{
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
        /// <param name="scheduleId">Schedule UUID.</param>
        /// <returns>Schedule data.</returns>
        [HttpGet("{scheduleId}")]
        [HandleScheduleNotFoundException]
        public async Task<IActionResult> GetGroupSchedule([FromRoute] Guid scheduleId)
        {
            var schedule = await groupSchedulesService.GetGroupScheduleById(scheduleId);
            return Ok(schedule);
        }

        /// <summary>
        /// Get subjects in group schedule by schedule ID.
        /// </summary>
        /// <param name="scheduleId">Schedule UUID.</param>
        /// <returns>Subjects in schedule.</returns>
        [HttpGet("{scheduleId}/subjects")]
        [HandleScheduleNotFoundException]
        public async Task<IActionResult> GetSubjectsInGroupSchedule([FromRoute] Guid scheduleId)
        {
            var subjects = await groupSchedulesService.GetSubjectsInGroupSchedule(scheduleId);
            return Ok(subjects);
        }

        /// <summary>
        /// Get teachers in group schedule by schedule ID.
        /// </summary>
        /// <param name="scheduleId">Schedule UUID.</param>
        /// <returns>Teachers in schedule.</returns>
        [HttpGet("{scheduleId}/teachers")]
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
        /// <returns>Group schedules search results.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchGroupSchedules([FromQuery] string groupNamePrefix, [FromQuery] int maxResults = 10)
        {
            var results = (await groupSchedulesService.SearchGroupSchedules(groupNamePrefix))
                .Take(maxResults);
            return Ok(results);
        }
    }
}
