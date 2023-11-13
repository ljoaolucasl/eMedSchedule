using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eMedSchedule.Infra.Orm.Common
{
    public class EMedScheduleDesignFactory : IDesignTimeDbContextFactory<EMedScheduleContext>
    {
        public EMedScheduleContext CreateDbContext(string[] args)
        {
            var configuracao = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<EMedScheduleContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new EMedScheduleContext(optionsBuilder.Options);
        }
    }
}