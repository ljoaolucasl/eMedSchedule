using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using Microsoft.EntityFrameworkCore;

namespace eMedSchedule.Infra.Orm.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository()
        {
        }

        public DoctorRepository(EMedScheduleContext contextDb) : base(contextDb)
        {
        }

        public async Task<List<Doctor>> RetrieveManyAsync(List<Guid> doctorsIds)
        {
            return await Data.Where(categoria => doctorsIds.Contains(categoria.Id)).ToListAsync();
        }
    }
}