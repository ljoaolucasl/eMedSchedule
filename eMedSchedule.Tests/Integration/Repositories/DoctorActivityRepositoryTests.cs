using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using eMedSchedule.Infra.Orm.Repositories;
using FizzWare.NBuilder;

namespace eMedSchedule.Tests.Integration.Repositories
{
    [TestClass]
    public class DoctorActivityActivityRepositoryTests
    {
        private DoctorActivityRepository _doctorActivityRepository;
        private DoctorRepository _doctorRepository;

        private EMedScheduleContext _context;

        private Guid _userId;

        [TestInitialize]
        public void Setup()
        {
            TestBase.DeleteUser();

            _userId = TestBase.CadastrarUsuario();

            _context = new EMedScheduleDesignFactory().CreateDbContext(new string[] { _userId.ToString() });

            _doctorActivityRepository = new DoctorActivityRepository(_context);
            _doctorRepository = new DoctorRepository(_context);

            BuilderSetup.SetCreatePersistenceMethod<DoctorActivity>(_doctorActivityRepository.AddTest);
            BuilderSetup.SetCreatePersistenceMethod<Doctor>(_doctorRepository.AddTest);
        }

        #region CrudDoctorActivity

        [TestMethod]
        public async Task Doctor_Activity_Repository_Should_Insert_New_DoctorActivity_On_DatabaseAsync()
        {
            DoctorActivity doctorActivityToTest = Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorActivityToTest2 = await _doctorActivityRepository.RetrieveByIDAsync(doctorActivityToTest.Id);

            doctorActivityToTest2.Should().Be(doctorActivityToTest);
        }

        [TestMethod]
        public async Task Doctor_Activity_Repository_Should_Update_The_DoctorActivity_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorActivityToTest2 = await _doctorActivityRepository.RetrieveByIDAsync(doctorActivityToTest.Id);
            doctorActivityToTest2.Title = "Title";
            doctorActivityToTest2.Doctors = new List<Doctor>() { doctorToTest };
            doctorActivityToTest2.ActivityType = ActivityTypeEnum.Surgery;
            doctorActivityToTest2.Date = new DateTime(2020, 1, 1);
            doctorActivityToTest2.StartTime = new TimeSpan(5, 0, 0);
            doctorActivityToTest2.EndTime = new TimeSpan(10, 0, 0);

            _doctorActivityRepository.Update(doctorActivityToTest2);
            await _context.SaveChangesAsync();

            var doctorActivityToTest3 = await _doctorActivityRepository.RetrieveByIDAsync(doctorActivityToTest.Id);
            _doctorActivityRepository.RetrieveAllAsync().Result.Count.Should().Be(1);
            doctorActivityToTest3.Should().Be(doctorActivityToTest2);
        }

        [TestMethod]
        public async Task Doctor_Activity_Repository_Should_Delete_The_DoctorActivity_In_The_Database()
        {
            var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();
            var doctorActivityToTest2 = await _doctorActivityRepository.RetrieveByIDAsync(doctorActivityToTest.Id);

            _doctorActivityRepository.Delete(doctorActivityToTest2);
            await _context.SaveChangesAsync();

            _doctorActivityRepository.RetrieveAllAsync().Result.Count.Should().Be(0);
        }

        [TestMethod]
        public async Task Doctor_Activity_Repository_Should_Retrieve_The_DoctorActivity_In_The_Database()
        {
            var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorActivityToTest2 = await _doctorActivityRepository.RetrieveByIDAsync(doctorActivityToTest.Id);

            doctorActivityToTest2.Should().Be(doctorActivityToTest);
        }

        [TestMethod]
        public async Task Doctor_Activity_Repository_Should_Retrieve_Every_DoctorActivitys_In_The_Database()
        {
            var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorActivityToTest4 = Builder<DoctorActivity>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var listDoctorActivitysToTest = await _doctorActivityRepository.RetrieveAllAsync();

            listDoctorActivitysToTest[0].Should().Be(doctorActivityToTest);
            listDoctorActivitysToTest[3].Should().Be(doctorActivityToTest4);
            listDoctorActivitysToTest.Count.Should().Be(4);
        }

        #endregion CrudDoctorActivity
    }
}