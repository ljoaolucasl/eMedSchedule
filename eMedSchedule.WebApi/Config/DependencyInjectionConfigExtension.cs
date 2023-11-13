using eMedSchedule.Application.Services;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using eMedSchedule.Infra.Orm.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eMedSchedule.WebApi.Config
{
    public static class DependencyInjectionConfigExtension
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("SqlServer");

            services.AddDbContext<IPersistenceContext, EMedScheduleContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<DoctorService>();

            services.AddTransient<IDoctorActivityRepository, DoctorActivityRepository>();
            services.AddTransient<DoctorActivityService>();

            services.AddTransient<ConfigureDoctorMappingAction>();
        }
    }
}