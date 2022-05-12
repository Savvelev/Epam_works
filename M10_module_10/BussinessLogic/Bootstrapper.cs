using BussinessLogic.Report;
using BussinessLogic.Services;
using Domain;
using Domain.Exceptions.BadRequest;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BussinessLogic
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddBussinessLogic(this IServiceCollection services)
        {
            return services
                .AddScoped<IHomeworkService, HomeworksService>()
                .AddScoped<ILectorService, LectorsService>()
                .AddScoped<ILectureService, LecturesService>()
                .AddScoped<IStudentService, StudentsService>()
                .AddScoped<IAttendanceService, AttendancesService>()
                .AddScoped<JsonReport>()
                .AddScoped<XmlReport>()
                .AddScoped<IWarningService, WarningService>()
                .AddTransient<Func<FormatSourse, IFormatReport>>(serviceProvider => key =>
                {
                    switch (key)
                    {
                        case FormatSourse.Json:
                            return serviceProvider.GetService<JsonReport>();
                        case FormatSourse.Xml:
                            return serviceProvider.GetService<XmlReport>();
                        default:
                            throw new OutputTypeReportException();
                    }
                    // some chages
                    var a = 2;
                    var b = 4;
                });
        }
    }
}
