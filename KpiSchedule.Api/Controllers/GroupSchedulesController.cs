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

        [HttpGet("{scheduleId}")]
        [HandleScheduleNotFound]
        public async Task<IActionResult> GetGroupSchedule([FromRoute] Guid scheduleId)
        {
            var schedule = await groupSchedulesService.GetGroupScheduleById(scheduleId);
            return Ok(schedule);
        }

        [HttpGet("{scheduleId}/subjects")]
        [HandleScheduleNotFound]
        public async Task<IActionResult> GetSubjectsInGroupSchedule([FromRoute] Guid scheduleId)
        {
            var subjects = await groupSchedulesService.GetSubjectsInGroupSchedule(scheduleId);
            return Ok(subjects);
        }

        [HttpGet("{scheduleId}/teachers")]
        [HandleScheduleNotFound]
        public async Task<IActionResult> GetTeachersInGroupSchedule([FromRoute] Guid scheduleId)
        {
            var teachers = await groupSchedulesService.GetTeachersInGroupSchedule(scheduleId);
            return Ok(teachers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchGroupSchedules([FromQuery] string query)
        {
            var results = await groupSchedulesService.SearchGroupSchedules(query);
            return Ok(results);
        }
    }
}
