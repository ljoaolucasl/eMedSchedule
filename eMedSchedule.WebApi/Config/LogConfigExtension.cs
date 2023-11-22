using Serilog;

namespace eMedSchedule.WebApi.Config
{
    public static class LogConfigExtension
    {
        public static void ConfigureLog(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            services.AddSerilog(Log.Logger);
        }
    }
}