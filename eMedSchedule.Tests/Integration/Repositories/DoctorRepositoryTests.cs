using eMedSchedule.Domain.DoctorActivityModule;
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
        private DoctorActivityRepository _doctorActivityRepository;

        private EMedScheduleContext _context;

        private Guid _userId;

        [TestInitialize]
        public async Task Setup()
        {
            TestBase.DeleteData();

            _userId = TestBase.RegisterUser();

            _context = new EMedScheduleDesignFactory().CreateDbContext(null);

            _doctorRepository = new DoctorRepository(_context);
            _doctorActivityRepository = new DoctorActivityRepository(_context);

            BuilderSetup.SetCreatePersistenceMethod<Doctor>(_doctorRepository.AddTest);
            BuilderSetup.SetCreatePersistenceMethod<DoctorActivity>(_doctorActivityRepository.AddTest);
        }

        #region CrudDoctor

        [TestMethod]
        public async Task Doctor_Repository_Should_Insert_New_Doctor_On_DatabaseAsync()
        {
            Doctor doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();

            await _context.SaveChangesAsync();

            var doctorToTest2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);

            doctorToTest2.Should().Be(doctorToTest);
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_Update_The_Doctor_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
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
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();
            var doctorToTest2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);

            _doctorRepository.Delete(doctorToTest2);
            await _context.SaveChangesAsync();

            _doctorRepository.RetrieveAllAsync().Result.Count.Should().Be(0);
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_Retrieve_The_Doctor_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorToTest2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);

            doctorToTest2.Should().Be(doctorToTest);
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_Retrieve_Every_Doctors_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorToTest4 = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var listDoctorsToTest = await _doctorRepository.RetrieveAllAsync();

            listDoctorsToTest[0].Should().Be(doctorToTest);
            listDoctorsToTest[3].Should().Be(doctorToTest4);
            listDoctorsToTest.Count.Should().Be(4);
        }

        #endregion CrudDoctor

        #region RetrieveMany

        [TestMethod]
        public async Task Doctor_Repository_Should_Retrieve_Many_Doctors_In_The_Database()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorToTest2 = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorToTest3 = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorToTest4 = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var listDoctorsToTest = _doctorRepository
                .RetrieveMany(new List<Guid>() { doctorToTest.Id, doctorToTest2.Id, doctorToTest3.Id, doctorToTest4.Id });

            listDoctorsToTest[0].Should().Be(doctorToTest);
            listDoctorsToTest[3].Should().Be(doctorToTest4);
            listDoctorsToTest.Count.Should().Be(4);
        }

        #endregion RetrieveMany

        #region CRMExists

        [TestMethod]
        public async Task Doctor_Repository_Should_True_When_Doctor_Crm_Exists()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorToCheck = new Doctor("Marcos", doctorToTest.CRM, new byte[12]);

            bool result = _doctorRepository.Exist(doctorToCheck);

            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task Doctor_Repository_Should_False_When_Doctor_Crm_Not_Exists()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).Persist();
            await _context.SaveChangesAsync();

            var doctorToCheck = new Doctor("Marcos", "45621-SC", new byte[12]);

            bool result = _doctorRepository.Exist(doctorToCheck);

            result.Should().BeFalse();
        }

        #endregion CRMExists

        #region CalculateWorkedHours

        [TestMethod]
        public async Task Doctor_Repository_Should_True_When_Doctor_Crm_Exists2()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).With(x => x.WorkedHours = TimeSpan.Zero).Persist();
            await _context.SaveChangesAsync();

            var doctor = await _doctorRepository.RetrieveByIDAsync(doctorToTest.Id);

            var activityToTest = Builder<DoctorActivity>.CreateNew()
                .With(x => x.UserId = _userId)
                .With(x => x.Doctors = new List<Doctor>() { doctor })
                .With(x => x.StartTime = new TimeSpan(10, 0, 0))
                .With(x => x.EndTime = new TimeSpan(11, 0, 0))
                .With(x => x.Date = new DateTime(2023, 10, 10))
                .Persist();
            await _context.SaveChangesAsync();

            var doctorToTest2 = Builder<Doctor>.CreateNew().With(x => x.UserId = _userId).With(x => x.WorkedHours = TimeSpan.Zero).Persist();
            await _context.SaveChangesAsync();

            var doctor2 = await _doctorRepository.RetrieveByIDAsync(doctorToTest2.Id);

            var activityToTest2 = Builder<DoctorActivity>.CreateNew()
                .With(x => x.UserId = _userId)
                .With(x => x.Doctors = new List<Doctor>() { doctor2, doctor })
                .With(x => x.StartTime = new TimeSpan(10, 0, 0))
                .With(x => x.EndTime = new TimeSpan(11, 0, 0))
                .With(x => x.Date = new DateTime(2023, 10, 10))
                .Persist();
            await _context.SaveChangesAsync();

            var doctorsToTest = _doctorRepository.GetListDoctorsMoreHoursWorked(new DateTime(2023, 10, 10), new DateTime(2023, 10, 11));

            doctorsToTest[1].WorkedHours.Should().Be(new TimeSpan(1, 0, 0));
            doctorsToTest[0].WorkedHours.Should().Be(new TimeSpan(2, 0, 0));
            doctorsToTest[0].Should().Be(doctor);
            doctorsToTest.Count.Should().Be(2);
        }

        #endregion CalculateWorkedHours
    }
}