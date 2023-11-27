using eMedSchedule.Infra.Orm.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eMedSchedule.Tests.Common
{
    public class EMedScheduleDesignFactory : IDesignTimeDbContextFactory<EMedScheduleContext>
    {
        public EMedScheduleContext CreateDbContext(string[] args)
        {
            Guid userId = Guid.Parse("A8BC593B-5945-417B-3C2A-08DBE8775234");

            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("PostgreSql");

            var optionsBuilder = new DbContextOptionsBuilder<EMedScheduleContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return new EMedScheduleContext(optionsBuilder.Options, new TestTenantProvider(userId));
        }
    }
}