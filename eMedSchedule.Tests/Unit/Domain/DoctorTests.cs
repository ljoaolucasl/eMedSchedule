using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using FizzWare.NBuilder;
using Moq;

namespace eMedSchedule.Tests.Unit.Domain
{
    [TestClass]
    public class DoctorTests
    {
        private DoctorValidator _validator;
        private DoctorActivityValidator _validatorActivity;
        private Doctor _doctor;
        private DoctorActivity _doctorActivity;

        [TestInitialize]
        public void Setup()
        {
            _validator = new DoctorValidator();
            _validatorActivity = new DoctorActivityValidator();
            _doctor = new("Carlos", "84526-SC", new byte[12]);
            _doctorActivity = new("Title", new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) }, ActivityTypeEnum.Appointment,
                new DateTime(2020, 10, 10), new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0));
        }

        [TestMethod]
        public void Doctor_Validate_Should_Return_True_When_All_Rules_Are_OK()
        {
            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeTrue();
        }

        #region Name

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Name_Is_Less_Than_3_Characters_Long()
        {
            _doctor.Name = "Ca";

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Name_Has_A_Special_Character()
        {
            _doctor.Name = "Car@";

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Name_Is_Empty()
        {
            _doctor.Name = "";

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Name_Is_Null()
        {
            _doctor.Name = null;

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        #endregion Name

        #region CRM

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Crm_Is_Empty()
        {
            _doctor.CRM = "";

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Crm_Is_Null()
        {
            _doctor.CRM = null;

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Crm_Is_In_The_Wrong_Format()
        {
            _doctor.CRM = "45526-33";

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        #endregion CRM

        #region ProfilePicture

        [TestMethod]
        public void Doctor_Validate_Should_Return_False_When_Doctor_Profile_Picture_Is_Larger_Than_2_Mb()
        {
            int bytesSize = 2 * 1024 * 1024 + 1;
            byte[] size = new byte[bytesSize];
            new Random().NextBytes(size);

            _doctor.ProfilePicture = size;

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Validate_Should_Return_True_When_Doctor_Profile_Picture_Is_Null()
        {
            _doctor.ProfilePicture = null;

            ValidationResult result = _validator.Validate(_doctor);

            result.IsValid.Should().BeTrue();
        }

        #endregion ProfilePicture

        #region ValidateActivityPending

        [TestMethod]
        public async Task Doctor_Should_Return_True_When_Doctor_Has_No_Pending_Activity()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Build();

            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Appointment,
                new DateTime(2000, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            doctorToTest.Activities = new List<DoctorActivity>() { doctorActivityToTest };

            var result = doctorToTest.ValidateDoctorPendingActivity(doctorToTest);

            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task Doctor_Should_Return_False_When_Doctor_Has_Pending_Activity()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Build();

            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Appointment,
                new DateTime(3000, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            doctorToTest.Activities = new List<DoctorActivity>() { doctorActivityToTest };

            var result = doctorToTest.ValidateDoctorPendingActivity(doctorToTest);

            result.Should().BeFalse();
        }

        #endregion ValidateActivityPending

        #region ValidateSchedule

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_True_When_The_Doctor_Schedule_Appointment_Success()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Appointment,
                new DateTime(2020, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.StartTime = new TimeSpan(6, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(10, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_The_Doctor_Schedule_Appointment_Has_A_Conflict()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Appointment,
                new DateTime(2020, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.StartTime = new TimeSpan(1, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(3, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_The_Doctor_Schedule_Appointment_Has_A_Conflict_2()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Appointment,
                new DateTime(2020, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.StartTime = new TimeSpan(5, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(7, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_The_Doctor_Schedule_Appointment_Has_A_Conflict_3()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Appointment,
                new DateTime(2020, 10, 10), new TimeSpan(1, 0, 0), new TimeSpan(5, 0, 0));

            _doctorActivity.Date = new DateTime(2020, 10, 9);
            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.StartTime = new TimeSpan(23, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(1, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_True_When_The_Doctor_Schedule_Surgery_Success()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 10), new TimeSpan(7, 0, 0), new TimeSpan(10, 0, 0));

            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.ActivityType = ActivityTypeEnum.Surgery;
            _doctorActivity.StartTime = new TimeSpan(1, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(3, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_The_Doctor_Schedule_Surgery_Has_A_Conflict()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 10), new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0));

            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.ActivityType = ActivityTypeEnum.Surgery;
            _doctorActivity.StartTime = new TimeSpan(1, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(3, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_The_Doctor_Schedule_Surgery_Has_A_Conflict_2()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.StartTime = new TimeSpan(8, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(10, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_The_Doctor_Schedule_Surgery_Has_A_Conflict_3()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            _doctorActivity.Date = new DateTime(2020, 10, 9);
            _doctorActivity.Doctors = doctorToTest;
            _doctorActivity.ActivityType = ActivityTypeEnum.Surgery;
            _doctorActivity.StartTime = new TimeSpan(23, 0, 0);
            _doctorActivity.EndTime = new TimeSpan(1, 0, 0);

            doctorToTest[0].Activities = new List<DoctorActivity>() { _doctorActivity };

            ValidationResult result = _validatorActivity.Validate(doctorActivityToTest);

            result.IsValid.Should().BeFalse();
        }

        #endregion ValidateSchedule

        #region CalculateWorkedHour

        [TestMethod]
        public void Doctor_Should_Return_3_Hours_When_Correctly_Calculated()
        {
            var doctorToTest = new Doctor("Carlos", "84526-SC", new byte[12]);
            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));
            _doctorActivity.Doctors.Add(doctorToTest);
            doctorToTest.Activities = new List<DoctorActivity>() { _doctorActivity, doctorActivityToTest };

            doctorToTest.CalculateWorkedHourDoctorsPeriod(new DateTime(2020, 10, 10), new DateTime(2020, 10, 11));

            doctorToTest.WorkedHours.Should().Be(new TimeSpan(3, 0, 0));
        }

        [TestMethod]
        public void Doctor_Should_Return_2_Hours_When_Correctly_Calculated2()
        {
            var doctorToTest = new Doctor("Carlos", "84526-SC", new byte[12]);
            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 11), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));
            _doctorActivity.Doctors.Add(doctorToTest);
            doctorToTest.Activities = new List<DoctorActivity>() { _doctorActivity, doctorActivityToTest };

            doctorToTest.CalculateWorkedHourDoctorsPeriod(new DateTime(2020, 10, 11), new DateTime(2020, 10, 11));

            doctorToTest.WorkedHours.Should().Be(new TimeSpan(2, 0, 0));
        }

        [TestMethod]
        public void Doctor_Should_Return_2_Hours_When_Correctly_Calculated3()
        {
            var doctorToTest = new Doctor("Carlos", "84526-SC", new byte[12]);
            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 11), new TimeSpan(23, 0, 0), new TimeSpan(1, 0, 0));
            _doctorActivity.Doctors.Add(doctorToTest);
            doctorToTest.Activities = new List<DoctorActivity>() { _doctorActivity, doctorActivityToTest };

            doctorToTest.CalculateWorkedHourDoctorsPeriod(new DateTime(2020, 10, 11), new DateTime(2020, 10, 11));

            doctorToTest.WorkedHours.Should().Be(new TimeSpan(2, 0, 0));
        }

        [TestMethod]
        public void Doctor_Should_Return_2_Hours_When_Correctly_Calculated4()
        {
            var doctorToTest = new Doctor("Carlos", "84526-SC", new byte[12]);
            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Surgery,
                new DateTime(3000, 10, 11), new TimeSpan(23, 0, 0), new TimeSpan(1, 0, 0));
            _doctorActivity.Doctors.Add(doctorToTest);
            doctorToTest.Activities = new List<DoctorActivity>() { _doctorActivity, doctorActivityToTest };

            doctorToTest.CalculateWorkedHourDoctorsPeriod(new DateTime(2020, 10, 11), new DateTime(2020, 10, 11));

            doctorToTest.WorkedHours.Should().Be(new TimeSpan(0, 0, 0));
        }

        #endregion CalculateWorkedHour
    }
}