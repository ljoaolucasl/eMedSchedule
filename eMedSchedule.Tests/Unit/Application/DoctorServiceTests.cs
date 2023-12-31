﻿using eMedSchedule.Application.Services;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using FizzWare.NBuilder;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using Moq;

namespace eMedSchedule.Tests.Unit.Application
{
    [TestClass]
    public class DoctorServiceTests
    {
        private Mock<IPersistenceContext> _context;
        private Mock<IDoctorRepository> _repositoryMoq;
        private Mock<IDoctorValidator> _validatorMoq;
        private DoctorService _service;
        private Doctor _doctor;

        [TestInitialize]
        public void Setup()
        {
            _context = new Mock<IPersistenceContext>();
            _repositoryMoq = new Mock<IDoctorRepository>();
            _validatorMoq = new Mock<IDoctorValidator>();

            _validatorMoq.Setup(v => v.Validate(It.IsAny<Doctor>())).Returns(new ValidationResult());

            _service = new(_context.Object, _repositoryMoq.Object, _validatorMoq.Object);
            _doctor = new("Carlos", "84526-SC", new byte[12]);
        }

        #region AddDoctor

        [TestMethod]
        public async Task Doctor_Service_Should_Insert_When_The_Doctor_When_Valid()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Build();

            var result = await _service.AddAsync(doctorToTest);

            result.Should().BeSuccess();
            _repositoryMoq.Verify(x => x.AddAsync(doctorToTest), Times.Once());
        }

        [TestMethod]
        public async Task Doctor_Service_Should_Not_Insert_When_The_Doctor_When_Invalid()
        {
            _validatorMoq.Setup(x => x.Validate(It.IsAny<Doctor>())).Returns(() =>
            {
                var result = new ValidationResult();
                result.Errors.Add(new ValidationFailure("error", "error"));
                return result;
            });

            var result = await _service.AddAsync(_doctor);

            result.Should().BeFailure();
            _repositoryMoq.Verify(x => x.AddAsync(_doctor), Times.Never());
        }

        #endregion AddDoctor

        #region UpdateDoctor

        [TestMethod]
        public async Task Doctor_Service_Should_Update_When_The_Doctor_When_Valid()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Build();

            var result = await _service.UpdateAsync(doctorToTest);

            result.Should().BeSuccess();
            _repositoryMoq.Verify(x => x.Update(doctorToTest), Times.Once());
        }

        [TestMethod]
        public async Task Doctor_Service_Should_Not_Update_When_The_Doctor_When_Invalid()
        {
            _validatorMoq.Setup(x => x.Validate(It.IsAny<Doctor>())).Returns(() =>
            {
                var result = new ValidationResult();
                result.Errors.Add(new ValidationFailure("erro", "erro"));
                return result;
            });

            var result = await _service.UpdateAsync(_doctor);

            result.Should().BeFailure();
            _repositoryMoq.Verify(x => x.Update(_doctor), Times.Never());
        }

        //[TestMethod]
        //public async Task Doctor_Service_Should_Not_Update_And_Catching_An_Exception_When_An_Exception_Is_Thrown()
        //{
        //    _repositoryMoq.Setup(x => x.Update(It.IsAny<Doctor>()))
        //        .Throws(() => new Exception());

        //    var result = await _service.UpdateAsync(_doctor);

        //    result.Should().BeFailure();
        //}

        #endregion UpdateDoctor

        #region DeleteDoctor

        [TestMethod]
        public async Task Doctor_Service_Should_Delete_When_The_Doctor_When_Valid()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Build();

            var result = await _service.DeleteAsync(doctorToTest);

            result.Should().BeSuccess();
            _repositoryMoq.Verify(x => x.Delete(doctorToTest), Times.Once());
        }

        [TestMethod]
        public async Task Doctor_Service_Should_Return_Success_When_Doctor_Has_No_Pending_Activity()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Build();

            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Appointment,
                new DateTime(2000, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            doctorToTest.Activities = new List<DoctorActivity>() { doctorActivityToTest };

            var result = await _service.DeleteAsync(doctorToTest);

            result.Should().BeSuccess();
            _repositoryMoq.Verify(x => x.Delete(doctorToTest), Times.Once());
        }

        [TestMethod]
        public async Task Doctor_Service_Should_Return_Failer_When_Doctor_Has_Pending_Activity()
        {
            var doctorToTest = Builder<Doctor>.CreateNew().Build();

            var doctorActivityToTest = new DoctorActivity("Title2", new List<Doctor>() { doctorToTest }, ActivityTypeEnum.Appointment,
                new DateTime(3000, 10, 10), new TimeSpan(3, 0, 0), new TimeSpan(5, 0, 0));

            doctorToTest.Activities = new List<DoctorActivity>() { doctorActivityToTest };

            var result = await _service.DeleteAsync(doctorToTest);

            result.Should().BeFailure();
            _repositoryMoq.Verify(x => x.Delete(doctorToTest), Times.Never());
        }

        #endregion DeleteDoctor

        [TestMethod]
        public void Doctor_Service_Should_Return_Failer_When_The_Doctor_Schedule_Surgery_Has_A_Conflict()
        {
            var doctorToTest = new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) };

            var doctorActivity = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 10), new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0));

            var doctorActivityToTest = new DoctorActivity("Title2", doctorToTest, ActivityTypeEnum.Surgery,
                new DateTime(2020, 10, 10), new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0));

            doctorToTest[0].Activities = new List<DoctorActivity>() { doctorActivity };

            List<Error> errors = new List<Error>();

            var result = doctorToTest[0].ValidateDoctorSchedule(doctorActivityToTest, errors);

            errors[0].Message.Should().Be($"Doctor {doctorToTest[0].Name} has a scheduling conflict at this time.");
        }
    }
}