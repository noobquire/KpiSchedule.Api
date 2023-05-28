using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KpiSchedule.Api.Filters
{
    public class AuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool methodIsAuthorized = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
            bool methodIsAnonymous = context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();
            bool controllerIsAuthorized = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
            bool displayAuthorization = methodIsAuthorized || (controllerIsAuthorized && !methodIsAnonymous);

            if (displayAuthorization)
            {

                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Operation is unauthorized." });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Operation is forbidden." });

                var jwtbearerScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ jwtbearerScheme ] = new string [] { }
                    }
                };
            }
        }
    }
}
