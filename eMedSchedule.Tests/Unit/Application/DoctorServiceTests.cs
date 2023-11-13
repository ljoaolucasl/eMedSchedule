using eMedSchedule.Application.Services;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using Moq;

namespace eMedSchedule.Tests.Unit.Application
{
    public class DoctorServiceTests
    {
        private Mock<IPersistenceContext> _context;
        private Mock<IDoctorRepository> _repositoryMoq;
        private DoctorService _servico;
        private Doctor _doctor;

        [TestInitialize]
        public void Setup()
        {
            _context = new Mock<IPersistenceContext>();
            _repositoryMoq = new Mock<IDoctorRepository>();
            _servico = new(_context.Object, _repositoryMoq.Object);
            _doctor = new("Carlos", "84526-SC");
        }
    }
}