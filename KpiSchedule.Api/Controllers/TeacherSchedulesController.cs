using KpiSchedule.Api.Filters;
using KpiSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{scheduleId}")]
        [HandleScheduleNotFound]
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
        [HttpGet("{scheduleId}/subjects")]
        [HandleScheduleNotFound]
        public async Task<IActionResult> GetSubjectsInTeacherSchedule([FromRoute] Guid scheduleId)
        {
            var subjects = await teacherSchedulesService.GetSubjectsInTeacherSchedule(scheduleId);
            return Ok(subjects);
        }

        /// <summary>
        /// Search teacher schedules by teacher name prefix.
        /// </summary>
        /// <param name="teacherNamePrefix">Teacher name prefix query.</param>
        /// <returns>Teacher schedules search results.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchTeacherSchedules([FromQuery] string teacherNamePrefix)
        {
            var results = await teacherSchedulesService.SearchTeacherSchedules(teacherNamePrefix);
            return Ok(results);
        }
    }
}
