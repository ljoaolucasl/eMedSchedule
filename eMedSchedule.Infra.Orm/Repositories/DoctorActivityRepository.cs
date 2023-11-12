using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Infra.Orm.Common;

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
    }
}