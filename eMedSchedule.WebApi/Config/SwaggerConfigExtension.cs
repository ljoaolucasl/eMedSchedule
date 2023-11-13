namespace eMedSchedule.WebApi.Config
{
    public static class SwaggerConfigExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}