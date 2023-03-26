using KpiSchedule.Api.Models.Responses;
using KpiSchedule.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace KpiSchedule.Api.Filters
{
    public class HandleInvalidAuthenticationAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is not InvalidAuthenticationException)
            {
                return;
            }

            var response = new ErrorResponse()
            {
                StatusCode = HttpStatusCode.Unauthorized,
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
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }
    }
}
