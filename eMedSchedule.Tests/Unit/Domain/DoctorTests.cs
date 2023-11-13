﻿using eMedSchedule.Domain.DoctorModule;
using FluentAssertions;
using FluentValidation.Results;

namespace eMedSchedule.Tests.Unit.Domain
{
    [TestClass]
    public class DoctorTests
    {
        private DoctorValidator _validator;
        private Doctor _doctor;

        [TestInitialize]
        public void Setup()
        {
            _validator = new DoctorValidator();
            _doctor = new("Carlos", "84526-SC", new byte[12]);
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

            result.IsValid.Should().BeFalse();
        }

        #endregion ProfilePicture
    }
}