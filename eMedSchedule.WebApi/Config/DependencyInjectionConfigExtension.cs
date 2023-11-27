using eMedSchedule.Application.AuthenticationModule;
using eMedSchedule.Application.Services;
using eMedSchedule.Domain.AuthenticationModule;
using eMedSchedule.Domain.Common;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using eMedSchedule.Infra.Orm.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eMedSchedule.WebApi.Config
{
    public static class DependencyInjectionConfigExtension
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("PostgreSql");

            services.AddDbContext<IPersistenceContext, EMedScheduleContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(connectionString);
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });

            services.AddTransient<ITenantProvider, ApiTenantProvider>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserValidator, UserValidator>();

            services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<EMedScheduleContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IDoctorValidator, DoctorValidator>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();

            services.AddTransient<IDoctorActivityService, DoctorActivityService>();
            services.AddTransient<IDoctorActivityValidator, DoctorActivityValidator>();
            services.AddScoped<IDoctorActivityRepository, DoctorActivityRepository>();

            services.AddTransient<ConfigureDoctorMappingAction>();
        }
    }
}