using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using CustomParser;

namespace M05_Exception_Handling_Logging_NLog
{
    class Program      
    {      
        static void Main(string[] args)
        {           

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                try
                {
                    var library = serviceProvider.GetService<StringParser>();
                    Console.WriteLine(library.ParseFromStringToInt("32583484545"));
                }                
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }               
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<StringParser>();

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddNLog("nlog.config");
            });
        }
    }
}
