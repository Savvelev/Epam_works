using DataAccess;
using DataAccess.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            return services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<UniversityDbContext>(options => options.UseNpgsql(connectionString))
                .AddScoped<ILectorRepository, LectorsRepository>()
                .AddScoped<ILectureRepository, LecturesRepository>()
                .AddScoped<IStudentRepository, StudentsRepository>()
                .AddScoped<IHomeworkRepository, HomeworksRepository>()
                .AddScoped<IReportRepository, ReportRepository>()
                .AddScoped<IAttendanceRepository, AttendanceRepository>();

        }

        public async static Task SeedDataAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            UniversityDbContext context = null;

            try
            {
                context = services.GetRequiredService<UniversityDbContext>();
                await context.Database.MigrateAsync();
                await SeedTestData.SeedLecture(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger>();
                logger.LogError(ex, "An error occured during migration");
            }
            finally { await context.DisposeAsync(); }
        }
    }
}
