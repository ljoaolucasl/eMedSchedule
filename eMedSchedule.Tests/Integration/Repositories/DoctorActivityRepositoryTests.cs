using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Infra.Orm.Common;
using eMedSchedule.Infra.Orm.Repositories;
using FizzWare.NBuilder;

namespace eMedSchedule.Tests.Integration.Repositories
{
    public class DoctorActivityRepositoryTests
    {
        private DoctorActivityRepository _doctorActivityRepository;

        private EMedScheduleContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new EMedScheduleDesignFactory().CreateDbContext(null);

            _doctorActivityRepository = new DoctorActivityRepository(_context);

            _context.RemoveRange(_doctorActivityRepository.Data);

            BuilderSetup.SetCreatePersistenceMethod<DoctorActivity>(_doctorActivityRepository.AddAsync);
        }
    }
}