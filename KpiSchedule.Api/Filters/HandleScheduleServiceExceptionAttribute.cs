using KpiSchedule.Api.Models.Responses;
using KpiSchedule.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace KpiSchedule.Api.Filters
{
    public class HandleScheduleServiceExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not KpiScheduleServiceException)
            {
                return;
            }

            var response = new ErrorResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
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
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
