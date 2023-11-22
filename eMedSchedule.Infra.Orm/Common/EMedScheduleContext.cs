using eMedSchedule.Domain.AuthenticationModule;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace eMedSchedule.Infra.Orm.Common
{
    public class EMedScheduleContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IPersistenceContext
    {
        private Guid _userId;

        public EMedScheduleContext(DbContextOptions<EMedScheduleContext> options, ITenantProvider? tenantProvider) : base(options)
        {
            if (tenantProvider != null)
                _userId = tenantProvider.UserId;
        }

        public async Task SaveDataAsync()
        {
            await SaveChangesAsync();
        }

        public void SaveData()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(Log.Logger);
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EMedScheduleContext).Assembly);

            modelBuilder.Entity<Doctor>().HasQueryFilter(x => x.UserId == _userId);
            modelBuilder.Entity<DoctorActivity>().HasQueryFilter(x => x.UserId == _userId);
        }
    }
}