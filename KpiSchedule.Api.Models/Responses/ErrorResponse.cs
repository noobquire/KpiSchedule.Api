using System.Net;

namespace KpiSchedule.Api.Models.Responses
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
