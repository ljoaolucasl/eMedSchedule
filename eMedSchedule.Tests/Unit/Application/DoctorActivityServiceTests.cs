using eMedSchedule.Application.Services;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using FizzWare.NBuilder;
using FluentResults.Extensions.FluentAssertions;
using Moq;

namespace eMedSchedule.Tests.Unit.Application
{
    [TestClass]
    public class DoctorActivityServiceTests
    {
        private Mock<IPersistenceContext> _context;
        private Mock<IDoctorActivityRepository> _repositoryMoq;
        private Mock<IDoctorActivityValidator> _validatorMoq;
        private DoctorActivityService _service;
        private DoctorActivity _doctorActivity;

        [TestInitialize]
        public void Setup()
        {
            _context = new Mock<IPersistenceContext>();
            _repositoryMoq = new Mock<IDoctorActivityRepository>();
            _validatorMoq = new Mock<IDoctorActivityValidator>();

            _validatorMoq.Setup(v => v.Validate(It.IsAny<DoctorActivity>())).Returns(new ValidationResult());

            _service = new(_context.Object, _repositoryMoq.Object, _validatorMoq.Object);
            _doctorActivity = new("Title", new List<Doctor>() { new Doctor("Carlos", "84526-SC", new byte[12]) }, ActivityTypeEnum.Appointment,
                new DateTime(2020, 10, 10), new TimeSpan(10, 0, 0), new TimeSpan(12, 0, 0));
        }

        #region AddDoctorActivity

        [TestMethod]
        public async Task Doctor_Activity_Service_Should_Insert_When_The_DoctorActivity_When_Valid()
        {
            var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().Build();

            var result = await _service.AddAsync(doctorActivityToTest);

            result.Should().BeSuccess();
            _repositoryMoq.Verify(x => x.AddAsync(doctorActivityToTest), Times.Once());
        }

        [TestMethod]
        public async Task Doctor_Activity_Service_Should_Not_Insert_When_The_DoctorActivity_When_Invalid()
        {
            _validatorMoq.Setup(x => x.Validate(It.IsAny<DoctorActivity>())).Returns(() =>
            {
                var result = new ValidationResult();
                result.Errors.Add(new ValidationFailure("error", "error"));
                return result;
            });

            var result = await _service.AddAsync(_doctorActivity);

            result.Should().BeFailure();
            _repositoryMoq.Verify(x => x.AddAsync(_doctorActivity), Times.Never());
        }

        //[TestMethod]
        //public async Task Doctor_Activity_Service_Should_Not_Insert_And_Catching_An_Exception_When_An_Exception_Is_Thrown()
        //{
        //    _repositoryMoq.Setup(x => x.AddAsync(It.IsAny<DoctorActivity>()))
        //        .Throws(() => new Exception());

        //    var result = await _service.AddAsync(_doctorActivity);

        //    result.Should().BeFailure();
        //}

        #endregion AddDoctorActivity

        #region UpdateDoctorActivity

        [TestMethod]
        public async Task Doctor_Activity_Service_Should_Update_When_The_DoctorActivity_When_Valid()
        {
            var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().Build();

            var result = await _service.UpdateAsync(doctorActivityToTest);

            result.Should().BeSuccess();
            _repositoryMoq.Verify(x => x.Update(doctorActivityToTest), Times.Once());
        }

        [TestMethod]
        public async Task Doctor_Activity_Service_Should_Not_Update_When_The_DoctorActivity_When_Invalid()
        {
            _validatorMoq.Setup(x => x.Validate(It.IsAny<DoctorActivity>())).Returns(() =>
            {
                var result = new ValidationResult();
                result.Errors.Add(new ValidationFailure("erro", "erro"));
                return result;
            });

            var result = await _service.UpdateAsync(_doctorActivity);

            result.Should().BeFailure();
            _repositoryMoq.Verify(x => x.Update(_doctorActivity), Times.Never());
        }

        //[TestMethod]
        //public async Task Doctor_Activity_Service_Should_Not_Update_And_Catching_An_Exception_When_An_Exception_Is_Thrown()
        //{
        //    _repositoryMoq.Setup(x => x.Update(It.IsAny<DoctorActivity>()))
        //        .Throws(() => new Exception());

        //    var result = await _service.UpdateAsync(_doctorActivity);

        //    result.Should().BeFailure();
        //}

        #endregion UpdateDoctorActivity

        #region DeleteDoctorActivity

        [TestMethod]
        public async Task Doctor_Activity_Service_Should_Delete_When_The_DoctorActivity_When_Valid()
        {
            var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().Build();

            var result = await _service.DeleteAsync(doctorActivityToTest);

            result.Should().BeSuccess();
            _repositoryMoq.Verify(x => x.Delete(doctorActivityToTest), Times.Once());
        }

        //[TestMethod]
        //public async Task Doctor_Activity_Service_Should_Delete_Not_When_The_DoctorActivity_When_Not_Exists()
        //{
        //    var doctorActivityToTest = Builder<DoctorActivity>.CreateNew().Build();

        //    var result = await _service.DeleteAsync(doctorActivityToTest);

        //    result.Should().BeFailure();
        //    _repositoryMoq.Verify(x => x.Delete(doctorActivityToTest), Times.Never());
        //}

        //[TestMethod]
        //public async Task Doctor_Activity_Service_Should_Not_Delete_And_Catching_An_Exception_When_An_Exception_Is_Thrown()
        //{
        //    DbUpdateException dbUpdateException = TestBase.CreateDbUpdateException("");
        //    _repositoryMoq.Setup(x => x.Delete(It.IsAny<DoctorActivity>())).Throws(dbUpdateException);

        //    var result = await _service.DeleteAsync(Builder<DoctorActivity>.CreateNew().Build());

        //    result.Should().BeFailure();
        //}

        #endregion DeleteDoctorActivity

        #region Trash

        //#region Testes Inserir

        //[TestMethod]
        //public void Nao_Deve_inserir_automovel_quando_ja_existe()
        //{
        //    //arrange
        //    _repositorioMoq.Setup(x => x.Existe(It.IsAny<Automovel>(), false)).Returns(true);

        //    //action
        //    var resultado = _service.Inserir(_automovel);

        //    //assent
        //    resultado.Should().BeFailure();
        //    resultado.Errors.OfType<CustomError>().FirstOrDefault().ErrorMessage.Should().Be("Esse Automóvel já existe");
        //    _repositorioMoq.Verify(x => x.Inserir(_automovel), Times.Never());
        //}

        //#endregion Testes Inserir

        //#region Testes Editar

        //[TestMethod]
        //public void Nao_Deve_editar_automovel_quando_ja_existe()
        //{
        //    //arrange
        //    _repositorioMoq.Setup(x => x.Existe(It.IsAny<Automovel>(), false)).Returns(true);

        //    //action
        //    var resultado = _service.Editar(_automovel);

        //    //assent
        //    resultado.Should().BeFailure();
        //    resultado.Errors.OfType<CustomError>().FirstOrDefault().ErrorMessage.Should().Be("Esse Automóvel já existe");
        //    _repositorioMoq.Verify(x => x.Editar(_automovel), Times.Never());
        //}

        //[TestMethod]
        //public void Deve_verificar_a_disponibilidade_do_automovel()
        //{
        //    //arrange
        //    _validadorMoq.Setup(x => x.VerificarSeAlugado(It.IsAny<Automovel>())).Returns(true);

        //    //action
        //    Result resultado = _service.VerificarDisponibilidade(_automovel);

        //    //assert
        //    resultado.Should().BeFailure();
        //    resultado.Errors.OfType<CustomError>().FirstOrDefault().ErrorMessage.Should().Be("Esse Automóvel está relacionado a um Aluguel em Aberto." +
        //                " Primeiro conclua o Aluguel relacionado");
        //}

        //#endregion Testes Editar

        //#region Testes Excluir

        //[TestMethod]
        //public void Nao_Deve_excluir_automovel_quando_relacionado_ao_aluguel()
        //{
        //    //arrange
        //    DbUpdateException dbUpdateException = TesteBase.CriarDbUpdateException("FK_TBAluguel_TBAutomovel");

        //    _repositorioMoq.Setup(x => x.Existe(It.IsAny<Automovel>(), true)).Returns(true);
        //    _repositorioMoq.Setup(x => x.Excluir(It.IsAny<Automovel>())).Throws(dbUpdateException);

        //    //action
        //    var resultado = _service.Excluir(Builder<Automovel>.CreateNew().Build());

        //    //assert
        //    resultado.Should().BeFailure();
        //    resultado.Errors.OfType<CustomError>().FirstOrDefault().ErrorMessage.Should().Be("Esse Automóvel está relacionado a um Aluguel. Primeiro exclua o Aluguel relacionado");
        //}

        //#endregion Testes Excluir

        #endregion Trash
    }
}