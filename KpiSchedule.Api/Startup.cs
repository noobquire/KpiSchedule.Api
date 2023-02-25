using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Common.Repositories;
using KpiSchedule.Services.Interfaces;
using KpiSchedule.Services;
using Serilog.Events;
using KpiSchedule.Common.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Builder;

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
            services
                .AddSerilogConsoleLogger(LogEventLevel.Information)
                .AddDynamoDbSchedulesRepository<IGroupSchedulesRepository, GroupSchedulesRepository, GroupScheduleEntity, GroupScheduleDayEntity, GroupSchedulePairEntity>(Configuration)
                .AddDynamoDbSchedulesRepository<ITeacherSchedulesRepository, TeacherSchedulesRepository, TeacherScheduleEntity, TeacherScheduleDayEntity, TeacherSchedulePairEntity>(Configuration)
                .AddDynamoDbSchedulesRepository<IStudentSchedulesRepository, StudentSchedulesRepository, StudentScheduleEntity, StudentScheduleDayEntity, StudentSchedulePairEntity>(Configuration)
                .AddScoped<IGroupSchedulesService, GroupSchedulesService>()
                .AddScoped<ITeacherSchedulesService, TeacherSchedulesService>();
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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
            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
