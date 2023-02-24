using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Repositories;
using KpiSchedule.Common.Repositories.Interfaces;
using KpiSchedule.Common.ServiceCollectionExtensions;
using KpiSchedule.Services;
using KpiSchedule.Services.Interfaces;
using Serilog.Events;

namespace KpiSchedule.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Add services to the container.
            builder.Services
                .AddSerilogConsoleLogger(LogEventLevel.Information)
                .AddDynamoDbSchedulesRepository<IGroupSchedulesRepository, GroupSchedulesRepository, GroupScheduleEntity, GroupScheduleDayEntity, GroupSchedulePairEntity>(config)
                .AddDynamoDbSchedulesRepository<ITeacherSchedulesRepository, TeacherSchedulesRepository, TeacherScheduleEntity, TeacherScheduleDayEntity, TeacherSchedulePairEntity>(config)
                .AddDynamoDbSchedulesRepository<IStudentSchedulesRepository, StudentSchedulesRepository, StudentScheduleEntity, StudentScheduleDayEntity, StudentSchedulePairEntity>(config)
                .AddScoped<IGroupSchedulesService, GroupSchedulesService>()
                .AddScoped<ITeacherSchedulesService, TeacherSchedulesService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}