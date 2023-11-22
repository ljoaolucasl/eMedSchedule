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
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("SqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<EMedScheduleContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new EMedScheduleContext(optionsBuilder.Options, new TestTenantProvider(Guid.Parse(args[0])));
        }
    }
}