using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace eMedSchedule.Infra.Orm.Common
{
    public class EMedScheduleContext : DbContext, IPersistenceContext
    {
        public EMedScheduleContext(DbContextOptions<EMedScheduleContext> options) : base(options)
        {
        }

        public async Task SaveDataAsync()
        {
            await SaveChangesAsync();
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
        }
    }
}