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

        public override async Task<Doctor> RetrieveByIDAsync(Guid id)
        {
            return await Data
                .Include(x => x.Activities)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<Doctor>> RetrieveAllAsync()
        {
            return await Data.Include(x => x.Activities).ToListAsync();
        }

        public List<Doctor> RetrieveMany(List<Guid> doctorsIds)
        {
            return Data.Where(doctor => doctorsIds.Contains(doctor.Id)).ToList();
        }
    }
}