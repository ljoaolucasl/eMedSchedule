using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Infra.Orm.Common;
using Microsoft.EntityFrameworkCore;

namespace eMedSchedule.Infra.Orm.Repositories
{
    public class DoctorActivityRepository : Repository<DoctorActivity>, IDoctorActivityRepository
    {
        public DoctorActivityRepository()
        {
        }

        public DoctorActivityRepository(EMedScheduleContext contextDb) : base(contextDb)
        {
        }

        public override async Task<DoctorActivity> RetrieveByIDAsync(Guid id)
        {
            return await Data
                .Include(x => x.Doctors)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<DoctorActivity>> RetrieveAllAsync()
        {
            return await Data.Include(x => x.Doctors).ToListAsync();
        }
    }
}