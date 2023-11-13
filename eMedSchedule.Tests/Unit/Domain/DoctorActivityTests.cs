﻿using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using FluentAssertions;
using FluentValidation.Results;

namespace eMedSchedule.Tests.Unit.Domain
{
    [TestClass]
    public class DoctorActivityTests
    {
        private DoctorActivityValidator _validator;
        private DoctorActivity _doctorActivity;

        [TestInitialize]
        public void Setup()
        {
            _validator = new DoctorActivityValidator();
            _doctorActivity = new("Title", new List<Doctor>() { new Doctor("Carlos", "84526-SC") }, ActivityType.Appointment, new DateTime(2020, 10, 10), new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0));
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_True_When_All_Rules_Are_OK()
        {
            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeTrue();
        }

        #region Title

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Title_Is_Less_Than_3_Characters_Long()
        {
            _doctorActivity.Title = "Ti";

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Title_Has_A_Special_Character()
        {
            _doctorActivity.Title = "Tit@";

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Title_Is_Empty()
        {
            _doctorActivity.Title = "";

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Title_Is_Null()
        {
            _doctorActivity.Title = null;

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        #endregion Title

        #region Doctors

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Doctors_Is_Empty()
        {
            _doctorActivity.Doctors = new List<Doctor>();

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Doctors_Is_Null()
        {
            _doctorActivity.Doctors = null;

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        #endregion Doctors

        #region ActivityType

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Activity_Type_Is_Not_A_Valid_Enum()
        {
            _doctorActivity.ActivityType = (ActivityType)10;

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Activity_Type_Is_Null()
        {
            _doctorActivity.ActivityType = default;

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        #endregion ActivityType

        #region Date

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Date_Is_Null()
        {
            _doctorActivity.Date = default;

            ValidationResult result = _validator.Validate(_doctorActivity);

            result.IsValid.Should().BeFalse();
        }

        #endregion Date

        #region StartTime

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_Start_Time_Is_Greater_Than_End_Time()
        {
            _doctorActivity.StartTime = new TimeSpan(12, 0, 0);

            ValidationResult resultado = _validator.Validate(_doctorActivity);

            resultado.IsValid.Should().BeFalse();
        }

        #endregion StartTime

        #region EndTime

        [TestMethod]
        public void Doctor_Activity_Validate_Should_Return_False_When_Doctor_Activity_End_Time_Is_Less_Than_Start_Time()
        {
            _doctorActivity.EndTime = new TimeSpan(9, 0, 0);

            ValidationResult resultado = _validator.Validate(_doctorActivity);

            resultado.IsValid.Should().BeFalse();
        }

        #endregion EndTime
    }
}