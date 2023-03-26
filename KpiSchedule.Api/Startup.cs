using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Common.Repositories;
using KpiSchedule.Services.Interfaces;
using KpiSchedule.Services;
using Serilog.Events;
using KpiSchedule.Common.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KpiSchedule.Services.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace KpiSchedule.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.Configure<TelegramOptions>(Configuration.GetSection("Telegram"));

            services
                .AddSerilogConsoleLogger(LogEventLevel.Information)
                .AddDynamoDbSchedulesRepository<IGroupSchedulesRepository, GroupSchedulesRepository, GroupScheduleEntity, GroupScheduleDayEntity, GroupSchedulePairEntity>(Configuration)
                .AddDynamoDbSchedulesRepository<ITeacherSchedulesRepository, TeacherSchedulesRepository, TeacherScheduleEntity, TeacherScheduleDayEntity, TeacherSchedulePairEntity>(Configuration)
                .AddDynamoDbSchedulesRepository<IStudentSchedulesRepository, StudentSchedulesRepository, StudentScheduleEntity, StudentScheduleDayEntity, StudentSchedulePairEntity>(Configuration)
                .AddScoped<ITelegramAuthenticationService, TelegramAuthenticationService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IGroupSchedulesService, GroupSchedulesService>()
                .AddScoped<ITeacherSchedulesService, TeacherSchedulesService>()
                .AddScoped<IStudentSchedulesService, StudentSchedulesService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.KeyBytes),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddAuthorization();

            services.AddMvc();
            services.AddRazorPages();
            services.AddControllers();

            services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid bearer token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.RoutePrefix = "";
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "KpiSchedule API v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
                e.MapRazorPages();
            });
        }
    }
}
