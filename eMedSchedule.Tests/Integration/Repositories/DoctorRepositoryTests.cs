using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using eMedSchedule.Infra.Orm.Repositories;
using FizzWare.NBuilder;

namespace eMedSchedule.Tests.Integration.Repositories
{
    [TestClass]
    public class DoctorRepositoryTests
    {
        private DoctorRepository _doctorRepository;

        private EMedScheduleContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new EMedScheduleDesignFactory().CreateDbContext(null);

            _doctorRepository = new DoctorRepository(_context);

            _context.RemoveRange(_doctorRepository.Data);

            BuilderSetup.SetCreatePersistenceMethod<Doctor>(_doctorRepository.AddTest);
        }

        #region CrudDoctor

        [TestMethod]
        public async Task Doctor_Repository_Should_Insert_New_Doctor_On_DatabaseAsync()
        {
            Doctor doctorToTest = Builder<Doctor>.CreateNew().Persist();

            await _context.SaveChangesAsync();

            var doctorToTest2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);

            doctorToTest2.Should().Be(doctorToTest);
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_Update_The_Doctor_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Persist();
            _context.SaveChanges();

            var doctorToTest2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);
            doctorToTest2.Name = "Carlos";
            doctorToTest2.CRM = "84526-SC";

            _doctorRepository.Update(doctorToTest2);
            await _context.SaveChangesAsync();

            var doctorToTest3 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);
            _doctorRepository.RetrieveAllAsync().Result.Count.Should().Be(1);
            doctorToTest3.Should().Be(doctorToTest2);
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_Delete_The_Doctor_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();
            var doctorToTest2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);

            _doctorRepository.Delete(doctorToTest2);
            await _context.SaveChangesAsync();

            _doctorRepository.RetrieveAllAsync().Result.Count.Should().Be(0);
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_Retrieve_The_Doctor_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            var doctorToTest2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);

            doctorToTest2.Should().Be(doctorToTest);
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_Retrieve_Every_Doctors_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            var doctorToTest4 = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            var listDoctorsToTest = await _doctorRepository.RetrieveAllAsync();

            listDoctorsToTest[0].Should().Be(doctorToTest);
            listDoctorsToTest[3].Should().Be(doctorToTest4);
            listDoctorsToTest.Count.Should().Be(4);
        }

        #endregion CrudDoctor

        [TestMethod]
        public async Task Doctor_Repository_Should_Retrieve_Many_Doctors_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            var doctorToTest2 = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            var doctorToTest3 = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            var doctorToTest4 = Builder<Doctor>.CreateNew().Persist();
            await _context.SaveChangesAsync();

            var listDoctorsToTest = _doctorRepository
                .RetrieveMany(new List<Guid>() { doctorToTest.Id, doctorToTest2.Id, doctorToTest3.Id, doctorToTest4.Id });

            listDoctorsToTest[0].Should().Be(doctorToTest);
            listDoctorsToTest[3].Should().Be(doctorToTest4);
            listDoctorsToTest.Count.Should().Be(4);
        }
    }
}