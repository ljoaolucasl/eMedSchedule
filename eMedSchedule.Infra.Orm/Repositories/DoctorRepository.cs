using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;

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
    }
}