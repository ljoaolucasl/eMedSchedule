using eMedSchedule.WebApi.Config.Converters;
using eMedSchedule.WebApi.Filters;

namespace eMedSchedule.WebApi.Config
{
    public static class ControllersExtensionConfig
    {
        public static void ConfigureControllers(this IServiceCollection service)
        {
            service.AddControllers(config =>
            {
                config.Filters.Add<SerilogActionFilter>();
            }).AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));
        }
    }
}