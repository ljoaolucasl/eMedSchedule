namespace eMedSchedule.WebApi.Config
{
    public static class ValidationConfigExtension
    {
        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;
             });
        }
    }
}