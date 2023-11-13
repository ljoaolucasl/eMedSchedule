namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<DoctorProfile>();
                opt.AddProfile<DoctorActivityProfile>();
            });
        }
    }
}