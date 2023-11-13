using eMedSchedule.Application.Services;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using Moq;

namespace eMedSchedule.Tests.Unit.Application
{
    public class DoctorActivityServiceTests
    {
        private Mock<IPersistenceContext> _context;
        private Mock<IDoctorActivityRepository> _repositoryMoq;
        private DoctorActivityService _servico;
        private DoctorActivity _doctorActivity;

        [TestInitialize]
        public void Setup()
        {
            _context = new Mock<IPersistenceContext>();
            _repositoryMoq = new Mock<IDoctorActivityRepository>();
            _servico = new(_context.Object, _repositoryMoq.Object);
            _doctorActivity = new("Title", new List<Doctor>() { new Doctor("Carlos", "84526-SC") }, ActivityTypeEnum.Appointment,
                new DateTime(2020, 10, 10), new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0));
        }
    }
}