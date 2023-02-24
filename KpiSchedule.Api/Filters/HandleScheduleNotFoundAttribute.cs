using KpiSchedule.Api.Models.Responses;
using KpiSchedule.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace KpiSchedule.Api.Filters
{
    public class HandleScheduleNotFoundAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            
            if (context.Exception is not ScheduleNotFoundException)
            {
                return;
            }

            var response = new ErrorResponse()
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = context.Exception.Message
            };
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var responseJson = JsonSerializer.Serialize(response, serializerOptions);

            context.Result = new ContentResult()
            {
                Content = responseJson,
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
    }
}
