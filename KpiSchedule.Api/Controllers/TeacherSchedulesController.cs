using KpiSchedule.Api.Filters;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KpiSchedule.Api.Controllers
{
    [Route("schedules/teacher")]
    [ApiController]
    public class TeacherSchedulesController : ControllerBase
    {
        private readonly ITeacherSchedulesService teacherSchedulesService;

        public TeacherSchedulesController(ITeacherSchedulesService teacherSchedulesService)
        {
            this.teacherSchedulesService = teacherSchedulesService;
        }

        [HttpGet("{scheduleId}")]
        [HandleScheduleNotFound]
        public async Task<IActionResult> GetTeacherSchedule([FromRoute] Guid scheduleId)
        {
            var schedule = await teacherSchedulesService.GetTeacherScheduleById(scheduleId);
            return Ok(schedule);
        }

        [HttpGet("{scheduleId}/subjects")]
        [HandleScheduleNotFound]
        public async Task<IActionResult> GetSubjectsInTeacherSchedule([FromRoute] Guid scheduleId)
        {
            var subjects = await teacherSchedulesService.GetSubjectsInTeacherSchedule(scheduleId);
            return Ok(subjects);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTeacherSchedules([FromQuery] string query)
        {
            var results = await teacherSchedulesService.SearchTeacherSchedules(query);
            return Ok(results);
        }
    }
}
