using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RestApi.Controllers;

namespace RestApi.LogServices
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddLog(this IServiceCollection services)
        {
            return services.AddTransient<StudentController>()
                           .AddTransient<LectorController>()
                           .AddTransient<LectureController>()
                           .AddTransient<HomeworkController>()
                           .AddLogging(builder =>
                           {
                               builder.SetMinimumLevel(LogLevel.Information);
                               builder.AddNLog("nlog.config");
                           });
        }
    }
}
